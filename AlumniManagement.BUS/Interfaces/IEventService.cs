using AlumniManagement.Shared.DTOs.Event;
using AlumniManagement.Shared.DTOs.Alumni;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlumniManagement.BUS.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<EventDto> GetEventByIdAsync(int id);
        Task<EventDto> CreateEventAsync(CreateEventRequest request, int createdBy);
        Task<bool> UpdateEventAsync(int id, UpdateEventRequest request);
        Task<bool> DeleteEventAsync(int id);
        Task<bool> CancelEventAsync(int id);
        Task<bool> RegisterForEventAsync(int eventId, int alumniId);
        Task<IEnumerable<AlumniDto>> GetEventParticipantsAsync(int eventId);
        Task<IEnumerable<EventDto>> GetUpcomingEventsAsync();
    }
}
