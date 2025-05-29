using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sakani.DA.Data;
using Sakani.Data.Models;

namespace Sakani.DA.Dbinitializer
{
    public class Dbinitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SakaniDbContext _db;
        private readonly ILogger<Dbinitializer> _logger;

        public Dbinitializer(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            SakaniDbContext db,
            ILogger<Dbinitializer> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    await _db.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in migration");
            }

            if (!await _roleManager.RoleExistsAsync(UserRole.Student.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString()));
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Owner.ToString()));
                await _roleManager.CreateAsync(new IdentityRole(UserRole.Student.ToString()));

                var adminUser = new User
                {
                    UserName = "SakaniTeam",
                    Email = "SakaniTeam@gmail.com",
                    password = "123456789k",
                    PhoneNumber = "+201551134280",
                };

                var result = await _userManager.CreateAsync(adminUser, "123456789k");
                if (!result.Succeeded)
                {
                    _logger.LogError("Failed to create admin user: {Errors}", 
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                    return;
                }

                var user = await _db.Users.FirstOrDefaultAsync(p => p.Email == "SakaniTeam@gmail.com");
                if (user != null)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());
                    if (!roleResult.Succeeded)
                    {
                        _logger.LogError("Failed to assign admin role: {Errors}", 
                            string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                    }
                }
            }
        }
    }
}
