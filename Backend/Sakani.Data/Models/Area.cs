using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakani.Data.Models
{
    public class Area
    {
        [Key]
       public  string AreaId { get; set; }

        public string Name { get; set; }

        [MaxLength(500)]
       public  string? location { get; set; }
    }
}
