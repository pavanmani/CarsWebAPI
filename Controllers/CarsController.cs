using AutoMapper;
using CARS.Data.Repo;
using CARS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CARS.Controllers
{
    [ApiController]
    [Route("api/cars")]
    //[Route("api/[controller]")]
    //same both
    public class CarsController : Controller
    {
        private readonly ICarRepository _repository;
        private readonly IMapper _mapper;
        public CarsController(ICarRepository repository,IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        //[HttpGet]
        //public IActionResult GetCars(string CompanyName)
        //{
        //    var results = _repository.GetCarsByCName(CompanyName);
        //    if(results!=null) return Ok(_mapper.Map<IEnumerable<car>>(results));
        //    return NotFound();
        //}
        //[HttpGet]
        //[Route("{id:guid}")]
        //public IActionResult GetCar([FromRoute] Guid id,string CompanyName)
        //{
        //    var cars = _repository.GetCarsByCName(CompanyName);
        //    if (cars != null)
        //    {
        //        var car = cars.Where(x => x.Id == id).FirstOrDefault();
        //        return Ok(_mapper.Map<CarRequest>(car));

        //    }
        //    return NotFound();
        //}
        [HttpGet]
        public IActionResult GetCars()
        {
            var results = _repository.GetAllCars();
            return Ok(_mapper.Map<IEnumerable<Car>>(results));
        }
        //[HttpGet]
        //public IActionResult GetCar([FromRoute] Guid id)
        //{
        //    var car = _repository.GetCarByID(id);
        //    if (car == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(_mapper.Map<CarRequest>(car));
        //}
        [HttpPost]
        public  IActionResult AddCar([FromBody]CarRequest addCarRequest)
        {
            var car = _mapper.Map<CarRequest, Car>(addCarRequest);

            _repository.AddCar(car);
            if (_repository.SaveAll())
            {
                //return Ok(car);
                return Created($"/api/cars/{car.Name}", _mapper.Map<CarRequest>(car));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<ActionResult<Car>> UpdateCar(CarRequest car,Guid id)
        {
            try
            {
                //if (id != car.Id)
                //    return BadRequest("ID mismatch");

                var CarToUpdate = _repository.GetCarByID(id);

                if (CarToUpdate == null)
                    return NotFound($"Car with Id = {id} not found");
                
                await _repository.UpdateCar(id,car);
                return Created($"/api/cars/{car.Name}", _mapper.Map<CarRequest>(car));               
              
             }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<ActionResult<Car>> DeleteCar([FromRoute] Guid id)
        {
           return await _repository.DeleteCar(id);
        }
    }
}
