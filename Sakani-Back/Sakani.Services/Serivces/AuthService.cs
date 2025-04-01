using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sakani.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Sakani.Contracts.Dto;
using Sakani.Data.Models;
using Sakani.Application.Interfaces;
using AutoMapper;

namespace Sakani.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _jwtTokenService;
        private readonly IMapper _mapper;



        public AuthService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<(string? token, string? errorMessage)> LoginUserAsync(LoginDto user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser == null)
            {
                return (null, "Email not found");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(
                existingUser.Email,
                user.password,
                isPersistent: false,
                lockoutOnFailure: false
            );

            if (!signInResult.Succeeded)
            {
                if (signInResult.IsLockedOut) return (null, "User Email is locked out");
                if (signInResult.IsNotAllowed) return (null, "Login not allowed");
                return (null, "Incorrect password");
            }

            var token = _jwtTokenService.GenerateToken(existingUser);
            return (token, null);
        }

        public async Task<(bool success, string? errorMessage)> RegisterUserAsync(RegisterDto registerDto)
        {
            var existingEmail = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingEmail != null)
            {
                return (false, "Email already in use");
            }

            var user = _mapper.Map<User>(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.password);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
                return (false, errorMessage);
            }

            return (true, null);
        }
    }
}
