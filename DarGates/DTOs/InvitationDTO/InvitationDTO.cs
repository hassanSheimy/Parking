using System.ComponentModel.DataAnnotations;

namespace DarGates.DTOs
{
    public class InvitationDTO
    {
        public class AddInvitation
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string GardUserId { get; set; }
            public int InvitationTypeID { get; set; }
            [Required]
            public string Date { get; set; }
        }
        public class InvitationType
        {
            public int ID { get; set; }
            public string Type { get; set; }
        }
    }
}
