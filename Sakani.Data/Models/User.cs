using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Sakani.Data.Models
{
    public enum UserRole
    {
        Admin,
        Owner,
        Student,
        Customer 
    }

    public class User : IdentityUser
    {
        public User()
        {
            CreatedOn = DateTime.UtcNow;
            Apartments = new List<Apartment>();
            Bookings = new List<Booking>();
            Feedbacks = new List<Feedback>();
        }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public UserRole Role { get; set; }

        
        public virtual Student Student { get; set; }
        
        public virtual Owner Owner { get; set; }

        public virtual ICollection<Apartment> Apartments { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
