using AlumniManagement.Shared.DTOs.Donation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlumniManagement.BUS.Interfaces
{
    public interface IDonationService
    {
        Task<DonationDto> CreateDonationAsync(CreateDonationRequest request, int alumniId);
        Task<IEnumerable<DonationDto>> GetAllDonationsAsync();
        Task<IEnumerable<DonationDto>> GetDonationsByAlumniAsync(int alumniId);
        Task<decimal> GetTotalDonationsAsync();
        Task<decimal> GetTotalDonationsByYearAsync(int year);
    }
}
