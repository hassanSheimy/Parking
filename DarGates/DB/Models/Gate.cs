using System.Collections.Generic;

namespace DarGates.DB
{
    public class Gate
    {
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrinterMac { get; set; }
        public virtual IList<GardUser> User { get; set; }
    }
}