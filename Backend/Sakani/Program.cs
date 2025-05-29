using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Sakani.DA;
using Sakani.DA.Data;
using Sakani.DA.Dbinitializer;
using Sakani.DA.Interfaces;
using Sakani.DA.Mapping;
using Sakani.DA.Repositories;
using Sakani.DA.UnitOfWork;
using Sakani.Data.Models;
using Sakni.Services;
using Sakni.Services.Implementations;
using Sakni.Sevices.Interfaces;
namespace Sakani
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.AddConsole(); //---> debuge to the console
            builder.Logging.AddDebug(); //--> debuge to visual studio

            // Add services to the container.
            builder.Services.AddDbContext<SakaniDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddControllers();

            //Identity
            builder.Services.AddIdentity<User, IdentityRole>(options => {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<SakaniDbContext>()
                .AddDefaultTokenProviders();
            //automapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));


            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero // Removes default 5-minute tolerance
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("OwnerOnly", policy => policy.RequireRole("Owner"));
                options.AddPolicy("StudentOnly", policy => policy.RequireRole("Student"));
                options.AddPolicy("OwnerOrAdmin", policy => policy.RequireRole("Owner", "Admin"));
            });           
            
            //CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });
            
            // Register repositories
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IStudentRepository,StudentRepository>();
            builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
            builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
            builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<IBedRepository, BedRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();

            //Services
            builder.Services.AddScoped<Dbinitializer>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IOwnerService, OwnerService>();
            builder.Services.AddScoped<IApartmentService, ApartmentService>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<IBedService, BedService>();
            builder.Services.AddScoped<IBookingService, BookingService>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Sakani API",
                    Version = "v1",
                    Description = "Sakani Student Housing API"
                });

                // Add JWT Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<Dbinitializer>();
                await dbInitializer.InitializeAsync();
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
         
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAllOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
      
    }
}
