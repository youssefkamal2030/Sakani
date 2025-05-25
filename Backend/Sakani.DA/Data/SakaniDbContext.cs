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
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Bed> Beds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User-Student cascade delete
            modelBuilder.Entity<User>()
                .HasOne(u => u.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User-Owner cascade delete
            modelBuilder.Entity<User>()
                .HasOne(u => u.Owner)
                .WithOne(o => o.User)
                .HasForeignKey<Owner>(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Owner-Apartment cascade delete
            modelBuilder.Entity<Owner>()
                .HasMany(o => o.Apartments)
                .WithOne(a => a.Owner)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Apartment-Room cascade delete
            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Rooms)
                .WithOne(r => r.Apartment)
                .HasForeignKey(r => r.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Room-Bed cascade delete
            modelBuilder.Entity<Room>()
                .HasMany(r => r.Beds)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Student-Booking relationship (changed to NoAction)
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Bookings)
                .WithOne(b => b.Student)
                .HasForeignKey(b => b.stuendtId)
                .OnDelete(DeleteBehavior.NoAction);

            // Apartment-Booking relationship (NoAction)
            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Bookings)
                .WithOne(b => b.Apartment)
                .HasForeignKey(b => b.ApartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Apartment-Feedback relationship (NoAction)
            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Feedbacks)
                .WithOne(f => f.Apartment)
                .HasForeignKey(f => f.ApartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Feedback-User relationship (NoAction)
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Bed-Student relationship (NoAction)
            modelBuilder.Entity<Bed>()
                .HasOne(b => b.Student)
                .WithMany()
                .HasForeignKey(b => b.studentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure indexes
            modelBuilder.Entity<Apartment>()
                .HasIndex(a => a.OwnerId);

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.UserId);

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.CollegeName);

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Origin);

            modelBuilder.Entity<Owner>()
                .HasIndex(o => o.UserId);

            modelBuilder.Entity<Owner>()
                .HasIndex(o => o.VerificationStatus);

            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.stuendtId);

            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.ApartmentId);

            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.Status);

            modelBuilder.Entity<Feedback>()
                .HasIndex(f => f.UserId);

            modelBuilder.Entity<Feedback>()
                .HasIndex(f => f.ApartmentId);
        }
    }
}
