using AutoMapper;
using CARS.Models;

namespace CARS.Data
{
    public class CarsMapping:Profile
    {
        public CarsMapping()
        {
            CreateMap<CarRequest, Car>().ForMember(c => c.Id, option => option.Ignore()).ReverseMap();

            CreateMap<CompRequest, Company>().ForMember(c => c.Cars, option => option.Ignore()).ReverseMap();

        }
    }
}
