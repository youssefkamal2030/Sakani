using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakani.Data.Models
{
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
        public DateTime BookingDate { get; set; }

        [Required]
        public DateTime FromDate { get; set; }

        [Required]
        public DateTime ToDate { get; set; }

        [StringLength(50)]
        public string Status { get; set; } 

        public bool Confirmed { get; set; }

        public bool Canceled { get; set; }
    }
}
