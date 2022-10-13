using System.ComponentModel.DataAnnotations;

namespace CARS.Models
{
    public class Company
    {             
            [Key]
            public string Name { get; set; }

            public string CEO { get; set; }
            public string Country { get; set; }
            public int Pincode { get; set; }
            public ICollection<Car> Cars { get; set; }
        
    }
}

