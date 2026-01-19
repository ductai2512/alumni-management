using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlumniManagement.BUS.Interfaces
{
    public class AlumniByYearDto
    {
        public int Year { get; set; }
        public int Count { get; set; }
    }

    public class AlumniByMajorDto
    {
        public string Major { get; set; }
        public int Count { get; set; }
    }

    public class TopCompanyDto
    {
        public string Company { get; set; }
        public int AlumniCount { get; set; }
    }

    public interface IStatisticsService
    {
        Task<IEnumerable<AlumniByYearDto>> GetAlumniByYearAsync();
        Task<IEnumerable<AlumniByMajorDto>> GetAlumniByMajorAsync();
        Task<IEnumerable<TopCompanyDto>> GetTopCompaniesAsync(int top = 10);
        Task<decimal> GetTotalDonationsAsync();
    }
}