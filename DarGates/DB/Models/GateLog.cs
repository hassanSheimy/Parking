using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarGates.DB
{
    public class GateLog : SoftDelete
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public GardUser User { get; set; }
        public string ParkType { get; set; }
        public float Price { get; set; } = 0;
        public int MilitryCount { get; set; } = 0;
        public float MilitryPrice { get; set; } = 0;
        public int CivilCount { get; set; } = 0;
        public float CivilPrice { get; set; } = 0;
        public float Total { get; set; } = 0;
        [Column(TypeName = "Date")]
        public DateTime? InDate { get; set; }
        public string InTime { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? OutDate { get; set; }
        public string OutTime { get; set; }
        public bool IsPayed { get; set; } = false;
        public string QRCode { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public virtual IList<PrinterLog> PrinterLogs { get; set; }
    }
}
