using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakani.Data.Models
{
    public class UserProfile
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Key, ForeignKey("User")]

        public string UserId { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(100)]
        public string College { get; set; }

        [StringLength(15)]
        public string PhoneNo { get; set; }

        [StringLength(200)]
        public string Image { get; set; }

        [StringLength(50)]
        public string Gender { get; set; }

        [StringLength(50)]
        public string Religion { get; set; }

        public virtual User User { get; set; }
    }
}
