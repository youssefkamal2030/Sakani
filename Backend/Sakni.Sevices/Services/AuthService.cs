using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakani.DA.DTOs;
using Sakni.Sevices.Interfaces;

namespace Sakni.Sevices.Services
{
    public class AuthService : IAuthService
    {
        public Task<bool> ChangePasswordAsync(string userId, ChangePasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ForgotPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> LogoutAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
