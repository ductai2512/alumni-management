using AlumniManagement.Shared.DTOs.Auth;
using System.Threading.Tasks;

namespace AlumniManagement.BUS.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<bool> RegisterAsync(RegisterRequest request);
        Task<bool> ChangePasswordAsync(int accountId, ChangePasswordRequest request);
    }
}