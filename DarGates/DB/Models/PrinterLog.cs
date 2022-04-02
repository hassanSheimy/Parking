using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarGates.DB
{
    public class PrinterLog : SoftDelete
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public GardUser User { get; set; }
        public int RePrintReasonID { get; set; }
        public RePrintReason RePrintReason { get; set; }
        public int GateLogID { get; set; }
        public GateLog GateLog { get; set; }
        public float Price { get; set; }
        public bool IsPayed { get; set; } = false;
        [Column(TypeName = "Date")]
        public DateTime? PrintDate { get; set; }
        public string PrintTime { get; set; }
    }
}
