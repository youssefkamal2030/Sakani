using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sakani.Data.Models;

namespace Sakani.DA
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> GetStudentById(int id);
        Task<Student> GetStudentByUserId(string userId);
        Task<Student> AddStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        Task<bool> DeleteStudent(int id);
        Task<bool> StudentExists(int id);
        Task<IEnumerable<Student>> GetStudentsByCollege(string collegeName);
        Task<IEnumerable<Student>> GetStudentsByOrigin(string origin);
    }
} 