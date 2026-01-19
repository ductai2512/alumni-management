using AlumniManagement.DAL.Data;
using AlumniManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniManagement.DAL.Repositories
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(AlumniDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Event>> GetUpcomingEventsAsync()
        {
            return await _dbSet
                .Where(e => e.EventDate >= DateTime.Now && !e.IsCanceled)
                .OrderBy(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _dbSet
                .Where(e => e.EventDate >= startDate && e.EventDate <= endDate && !e.IsCanceled)
                .OrderBy(e => e.EventDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alumni>> GetEventParticipantsAsync(int eventId)
        {
            return await _context.AlumniEvents
                .Where(ae => ae.EventId == eventId)
                .Include(ae => ae.Alumni)
                .Select(ae => ae.Alumni)
                .ToListAsync();
        }
    }
}
