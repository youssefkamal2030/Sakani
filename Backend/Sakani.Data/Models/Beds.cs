using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakani.Data.Models
{
    public class Beds
    {   
        public int Id { get; set; }
        public int RoomId { get; set; }
        public decimal Price { get; set; }
        public bool IsAC { get; set; }
        public bool IsVacant { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("RoomId")]
        public virtual Rooms Room { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
