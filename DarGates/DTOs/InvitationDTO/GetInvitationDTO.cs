namespace DarGates.DTOs
{
    public class GetInvitationDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string InvitationType { get; set; }
        public string CreationDate { get; set; }
        public string QrCode { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public int Status { get; set; }
    }
}