using System.Runtime.CompilerServices;
using CARS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CARS.Data.Repo
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCarsByCName(string CompanyName);
        IEnumerable<Car> GetAllCars();
        Car GetCarByID(Guid ID);
        bool SaveAll();
        void AddCar(Car car);
        Task<Car> UpdateCar(Guid id,CarRequest car);
        Task<Car> DeleteCar(Guid id);
    }
}