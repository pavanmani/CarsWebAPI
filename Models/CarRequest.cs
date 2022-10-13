using System.ComponentModel.DataAnnotations;

namespace CARS.Models
{
    public class CarRequest
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        public string Color { get; set; }
        public int Hp { get; set; }
        public string Fuel { get; set; }
        public string CompanyName { get; set; }
    }
}
