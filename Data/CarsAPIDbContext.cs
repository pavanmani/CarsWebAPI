
using CARS.Models;
using Microsoft.EntityFrameworkCore;


namespace CARS.Data
{
    public class CarsAPIDbContext : DbContext

    {
        public CarsAPIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Company> Companies { get; set; }


    }
}
