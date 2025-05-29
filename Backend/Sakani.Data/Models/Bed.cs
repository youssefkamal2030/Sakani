using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakani.Data.Models
{
    public class Bed
    {   
        public Guid Id { get; set; }
        public int RoomId { get; set; }
        public Guid? studentId { get; set; } 

        [Column(TypeName = "decimal(18,2)")] 
        public decimal Price { get; set; }
        public bool IsAC { get; set; }
        public bool IsVacant { get; set; }
        

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        [ForeignKey("studentId")]
        public virtual Student Student { get; set; }

    }
}
