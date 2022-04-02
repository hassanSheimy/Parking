using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarGates.DB
{
    public class SignInLog
    {
        public int Id { get; set; }
        public string GardUserId { get; set; }
        public GardUser GardUser { get; set; }
        [Column(TypeName = "Date")]
        public DateTime LogInDate { get; set; }
        public string LogInTime { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? LogOutDate { get; set; }
        public string LogOutTime { get; set; }
        public string Image { get; set; }
    }
}
