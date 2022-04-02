using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace DarGates.DB
{
    public class GardUser : IdentityUser
    {
        public string Name { get; set; }
        public string Rank { get; set; }
        public int? GateID { get; set; }
        public Gate Gate { get; set; }
        public long? UID { get; set; }
        public virtual IList<SignInLog> SignInLog { get; set; }
        public virtual IList<GateLog> GateLogs { get; set; }
        public virtual IList<GateLog> DeletedLogs { get; set; }
        public virtual IList<PrinterLog> PrinterLogs { get; set; }
        public virtual IList<PrinterLog> DeletedPrinterLogs { get; set; }
        public virtual IList<Invitation> Invitations { get; set; }
    }
}