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
    public class Feedback
    {
        [Key]
        public Guid FeedbackId { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Required]
        public Guid ApartmentId { get; set; }

        [ForeignKey("ApartmentId")]
        public virtual Apartment Apartment { get; set; }

        [Range(1, 5)]
        public int? Rate { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
