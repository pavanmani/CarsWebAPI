using AutoMapper;
using CARS.Data.Repo;
using CARS.Models;
using Microsoft.AspNetCore.Mvc;

namespace CARS.Controllers
{
    [ApiController]
    [Route("api/comp/cars/{CompanyName}")]
    //[Route("api/[Controller]")]
    public class AscController : Controller
    {
        private readonly ICarRepository _repository;
        private readonly IMapper _mapper;
        public AscController(ICarRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetCars(string CompanyName)
        {
            var results = _repository.GetCarsByCName(CompanyName);
            if (results != null) return Ok(_mapper.Map<IEnumerable<Car>>(results));
            return NotFound();
        }
        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetCar([FromRoute] Guid id, string CompanyName)
        {
            var cars = _repository.GetCarsByCName(CompanyName);
            if (cars != null)
            {
                var car = cars.Where(x => x.Id == id).FirstOrDefault();
                if (car != null)
                {
                    return Ok(_mapper.Map<CarRequest>(car));
                }

            }
            return NotFound();
        }
    }
}
