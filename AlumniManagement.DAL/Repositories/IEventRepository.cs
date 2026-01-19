using AlumniManagement.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlumniManagement.DAL.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetUpcomingEventsAsync();
        Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Alumni>> GetEventParticipantsAsync(int eventId);
    }
}