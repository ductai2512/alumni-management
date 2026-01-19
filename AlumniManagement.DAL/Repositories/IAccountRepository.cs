using AlumniManagement.DAL.Entities;
using System.Threading.Tasks;

namespace AlumniManagement.DAL.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByUsernameAsync(string username);
        Task<Account> GetByAlumniIdAsync(int alumniId);
    }
}
