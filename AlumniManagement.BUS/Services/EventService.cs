using AlumniManagement.Shared.DTOs.Event;
using AlumniManagement.Shared.DTOs.Alumni;
using AlumniManagement.BUS.Interfaces;
using AlumniManagement.DAL.Entities;
using AlumniManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniManagement.BUS.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IAlumniEventRepository _alumniEventRepository;

        public EventService(
            IEventRepository eventRepository,
            IAlumniEventRepository alumniEventRepository)
        {
            _eventRepository = eventRepository;
            _alumniEventRepository = alumniEventRepository;
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            return events.Select(MapToDto);
        }

        public async Task<EventDto> GetEventByIdAsync(int id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity == null)
                throw new InvalidOperationException("Event not found");

            return MapToDto(eventEntity);
        }

        public async Task<EventDto> CreateEventAsync(CreateEventRequest request, int createdBy)
        {
            var eventEntity = new Event
            {
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                EventDate = request.EventDate,
                CreatedBy = createdBy,
                IsCanceled = false,
                CreatedAt = DateTime.Now
            };

            var created = await _eventRepository.AddAsync(eventEntity);
            return MapToDto(created);
        }

        public async Task<bool> UpdateEventAsync(int id, UpdateEventRequest request)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity == null)
                throw new InvalidOperationException("Event not found");

            if (!string.IsNullOrEmpty(request.Title))
                eventEntity.Title = request.Title;

            if (!string.IsNullOrEmpty(request.Description))
                eventEntity.Description = request.Description;

            if (!string.IsNullOrEmpty(request.Location))
                eventEntity.Location = request.Location;

            if (request.EventDate.HasValue)
                eventEntity.EventDate = request.EventDate.Value;

            await _eventRepository.UpdateAsync(eventEntity);
            return true;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity == null)
                throw new InvalidOperationException("Event not found");

            await _eventRepository.DeleteAsync(eventEntity);
            return true;
        }

        public async Task<bool> CancelEventAsync(int id)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity == null)
                throw new InvalidOperationException("Event not found");

            eventEntity.IsCanceled = true;
            await _eventRepository.UpdateAsync(eventEntity);
            return true;
        }

        public async Task<bool> RegisterForEventAsync(int eventId, int alumniId)
        {
            var eventEntity = await _eventRepository.GetByIdAsync(eventId);
            if (eventEntity == null)
                throw new InvalidOperationException("Event not found");

            if (eventEntity.IsCanceled)
                throw new InvalidOperationException("Cannot register for canceled event");

            var isRegistered = await _alumniEventRepository.IsRegisteredAsync(alumniId, eventId);
            if (isRegistered)
                throw new InvalidOperationException("Already registered for this event");

            var registration = new AlumniEvent
            {
                AlumniId = alumniId,
                EventId = eventId,
                RegisterDate = DateTime.Now
            };

            await _alumniEventRepository.AddAsync(registration);
            return true;
        }

        public async Task<IEnumerable<AlumniDto>> GetEventParticipantsAsync(int eventId)
        {
            var participants = await _eventRepository.GetEventParticipantsAsync(eventId);
            return participants.Select(MapAlumniToDto);
        }

        public async Task<IEnumerable<EventDto>> GetUpcomingEventsAsync()
        {
            var events = await _eventRepository.GetUpcomingEventsAsync();
            return events.Select(MapToDto);
        }

        private EventDto MapToDto(Event eventEntity)
        {
            return new EventDto
            {
                EventId = eventEntity.EventId,
                Title = eventEntity.Title,
                Description = eventEntity.Description,
                Location = eventEntity.Location,
                EventDate = eventEntity.EventDate,
                CreatedBy = eventEntity.CreatedBy,
                IsCanceled = eventEntity.IsCanceled,
                CreatedAt = eventEntity.CreatedAt
            };
        }

        private AlumniDto MapAlumniToDto(Alumni alumni)
        {
            return new AlumniDto
            {
                AlumniId = alumni.AlumniId,
                StudentCode = alumni.StudentCode,
                FullName = alumni.FullName,
                DateOfBirth = alumni.DateOfBirth,
                Gender = alumni.Gender,
                Email = alumni.Email,
                Phone = alumni.Phone,
                GraduationYear = alumni.GraduationYear,
                Major = alumni.Major,
                CurrentJob = alumni.CurrentJob,
                Company = alumni.Company,
                Address = alumni.Address,
                IsActive = alumni.IsActive,
                CreatedAt = alumni.CreatedAt,
                UpdatedAt = alumni.UpdatedAt
            };
        }
    }
}
