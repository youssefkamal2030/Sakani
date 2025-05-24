using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakni.Sevices.Interfaces
{
    public  interface IAuthService
    {
        Task<(bool IsSuccess, string Message)> RegisterAsync(string email, string password, string name, string role);
        Task<(bool IsSuccess, string Message)> LoginAsync(string email, string password);
        Task<(bool IsSuccess, string Message)> ChangePasswordAsync(string email, string oldPassword, string newPassword);
        Task<(bool IsSuccess, string Message)> ResetPasswordAsync(string email, string newPassword);
        Task<(bool IsSuccess, string Message)> SendPasswordResetLinkAsync(string email);

    }
}
