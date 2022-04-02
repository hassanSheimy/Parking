using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.DTOs
{
    public class LogSummaryDTO
    {
        public List<GateLogDTO> LogDTO { get; set; }
        public SummaryDTO SummaryDTO { get; set; }
        public List<ParkTypeCountDTO> ParkTypeCountDTO { get; set; }
    }
}
