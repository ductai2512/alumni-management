using AlumniManagement.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlumniManagement.DAL.Repositories
{
    public interface IAlumniEventRepository : IRepository<AlumniEvent>
    {
        Task<AlumniEvent> GetRegistrationAsync(int alumniId, int eventId);
        Task<IEnumerable<Event>> GetAlumniEventsAsync(int alumniId);
        Task<bool> IsRegisteredAsync(int alumniId, int eventId);
    }
}
