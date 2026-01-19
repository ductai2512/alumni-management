using AlumniManagement.BUS.Interfaces;
using AlumniManagement.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniManagement.BUS.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly AlumniDbContext _context;

        public StatisticsService(AlumniDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlumniByYearDto>> GetAlumniByYearAsync()
        {
            return await _context.Alumni
                .Where(a => a.IsActive)
                .GroupBy(a => a.GraduationYear)
                .Select(g => new AlumniByYearDto
                {
                    Year = g.Key,
                    Count = g.Count()
                })
                .OrderBy(x => x.Year)
                .ToListAsync();
        }

        public async Task<IEnumerable<AlumniByMajorDto>> GetAlumniByMajorAsync()
        {
            return await _context.Alumni
                .Where(a => a.IsActive && !string.IsNullOrEmpty(a.Major))
                .GroupBy(a => a.Major)
                .Select(g => new AlumniByMajorDto
                {
                    Major = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(x => x.Count)
                .ToListAsync();
        }

        public async Task<IEnumerable<TopCompanyDto>> GetTopCompaniesAsync(int top = 10)
        {
            return await _context.Alumni
                .Where(a => a.IsActive && !string.IsNullOrEmpty(a.Company))
                .GroupBy(a => a.Company)
                .Select(g => new TopCompanyDto
                {
                    Company = g.Key,
                    AlumniCount = g.Count()
                })
                .OrderByDescending(x => x.AlumniCount)
                .Take(top)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalDonationsAsync()
        {
            var total = await _context.Donations.SumAsync(d => (decimal?)d.Amount);
            return total ?? 0;
        }
    }
}