using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.DTOs
{
    public class BillDTO
    {
        [Required]
        public int TypeID { get; set; }
        [Required]
        public int MilitryCount { get; set; }
        [Required]
        public int CivilCount { get; set; }
        public float? Price { get; set; }
        public float? MilitryPrice { get; set; }
        public float? CivilPrice { get; set; }
        public float? Total { get; set; }
    }
}
