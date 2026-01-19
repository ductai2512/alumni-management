using AlumniManagement.DAL.Data;
using AlumniManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniManagement.DAL.Repositories
{
    public class AlumniEventRepository : IAlumniEventRepository
    {
        private readonly AlumniDbContext _context;

        public AlumniEventRepository(AlumniDbContext context)
        {
            _context = context;
        }

        public async Task<AlumniEvent> GetRegistrationAsync(int alumniId, int eventId)
        {
            return await _context.AlumniEvents
                .FirstOrDefaultAsync(ae => ae.AlumniId == alumniId && ae.EventId == eventId);
        }

        public async Task<IEnumerable<Event>> GetAlumniEventsAsync(int alumniId)
        {
            return await _context.AlumniEvents
                .Where(ae => ae.AlumniId == alumniId)
                .Include(ae => ae.Event)
                .Select(ae => ae.Event)
                .ToListAsync();
        }

        public async Task<bool> IsRegisteredAsync(int alumniId, int eventId)
        {
            return await _context.AlumniEvents
                .AnyAsync(ae => ae.AlumniId == alumniId && ae.EventId == eventId);
        }

        public async Task<AlumniEvent> AddAsync(AlumniEvent entity)
        {
            await _context.AlumniEvents.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(AlumniEvent entity)
        {
            _context.AlumniEvents.Remove(entity);
            await _context.SaveChangesAsync();
        }

        // Implement other IRepository methods
        public async Task<IEnumerable<AlumniEvent>> GetAllAsync()
        {
            return await _context.AlumniEvents.ToListAsync();
        }

        public Task<AlumniEvent> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<AlumniEvent>> FindAsync(System.Linq.Expressions.Expression<System.Func<AlumniEvent, bool>> predicate)
        {
            return await _context.AlumniEvents.Where(predicate).ToListAsync();
        }

        public Task UpdateAsync(AlumniEvent entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<int> CountAsync(System.Linq.Expressions.Expression<System.Func<AlumniEvent, bool>> predicate = null)
        {
            if (predicate == null)
                return await _context.AlumniEvents.CountAsync();
            return await _context.AlumniEvents.CountAsync(predicate);
        }
    }
}
