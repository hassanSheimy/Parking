using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DarGates.DTOs
{
    public class UserDTO
    {
        public class Request
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Name { get; set; }
            public string Rank { get; set; }
            public int GateID { get; set; }
        }
        public class Response 
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string GateName { get; set; }
            public int GateID { get; set; }
            public string PrinterMac { get; set; }
            public double Balance { get; set; }
            public string Token { get; set; }
        }
        
    }
}
