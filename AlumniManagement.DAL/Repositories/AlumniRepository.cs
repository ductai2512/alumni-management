using AlumniManagement.DAL.Data;
using AlumniManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlumniManagement.DAL.Repositories
{
    public class AlumniRepository : Repository<Alumni>, IAlumniRepository
    {
        public AlumniRepository(AlumniDbContext context) : base(context)
        {
        }

        public async Task<Alumni> GetByStudentCodeAsync(string studentCode)
        {
            return await _dbSet
                .Include(a => a.Account)
                .FirstOrDefaultAsync(a => a.StudentCode == studentCode);
        }

        public async Task<Alumni> GetByEmailAsync(string email)
        {
            return await _dbSet
                .Include(a => a.Account)
                .FirstOrDefaultAsync(a => a.Email == email);
        }

        public async Task<IEnumerable<Alumni>> GetByGraduationYearAsync(int year)
        {
            return await _dbSet
                .Where(a => a.GraduationYear == year && a.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alumni>> GetByMajorAsync(string major)
        {
            return await _dbSet
                .Where(a => a.Major == major && a.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<Alumni>> SearchAsync(string keyword, int? graduationYear, string major, int pageNumber, int pageSize)
        {
            var query = _dbSet.Where(a => a.IsActive);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.ToLower();
                query = query.Where(a => 
                    a.FullName.ToLower().Contains(keyword) ||
                    a.StudentCode.ToLower().Contains(keyword) ||
                    a.Email.ToLower().Contains(keyword));
            }

            if (graduationYear.HasValue)
            {
                query = query.Where(a => a.GraduationYear == graduationYear.Value);
            }

            if (!string.IsNullOrWhiteSpace(major))
            {
                query = query.Where(a => a.Major == major);
            }

            return await query
                .OrderByDescending(a => a.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetSearchCountAsync(string keyword, int? graduationYear, string major)
        {
            var query = _dbSet.Where(a => a.IsActive);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.ToLower();
                query = query.Where(a => 
                    a.FullName.ToLower().Contains(keyword) ||
                    a.StudentCode.ToLower().Contains(keyword) ||
                    a.Email.ToLower().Contains(keyword));
            }

            if (graduationYear.HasValue)
            {
                query = query.Where(a => a.GraduationYear == graduationYear.Value);
            }

            if (!string.IsNullOrWhiteSpace(major))
            {
                query = query.Where(a => a.Major == major);
            }

            return await query.CountAsync();
        }
    }
}