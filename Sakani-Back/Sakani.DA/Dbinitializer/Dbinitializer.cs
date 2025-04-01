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
    public class Dbinitializer : IDbinitializer
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

        public void Initialize()
        {

            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in migration");
            }

            if (!_roleManager.RoleExistsAsync(UserRole.Customer.ToString()).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString())).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(UserRole.Owner.ToString())).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(UserRole.Customer.ToString())).GetAwaiter().GetResult();

                var adminUser = new User
                {
                    UserName = "SakaniTeam",
                    Email = "SakaniTeam@gmail.com",
                    password = "123456789k",
                    PhoneNumber = "+201551134280",
                };
                _userManager.CreateAsync(adminUser, "123456789k").GetAwaiter().GetResult();

                User user = _db.Users.FirstOrDefault(p => p.Email == "SakaniTeam@gmail.com");
                if (user != null)
                {
                    _userManager.AddToRoleAsync(user, UserRole.Admin.ToString()).GetAwaiter().GetResult();
                }
                else
                {
                    _logger.LogError($"User with email {adminUser.Email} not found.");
                }
            }

        }
    }
}
