using AlumniManagement.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlumniManagement.DAL.Repositories
{
    public interface IAlumniRepository : IRepository<Alumni>
    {
        Task<Alumni> GetByStudentCodeAsync(string studentCode);
        Task<Alumni> GetByEmailAsync(string email);
        Task<IEnumerable<Alumni>> GetByGraduationYearAsync(int year);
        Task<IEnumerable<Alumni>> GetByMajorAsync(string major);
        Task<IEnumerable<Alumni>> SearchAsync(string keyword, int? graduationYear, string major, int pageNumber, int pageSize);
        Task<int> GetSearchCountAsync(string keyword, int? graduationYear, string major);
    }
}
