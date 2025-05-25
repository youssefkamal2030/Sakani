using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakani.DA.DTOs;

namespace Sakni.Sevices.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> GetStudentByIdAsync(Guid id);
        Task<StudentDto> GetStudentByUserIdAsync(string userId);
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync(int? skip = null, int? take = null);
        Task<IEnumerable<StudentDto>> GetStudentsByCollegeAsync(string collegeName, int? skip = null, int? take = null);
        Task<IEnumerable<StudentDto>> GetStudentsByOriginAsync(string origin, int? skip = null, int? take = null);
        Task<IEnumerable<StudentDto>> GetStudentsWithBookingsAsync(int? skip = null, int? take = null);
        Task<StudentDto> CreateStudentAsync(CreateStudentDto request);
        Task<StudentDto> UpdateStudentAsync(Guid id, UpdateStudentDto request);
        Task<bool> DeleteStudentAsync(Guid id);
        Task<IEnumerable<StudentDto>> SearchStudentsAsync(string searchTerm, int? skip = null, int? take = null);
        Task<int> GetTotalStudentsCountAsync();
    }
}
