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
    [Index(nameof(OwnerId))]
    public class Apartment
    {
        [Key]
        public Guid ApartmentId { get; set; }

        [Required]
        [StringLength(150)]
        public string ApartmentTitle { get; set; }

        public int NumberOfRooms { get; set; }

        public int NumberOfBeds { get; set; }

        public bool? IsWifi { get; set; }

        [Required]
        public Guid? OwnerId { get; set; }

   

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(255)]
        public string Location { get; set; }

        public string? MainImage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

        public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
        [ForeignKey("OwnerId")]
        public virtual Owner Owner { get; set; }

    }
}
