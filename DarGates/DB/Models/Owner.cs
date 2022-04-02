
namespace DarGates.DB
{
    public class Owner : SoftDelete
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public float PriceInHoliday { get; set; }

    }
}