using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sakani.Data.Models;

namespace Sakani.DA.Interfaces
{
    public interface IStudentRepository : IGenericRepository<Student>
    {
        Task<Student> GetStudentByUserIdAsync(string userId);
        Task<IEnumerable<Student>> GetStudentsByCollegeAsync(string collegeName, int? skip = null, int? take = null);
        Task<IEnumerable<Student>> GetStudentsByOriginAsync(string origin, int? skip = null, int? take = null);
        Task<IEnumerable<Student>> GetStudentsWithBookingsAsync(int? skip = null, int? take = null);
        Task<Student> GetStudentWithDetailsAsync(Guid studentId);
        Task<bool> UpdateStudentProfileAsync(Student student);
        Task<int> GetTotalStudentsCountAsync();
        Task<IEnumerable<Student>> SearchStudentsAsync(string searchTerm, int? skip = null, int? take = null);
        Task<Student> AddStudentAsync(Student student);
        Task<Student> UpdateStudentAsync(Student student);
        Task<bool> DeleteStudentAsync(Guid id);
        Task<Student> GetStudentByIdAsync(Guid id);
        Task<IEnumerable<Student>> GetAllStudentsAsync(int? skip = null, int? take = null);
        Task<bool> StudentExistsAsync(Guid id);
    }
} 