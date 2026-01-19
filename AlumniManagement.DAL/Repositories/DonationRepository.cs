using AlumniManagement.DAL.Data;
using AlumniManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniManagement.DAL.Repositories
{
    public class DonationRepository : Repository<Donation>, IDonationRepository
    {
        public DonationRepository(AlumniDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Donation>> GetByAlumniIdAsync(int alumniId)
        {
            return await _dbSet
                .Where(d => d.AlumniId == alumniId)
                .OrderByDescending(d => d.DonationDate)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalDonationsAsync()
        {
            return await _dbSet.SumAsync(d => d.Amount);
        }

        public async Task<decimal> GetTotalDonationsByAlumniAsync(int alumniId)
        {
            return await _dbSet
                .Where(d => d.AlumniId == alumniId)
                .SumAsync(d => d.Amount);
        }

        public async Task<decimal> GetTotalDonationsByYearAsync(int year)
        {
            return await _dbSet
                .Where(d => d.DonationDate.Year == year)
                .SumAsync(d => d.Amount);
        }
    }
}