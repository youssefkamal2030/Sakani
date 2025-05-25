using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sakani.DA;
using Sakani.DA.Data;
using Sakani.DA.Interfaces;
using Sakani.DA.Mapping;
using Sakani.DA.Repositories;
using Sakani.Data.Models;
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

            // Register repositories
            builder.Services.AddScoped<IStudentRepository,StudentRepository>();
            builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
            builder.Services.AddScoped<IApartmentRepository, ApartmentRepository>();
            builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<IBedRepository, BedRepository>();
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
