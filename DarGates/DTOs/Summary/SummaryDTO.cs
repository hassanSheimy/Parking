using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.DTOs
{
    public class SummaryDTO
    {
        public int Count { get; set; }
        public int MemberCarCount { get; set; }
        public int MilitryCarCount { get; set; }
        public int CitizenCarCount { get; set; }
        public int ActivityCarCount { get; set; }
        public float? ParkPrice { get; set; }
        public float? MilitryPrice { get; set; }
        public int MilitryCount { get; set; }
        //accompaniment
        public float? CivilPrice { get; set; }
        public int CivilCount { get; set; }
        public float? Total { get; set; }
        public float? RePrintFines { get; set; }
        public float? Total_Fines { get; set; }
    }
}
