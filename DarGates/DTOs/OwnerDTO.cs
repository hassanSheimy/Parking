using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.DTOs
{
    public class OwnerDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public float PriceInHoliday { get; set; }
    }
}
