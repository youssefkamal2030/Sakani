using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakani.Contracts.Dto;


namespace Sakani.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool success, string? errorMessage)> RegisterUserAsync(RegisterDto user);
        Task<(string? token, string? errorMessage)> LoginUserAsync(LoginDto user);
    }
}
