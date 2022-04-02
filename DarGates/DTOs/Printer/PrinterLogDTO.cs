using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.DTOs
{
    public class PrinterLogDTO
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string RePrintReason { get; set; }
        public int GateLogID { get; set; }
        public float Price { get; set; }
        public string PrintDate { get; set; }
        public string PrintTime { get; set; }
        public DateTime? DeleteTime { get; set; }
    }
}
