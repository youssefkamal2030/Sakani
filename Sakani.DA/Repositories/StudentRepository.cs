using Microsoft.EntityFrameworkCore;
using Sakani.DA.Data;
using Sakani.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakani.DA.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly SakaniDbContext _context;

        public StudentRepository(SakaniDbContext context)
        {
            _context = context;
        }

        public async Task<Student> AddStudent(Student student)
        {
            student.CreatedAt = DateTime.UtcNow;
            student.UpdatedAt = DateTime.UtcNow;
            
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return false;

            _context.Students.Remove(student);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> GetStudentByUserId(string userId)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<IEnumerable<Student>> GetStudentsByCollege(string collegeName)
        {
            return await _context.Students
                .Where(s => s.CollegeName == collegeName)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsByOrigin(string origin)
        {
            return await _context.Students
                .Where(s => s.Origin == origin)
                .ToListAsync();
        }

        public async Task<bool> StudentExists(int id)
        {
            return await _context.Students.AnyAsync(s => s.StudentId == id);
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            student.UpdatedAt = DateTime.UtcNow;
            
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
            return student;
        }
    }
} 