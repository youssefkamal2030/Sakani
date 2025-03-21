using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
public enum UserRole
{
    Admin,
    Owner,
    Customer 
}
namespace Sakani.Data.Models
{
    public class User : IdentityUser
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]

        public string password { get;set; }

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; }

        public string? PasswordHash { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
