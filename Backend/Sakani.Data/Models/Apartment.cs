using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sakani.Data.Models
{
    [Index(nameof(UserId))]
    public class Apartment
    {
        [Key]
        public Guid ApartmentId { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int NumberOfRooms { get; set; }

        public int NumberOfBeds { get; set; }

        public bool IsVacant { get; set; }

        public bool IsAC { get; set; }

        public bool IsWifi { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(255)]
        public string Location { get; set; }

        [StringLength(255)]
        public string MainImage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
