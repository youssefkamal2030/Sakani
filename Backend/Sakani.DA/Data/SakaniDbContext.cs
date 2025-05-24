using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sakani.Data.Models;

namespace Sakani.DA.Data
{
    public class SakaniDbContext : IdentityDbContext<User>
    {
        public SakaniDbContext(DbContextOptions<SakaniDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1) User <-> UserProfile (1:1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserProfile)
                .WithOne(up => up.User)
                .HasForeignKey<UserProfile>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 2) User <-> Student (1:1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 3) User <-> Owner (1:1)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Owner)
                .WithOne(o => o.User)
                .HasForeignKey<Owner>(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 4) User <-> Apartments (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Apartments)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // 5) User <-> Bookings (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // 6) User <-> Feedback (1:N)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Feedbacks)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // 7) Apartment <-> Bookings (1:N)
            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Bookings)
                .WithOne(b => b.Apartment)
                .HasForeignKey(b => b.ApartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // 8) Apartment <-> Feedback (1:N)
            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Feedbacks)
                .WithOne(f => f.Apartment)
                .HasForeignKey(f => f.ApartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure enum conversions
            modelBuilder.Entity<Student>()
                .Property(s => s.Gender)
                .HasMaxLength(10)
                .HasConversion<string>();

            modelBuilder.Entity<Owner>()
                .Property(o => o.Gender)
                .HasMaxLength(10)
                .HasConversion<string>();

            modelBuilder.Entity<Owner>()
                .Property(o => o.VerificationStatus)
                .HasMaxLength(20)
                .HasConversion<string>();

            // Additional indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.NormalizedEmail)
                .HasDatabaseName("EmailIndex");

            modelBuilder.Entity<User>()
                .HasIndex(u => u.NormalizedUserName)
                .HasDatabaseName("UserNameIndex")
                .IsUnique();

            // Set up auto-include for related entities
            modelBuilder.Entity<Apartment>()
                .Navigation(a => a.User)
                .AutoInclude();

            modelBuilder.Entity<Booking>()
                .Navigation(b => b.User)
                .AutoInclude();

            modelBuilder.Entity<Booking>()
                .Navigation(b => b.Apartment)
                .AutoInclude();

            modelBuilder.Entity<Feedback>()
                .Navigation(f => f.User)
                .AutoInclude();

            modelBuilder.Entity<Feedback>()
                .Navigation(f => f.Apartment)
                .AutoInclude();
        }
    }
}
