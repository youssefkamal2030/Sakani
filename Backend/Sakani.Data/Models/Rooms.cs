using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakani.Data.Models
{
    public class Rooms
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public string RoomType { get; set; }
        public int NumberOfBeds { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("ApartmentId")]
        public virtual Apartment Apartment { get; set; }
     

    }
}
