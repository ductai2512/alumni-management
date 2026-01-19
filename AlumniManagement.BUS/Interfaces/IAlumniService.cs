using AlumniManagement.Shared.DTOs.Alumni;
using AlumniManagement.Shared.DTOs.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlumniManagement.BUS.Interfaces
{
    public interface IAlumniService
    {
        Task<PagedResult<AlumniDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<AlumniDto> GetByIdAsync(int id);
        Task<AlumniDto> CreateAsync(CreateAlumniRequest request);
        Task<bool> UpdateAsync(int id, UpdateAlumniRequest request);
        Task<bool> DeleteAsync(int id);
        Task<PagedResult<AlumniDto>> SearchAsync(AlumniSearchRequest request);
        Task<IEnumerable<AlumniDto>> GetByGraduationYearAsync(int year);
        Task<IEnumerable<AlumniDto>> GetByMajorAsync(string major);
    }
}