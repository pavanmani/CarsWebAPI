//This is for adding initial data to sql db ; we can also use Json file to add code to db
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using CARS.Models;

namespace CARS.Data
{
    public class CompanySeeder
    {
        private readonly CarsAPIDbContext _ctx;
        public CompanySeeder(CarsAPIDbContext ctx)
        {
            _ctx = ctx;
        }
        public void Seed()
        {
            _ctx.Database.EnsureCreated();

            if (!_ctx.Companies.Any())
            {
                var companies = new List<Company>
                {
                   new Company{Name="Toyota"},
                   new Company{Name="Benz"},
                   new Company{Name="BMW"},
                   new Company{Name="MG"},
                   new Company{Name="RR"}
                };
                _ctx.Companies.AddRange(companies);
                _ctx.SaveChanges();
                var cars = new List<Car>
                {
                    new Car{Name= "Storm",Color= "Black",Hp=2999,Fuel= "Electric",CompanyName= "MG"},
                    new Car{Name= "Innova",Color= "Silver",Hp=3333,Fuel= "Diesel",CompanyName= "Toyota"}
                };

                _ctx.Cars.AddRange(cars);
                _ctx.SaveChanges();

            }
        }

    }
}
