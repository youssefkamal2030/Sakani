using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sakani.Application.Interfaces;
using Sakani.DA.Data;
using Sakani.DA.Dbinitializer;
using Sakani.Data.Models;
using Sakani.Services.Interfaces;
using Sakani.Services.Profiles;
using Sakani.Services.Services;
namespace Sakani
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<SakaniDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<SakaniDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Custom services
            builder.Services.AddScoped<IDbinitializer, Dbinitializer>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ILogger, Logger<Program>>();
            builder.Services.AddAutoMapper(typeof(ApplicationUserMapping));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            seedDb();

            app.MapControllers();

            app.Run();

            void seedDb()
            {
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var dbInit = services.GetRequiredService<IDbinitializer>();
                    var logger = services.GetRequiredService<ILogger<Program>>();

                    try
                    {
                        dbInit.Initialize();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred while seeding the database.");
                    }
                }
            }
        }
    }
}
