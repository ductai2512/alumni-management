using AlumniManagement.DAL.Entities;

namespace AlumniManagement.BUS.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Account account);
    }
}