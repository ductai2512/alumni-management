using AlumniManagement.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlumniManagement.DAL.Repositories
{
    public interface IDonationRepository : IRepository<Donation>
    {
        Task<IEnumerable<Donation>> GetByAlumniIdAsync(int alumniId);
        Task<decimal> GetTotalDonationsAsync();
        Task<decimal> GetTotalDonationsByAlumniAsync(int alumniId);
        Task<decimal> GetTotalDonationsByYearAsync(int year);
    }
}
