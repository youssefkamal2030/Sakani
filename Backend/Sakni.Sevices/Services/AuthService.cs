using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakni.Sevices.Interfaces;

namespace Sakni.Sevices.Services
{
    public class AuthService : IAuthService
    {
        public Task<(bool IsSuccess, string Message)> ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, string Message)> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, string Message)> RegisterAsync(string email, string password, string name, string role)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, string Message)> ResetPasswordAsync(string email, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<(bool IsSuccess, string Message)> SendPasswordResetLinkAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
