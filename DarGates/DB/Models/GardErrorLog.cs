using System;

namespace DarGates.DB
{
    public class GardErrorLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string EndPoint { get; set; }
        public DateTime DateTime { get; set; }
    }
}
