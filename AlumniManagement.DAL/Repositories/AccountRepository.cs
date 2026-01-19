using AlumniManagement.DAL.Data;
using AlumniManagement.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AlumniManagement.DAL.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(AlumniDbContext context) : base(context)
        {
        }

        public async Task<Account> GetByUsernameAsync(string username)
        {
           return await _context.Accounts
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Username == username);
        }

        public async Task<Account> GetByAlumniIdAsync(int alumniId)
        {
            return await _dbSet
                .Include(a => a.Alumni)
                .FirstOrDefaultAsync(a => a.AlumniId == alumniId);
        }
    }
}