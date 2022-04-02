using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarGates.DB
{
    public class Invitation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string QrCode { get; set; }
        public string GardUserId { get; set; }
        public GardUser GardUser { get; set; }
        public int UserType { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? CreationDate { get; set; }
        public int InvitationTypeID { get; set; }
        public InvitationType InvitationType { get; set; }
        public int? GateLogId { get; set; }
        public GateLog GateLog { get; set; }
        //[Column(TypeName = "Date")]
        //public DateTime? InDate { get; set; }
        //public string InTime { get; set; }
        //[Column(TypeName = "Date")]
        //public DateTime? OutDate { get; set; }
        //public string OutTime { get; set; }
    }
}
