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
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return new AuthResponse { Success = false, Message = "User not found." };

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            return new AuthResponse { Success = false, Message = "Invalid credentials." };

        var token = await _tokenService.GenerateJwtToken(user);

        return new AuthResponse
        {
            Success = true,
            Token = token.ToString(),
            ExpiresAt = DateTime.UtcNow.AddHours(2),
            User = null
        };
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var userExists = await _userManager.FindByEmailAsync(request.Email);
            if (userExists != null)
                return new AuthResponse { Success = false, Message = "User already exists." };

            var user = new User
            {
                UserName = request.UserName,
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
                User = new UserDto { Id = user.Id, Email = user.Email, Name = user.UserName,
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
