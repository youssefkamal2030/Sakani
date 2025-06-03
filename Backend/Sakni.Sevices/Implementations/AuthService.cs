using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Sakani.Data.Models;
using Sakani.DA.DTOs;
using Sakni.Sevices.Interfaces;
using Sakani.DA.Interfaces;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IOwnerService _ownerService;
    private readonly IStudentService _studentService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(UserManager<User> userManager, 
        SignInManager<User> signInManager, 
        ITokenService tokenService, 
        IOwnerService ownerService, 
        IStudentService studentService,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _ownerService = ownerService;
        _studentService = studentService;
        _unitOfWork = unitOfWork;

    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new AuthResponse { Success = false, Message = "User not found." };

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
                return new AuthResponse { Success = false, Message = "Invalid credentials." };

            string id = null;
            string firstName = null;
            string lastName = null;
            string phoneNumber = null;

            if (user.Role == UserRole.Student)
            {
                var student = await _studentService.GetStudentByUserIdAsync(user.Id);
                if (student != null)
                {
                    id = student.StudentId.ToString();
                    firstName = student.FirstName;
                    lastName = student.LastName;
                    phoneNumber = student.PhoneNumber;
                }
            }
            else if (user.Role == UserRole.Owner)
            {
                var owner = await _ownerService.GetOwnerByUserIdAsync(user.Id);
                if (owner != null)
                {
                    id = owner.OwnerId.ToString();
                    firstName = owner.FirstName;
                    lastName = owner.LastName;
                    phoneNumber = owner.PhoneNumber;
                }
            }

            var ResponseUser = new UserDto
            {
                FirstName = firstName,
                LastName = lastName,
                Id = id,
                Email = user.Email,
                PhoneNumber = phoneNumber,
                Role = user.Role
            };

            var token = await _tokenService.GenerateJwtToken(user);

            return new AuthResponse
            {
                Success = true,
                Token = token.ToString(),
                ExpiresAt = DateTime.UtcNow.AddHours(2),
                User = ResponseUser,
            };
        }
        catch (Exception ex)
        {
            return new AuthResponse
            {
                Success = false,
                Message = "An error occurred during login.",
                Errors = new List<string> { ex.Message }
            };
        }
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists != null)
                return new AuthResponse { Success = false, Message = "User already exists." };
            if(request.Role.ToString()!= "Student" &&  request.Role.ToString()!= "Owner")
            {
                throw new Exception("Invalid Role Type ");
            }

            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                password = request.Password,
                Role = (UserRole)Enum.Parse(typeof(UserRole), request.Role, ignoreCase:true)
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            
            if (!result.Succeeded)
                return new AuthResponse { Success = false, Message = "Registration Failed", Errors = result.Errors.Select(e => e.Description).ToList() };

            if(request.Role ==UserRole.Owner.ToString())
            {
                var Owner = new CreateOwnerDto
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserId = user.Id
                };
                await _ownerService.CreateOwnerAsync(Owner);
                await _userManager.AddToRoleAsync(user, UserRole.Owner.ToString());
                
            }
            if(request.Role == UserRole.Student.ToString())
            {
                var Student = new CreateStudentDto 
                { 
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserId = user.Id
                };
                await _studentService.CreateStudentAsync(Student);
                await _userManager.AddToRoleAsync(user,UserRole.Student.ToString());
            }
            _unitOfWork.CommitTransactionAsync();
            return new AuthResponse
            {
                Success = true,
                User = new UserDto { Id = user.Id, Email = user.Email, FirstName = user.UserName,
                    Role = (UserRole)Enum.Parse(typeof(UserRole), request.Role, ignoreCase: true)                }
            };
        }
        catch (Exception ex)
        {
            _unitOfWork.RollbackTransactionAsync();
            return new AuthResponse { Success = false, Message = "An error occurred during registration.", Errors = new List<string> { ex.Message } };
        }
    }

    public async Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return false;

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        return result.Succeeded;
    }

    public async Task<bool> ForgotPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return false;

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        Console.WriteLine($"Password reset token for {email}: {token}");

        return true;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return false;

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
        return result.Succeeded;
    }

    public async Task<bool> LogoutAsync(string userId)
    {
        return await Task.FromResult(true);
    }
}
