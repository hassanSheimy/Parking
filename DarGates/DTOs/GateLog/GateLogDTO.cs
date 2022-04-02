using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.DTOs
{
    public class GateLogDTO
    {
        public int? ID { get; set; }
        public string UserID { get; set; }
        public string ParkType { get; set; }
        public int MilitryCount { get; set; }
        public int CivilCount { get; set; }
        public string QrCode { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        //public string Type { get; set; }
        public bool IsPayed { get; set; }
        public float? Price { get; set; }
        public float? MilitryPrice { get; set; }
        public float? CivilPrice { get; set; }
        public float? Total { get; set; }
        public float? RePrintFines { get; set; }
        public string PrintTime { get; set; }
        public string InDate { get; set; }
        public string InTime { get; set; }
        public string OutDate { get; set; }
        public string OutTime { get; set; }
        public DateTime? DeleteTime { get; set; }
    }
}
