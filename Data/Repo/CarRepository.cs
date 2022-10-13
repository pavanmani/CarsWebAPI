using System;
using CARS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CARS.Data.Repo
{
    public class CarRepository : ICarRepository
    {
        private readonly CarsAPIDbContext _ctx;
        public CarRepository(CarsAPIDbContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Car> GetCarsByCName(string CompanyName)
        {
            return _ctx.Cars.Where(p => p.CompanyName == CompanyName);
        }
        public IEnumerable<Car> GetAllCars()
        {
            return _ctx.Cars.OrderBy(c => c.CompanyName).ToList();
        }
        public Car GetCarByID(Guid id)
        {
            return _ctx.Cars.Where(p => p.Id == id).FirstOrDefault();
        }
        public void AddCar(Car c)
        {
            _ctx.Add(c);
        }
        public async Task<Car> UpdateCar(Guid id,CarRequest car)
        {
            var result = await _ctx.Cars.FirstOrDefaultAsync(e => e.Id == id);
            //car result = _ctx.Cars.Where(p => p.Id == id)
            if (result != null)
            { 
                result.Name = car.Name;
                result.Fuel = car.Fuel;
                result.Color=car.Color;
                result.Hp=car.Hp;
                result.CompanyName = car.CompanyName;

                await _ctx.SaveChangesAsync();

                return result;
            }

            return null;
        }
        public async Task<Car> DeleteCar(Guid id)
        {
            var result = await _ctx.Cars.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                _ctx.Cars.Remove(result);
                await _ctx.SaveChangesAsync();
                return result;
            }
            return null;
        }
        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }       

    }
}
