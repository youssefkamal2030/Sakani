using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Sakani.Data.Models;
using Sakani.DA.DTOs;
using Sakni.Sevices.Interfaces;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return new AuthResponse { Success = false, Message = "User not found." };

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!result.Succeeded)
            return new AuthResponse { Success = false, Message = "Invalid credentials." };

        var token = _tokenService.GenerateJwtToken(user);

        return new AuthResponse
        {
            Success = true,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            User = new UserDto { Id = user.Id, Email = user.Email, Name = user.UserName }
        };
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var userExists = await _userManager.FindByEmailAsync(request.Email);
        if (userExists != null)
            return new AuthResponse { Success = false, Message = "User already exists." };

        var user = new User
        {
            UserName = request.UserName,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            return new AuthResponse { Success = false, Message = string.Join(", ", result.Errors) };

        var token = _tokenService.GenerateJwtToken(user);

        return new AuthResponse
        {
            Success = true,
            Token = token,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            User = new UserDto { Id = user.Id, Email = user.Email, Name = user.UserName }
        };
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
