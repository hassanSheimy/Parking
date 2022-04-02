using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.DTOs
{
    public class ParkedReasonDTO
    {
        public List<ParkedDTO> ParkedDTO { get; set; }
        public List<ReasonDTO> ReasonDTO { get; set; }
        public int Count { get; set; }
    }
}
