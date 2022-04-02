using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DarGates.DB
{
    public class OfficialHoliday
    {
        public int Id { get; set; }
        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
    }
}
