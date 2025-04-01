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
        public string password { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public virtual UserProfile UserProfile { get; set; }
        public virtual ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
