using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sakani.Data.Models
{
    [Index(nameof(UserId))]
    [Index(nameof(CollegeName))]
    [Index(nameof(Origin))]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid StudentId { get; set; }

        [Required]
        public string UserId { get; set; }


        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [Required]
        [StringLength(100)]
        public string CollegeName { get; set; }

        
        public int Age { get; set; }

        [StringLength(100)]
        public string Origin { get; set; }

        [StringLength(255)]
        public string? ProfilePhoto { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
} 