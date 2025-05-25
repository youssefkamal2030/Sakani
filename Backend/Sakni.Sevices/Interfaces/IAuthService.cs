using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakani.DA.DTOs;

namespace Sakni.Sevices.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordRequest request);
        Task<bool> LogoutAsync(string userId);
    }
}
