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
    [Index(nameof(ApartmentId))]
    [Index(nameof(Status))]
    public class Booking
    {
        [Key]
        public Guid BookingId { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public Guid ApartmentId { get; set; }

        [ForeignKey("ApartmentId")]
        public virtual Apartment Apartment { get; set; }

        [Required]
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending";

        public bool Confirmed { get; set; }

        public bool Canceled { get; set; }

        public decimal TotalPrice { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
