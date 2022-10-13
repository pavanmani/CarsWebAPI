using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CARS.Models
{
    public class Car
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public string Color { get; set; }
        public int Hp { get; set; }
        public string Fuel { get; set; }

        [ForeignKey("Company")] 
        public string CompanyName { get; set; }
        //public Company Company { get; set; }

    }
}
