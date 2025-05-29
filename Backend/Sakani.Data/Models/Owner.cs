using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sakani.Data.Models
{
    [Index(nameof(UserId))]
    [Index(nameof(VerificationStatus))]
    public class Owner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OwnerId { get; set; }

        [Required]
        public string UserId { get; set; }

      

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        [StringLength(200)]
        public string? Residence { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; }

        [StringLength(50)]
        public string? Religion { get; set; }

       
        [StringLength(20)]
        [Phone]
        public string? PhoneNumber { get; set; }

        [StringLength(255)]
        public string? ProfilePhoto { get; set; }

        [StringLength(20)]
        public string VerificationStatus { get; set; } = "Not Verified";

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
} 