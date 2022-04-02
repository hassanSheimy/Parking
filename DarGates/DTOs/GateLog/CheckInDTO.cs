using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.DTOs
{
    public class CheckInDTO
    {
        public class Request
        {
            public int? ID { get; set; }
            public string UserID { get; set; }
            public int TypeID { get; set; }
            public int MilitryCount { get; set; }
            public int CivilCount { get; set; }
        }
        public class Response
        {
            public int ID { get; set; }
            public string QrCode { get; set; }
            public float Price { get; set; }
            public float MilitryPrice { get; set; }
            public float CivilPrice { get; set; }
            public float Total { get; set; }
            public string PrintTime { get; set; }
        }
    }
}
