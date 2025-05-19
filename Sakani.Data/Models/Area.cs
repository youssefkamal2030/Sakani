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
        string AreaId { get; set; }

        string Name { get; set; }
    }
}
