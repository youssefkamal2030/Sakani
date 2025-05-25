using Microsoft.EntityFrameworkCore;
using Sakani.DA.Data;
using Sakani.DA.Interfaces;
using Sakani.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async Task<Student> AddAsync(Student entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            await _context.Students.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            return await AddAsync(student);
        }

        public async Task<int> CountAsync(Expression<Func<Student, bool>> filter = null)
        {
            return await _context.Students.CountAsync(filter ?? (x => true));
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return false;

            _context.Students.Remove(student);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            return await DeleteAsync(id);
        }

        public async Task<bool> ExistsAsync(object id)
        {
            return await _context.Students.AnyAsync(s => s.StudentId == (Guid)id);
        }

        public async Task<bool> StudentExistsAsync(Guid id)
        {
            return await ExistsAsync(id);
        }

        public async Task<IEnumerable<Student>> FindAsync(Expression<Func<Student, bool>> predicate)
        {
            return await _context.Students.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync(
            Expression<Func<Student, bool>> filter = null,
            Func<IQueryable<Student>, IOrderedQueryable<Student>> orderBy = null,
            string includeProperties = "",
            int? skip = null,
            int? take = null)
        {
            IQueryable<Student> query = _context.Students;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                query = orderBy(query);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync(int? skip = null, int? take = null)
        {
            return await GetAllAsync(null, null, "", skip, take);
        }

        public async Task<Student> GetByIdAsync(object id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> GetStudentByIdAsync(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<Student> GetStudentByUserIdAsync(string userId)
        {
            return await _context.Students
                .FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<Student> GetStudentWithDetailsAsync(Guid studentId)
        {
            return await _context.Students
                .Include(s => s.Bookings)
                .ThenInclude(b => b.Apartment)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);
        }

        public async Task<IEnumerable<Student>> GetStudentsByCollegeAsync(string collegeName, int? skip = null, int? take = null)
        {
            var query = _context.Students.Where(s => s.CollegeName == collegeName);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsByOriginAsync(string origin, int? skip = null, int? take = null)
        {
            var query = _context.Students.Where(s => s.Origin == origin);

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsWithBookingsAsync(int? skip = null, int? take = null)
        {
            var query = _context.Students
                .Include(s => s.Bookings)
                .Where(s => s.Bookings.Any());

            if (skip.HasValue)
                query = query.Skip(skip.Value);

            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public async Task<int> GetTotalStudentsCountAsync()
        {
            return await _context.Students.CountAsync();
        }

        public async Task<IEnumerable<Student>> SearchStudentsAsync(string searchTerm, int? skip = null, int? take = null)
        {
            var query = _context.Students
        .Include(s => s.User)  // Include the User entity
        .Where(s => s.FirstName.Contains(searchTerm) || 
                   s.LastName.Contains(searchTerm) || 
                   s.CollegeName.Contains(searchTerm) ||
                   s.User.Email.Contains(searchTerm) ||  // Search in User's email
                   s.User.UserName.Contains(searchTerm)); // Search in User's username

    if (skip.HasValue)
        query = query.Skip(skip.Value);

    if (take.HasValue)
        query = query.Take(take.Value);

    return await query.ToListAsync();
        }

        public async Task<bool> UpdateStudentProfileAsync(Student student)
        {
            student.UpdatedAt = DateTime.UtcNow;
            _context.Students.Update(student);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Student> UpdateAsync(Student entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Students.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Student> UpdateStudentAsync(Student student)
        {
            return await UpdateAsync(student);
        }
    }
} 