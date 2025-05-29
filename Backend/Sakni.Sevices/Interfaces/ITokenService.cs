using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Sakani.Data.Models;

namespace Sakni.Sevices.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtToken(User user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<bool> ValidateTokenAsync(string token);
    
    }
}
