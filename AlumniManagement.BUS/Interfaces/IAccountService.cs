using System.Threading.Tasks;

namespace AlumniManagement.BUS.Interfaces
{
    public interface IAccountService
    {
        Task<bool> LockAccountAsync(int accountId);
        Task<bool> UnlockAccountAsync(int accountId);
    }
}