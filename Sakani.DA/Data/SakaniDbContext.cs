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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1) User <-> UserProfile (1:1)
            // Typically, a profile is just an extension of User, so we can safely cascade.
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserProfile)
                .WithOne(up => up.User)
                .HasForeignKey<UserProfile>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // 2) User <-> Apartments (1:N)
            // Restrict deleting a User if they still have Apartments. 
            // This prevents accidental data loss and multiple cascade paths.
            modelBuilder.Entity<User>()
                .HasMany(u => u.Apartments)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // 3) User <-> Bookings (1:N)
            // Restrict deleting a User if they still have Bookings.
            modelBuilder.Entity<User>()
                .HasMany(u => u.Bookings)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // 4) User <-> Feedback (1:N)
            // Restrict deleting a User if they still have Feedback.
            modelBuilder.Entity<User>()
                .HasMany(u => u.Feedbacks)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // 5) Apartment <-> Bookings (1:N)
            // Restrict deleting an Apartment if there are Bookings referencing it.
            // This is common so you don't accidentally wipe out booking history.
            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Bookings)
                .WithOne(b => b.Apartment)
                .HasForeignKey(b => b.ApartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // 6) Apartment <-> Feedback (1:N)
            // Restrict deleting an Apartment if there is Feedback referencing it.
            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Feedbacks)
                .WithOne(f => f.Apartment)
                .HasForeignKey(f => f.ApartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Example Index on Email (unique constraint)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Additional configuration or seed data as needed...
        }

    }
}
