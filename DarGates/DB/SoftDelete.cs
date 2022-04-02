using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarGates.DB
{
    public class SoftDelete
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeleteTime { get; set; }
        public string DeletedByUserId { get; set; }
        [ForeignKey("DeletedByUserId")]
        public GardUser DeletedByUser { get; set; }
    }
}
