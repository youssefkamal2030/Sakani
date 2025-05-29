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

            // User -> Student (Cascade - when user is deleted, student profile is deleted)
            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // User -> Owner (Cascade - when user is deleted, owner profile is deleted)
            modelBuilder.Entity<Owner>()
                .HasOne(o => o.User)
                .WithOne(u => u.Owner)
                .HasForeignKey<Owner>(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Owner -> Apartment (Cascade - when owner is deleted, their apartments are deleted)
            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.Owner)
                .WithMany(o => o.Apartments)
                .HasForeignKey(a => a.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Apartment -> Room (Cascade - when apartment is deleted, all its rooms are deleted)
            modelBuilder.Entity<Room>()
                .HasOne(r => r.Apartment)
                .WithMany(a => a.Rooms)
                .HasForeignKey(r => r.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Room -> Bed (Cascade - when room is deleted, all its beds are deleted)
            modelBuilder.Entity<Bed>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Beds)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // Student -> Bed (NoAction - prevent multiple cascade paths)
            modelBuilder.Entity<Bed>()
                .HasOne(b => b.Student)
                .WithMany()
                .HasForeignKey(b => b.studentId)
                .OnDelete(DeleteBehavior.NoAction);

            // Bed -> Booking (NoAction - prevent multiple cascade paths)
            modelBuilder.Entity<Bed>()
                .HasMany(b => b.Bookings)
                .WithOne(b => b.Bed)
                .HasForeignKey(b => b.BedId)
                .OnDelete(DeleteBehavior.NoAction);

            // Student -> Booking (Cascade - when student is deleted, their bookings are deleted)
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Student)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.stuendtId)
                .OnDelete(DeleteBehavior.NoAction);

            // Apartment -> Feedback (NoAction - prevent cascade cycle with User)
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Apartment)
                .WithMany(a => a.Feedbacks)
                .HasForeignKey(f => f.ApartmentId)
                .OnDelete(DeleteBehavior.NoAction);

            // User -> Feedback (Cascade - when user is deleted, their feedback is deleted)
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Add indexes for performance
            modelBuilder.Entity<Owner>()
                .HasIndex(o => o.VerificationStatus);

            modelBuilder.Entity<Booking>()
                .HasIndex(b => b.Status);

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.CollegeName);

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.Origin);
        }
    }
}
