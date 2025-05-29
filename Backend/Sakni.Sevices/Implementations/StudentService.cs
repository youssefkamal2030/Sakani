using AutoMapper;
using Sakni.Sevices.Interfaces;
using Sakani.DA.DTOs;
using Sakani.DA.UnitOfWork;
using Sakani.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sakani.DA.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Sakni.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public StudentService(IUnitOfWork unitOfWork, IMapper mapper,UserManager<User> usermanager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = usermanager;
        }

        public async Task<StudentDto> GetStudentByIdAsync(Guid id)
        {
            var student = await _unitOfWork.Students.GetStudentByIdAsync(id);
            return student == null ? null : _mapper.Map<StudentDto>(student);
        }

        public async Task<StudentDto> GetStudentByUserIdAsync(string userId)
        {
            var student = await _unitOfWork.Students.GetStudentByUserIdAsync(userId);
            return student == null ? null : _mapper.Map<StudentDto>(student);
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync(int? skip = null, int? take = null)
        {
            var students = await _unitOfWork.Students.GetAllStudentsAsync(skip, take);
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsByCollegeAsync(string collegeName, int? skip = null, int? take = null)
        {
            var students = await _unitOfWork.Students.GetStudentsByCollegeAsync(collegeName, skip, take);
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsByOriginAsync(string origin, int? skip = null, int? take = null)
        {
            var students = await _unitOfWork.Students.GetStudentsByOriginAsync(origin, skip, take);
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<IEnumerable<StudentDto>> GetStudentsWithBookingsAsync(int? skip = null, int? take = null)
        {
            var students = await _unitOfWork.Students.GetStudentsWithBookingsAsync(skip, take);
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> CreateStudentAsync(CreateStudentDto request)
        {
            var student = _mapper.Map<Student>(request);
            student.StudentId = Guid.NewGuid();
            student.CreatedAt = DateTime.UtcNow;
            student.UpdatedAt = DateTime.UtcNow;

            var created = await _unitOfWork.Students.AddStudentAsync(student);
            return _mapper.Map<StudentDto>(created);
        }

        public async Task<StudentDto> UpdateStudentAsync(Guid id, UpdateStudentDto request)
        {
            var existing = await _unitOfWork.Students.GetStudentByIdAsync(id);
            if (existing == null) return null;

            _mapper.Map(request, existing);
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await _unitOfWork.Students.UpdateStudentAsync(existing);
            var user = await _userManager.FindByIdAsync(existing.UserId);
            if (user != null)
            {
                user.UserName = request.Fullname;
                user.Email = request.email;
                await _userManager.UpdateAsync(user);
            }
            return _mapper.Map<StudentDto>(updated);
        }

        public async Task<bool> DeleteStudentAsync(Guid id)
        {
            try
            {
                var student = await _unitOfWork.Students.GetByIdAsync(id);
                var user = await _userManager.FindByIdAsync(student.UserId);
                await _userManager.DeleteAsync(user);
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
                return false;
            }
            
        }

        public async Task<IEnumerable<StudentDto>> SearchStudentsAsync(string searchTerm, int? skip = null, int? take = null)
        {
            var students = await _unitOfWork.Students.SearchStudentsAsync(searchTerm, skip, take);
            return _mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<int> GetTotalStudentsCountAsync()
        {
            return await _unitOfWork.Students.GetTotalStudentsCountAsync();
        }
    }
}
