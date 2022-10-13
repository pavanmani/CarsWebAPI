using System.Linq;
using AutoMapper;
using CARS.Data;
using CARS.Data.Repo;
using CARS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CARS.Controllers
{
    
    [ApiController]
    [Route("api/comp")]
    //[Route("api/[controller]")]
    public class CompController : Controller
    {
        private readonly ICompRepository _repository;
        private readonly IMapper _mapper;

        public CompController(ICompRepository repository,IMapper Mapper)
        {
            this._repository = repository;
            this._mapper = Mapper;
        }
        [HttpGet]
        public IActionResult GetComapnies(bool includeCars=true)
        {
            var results = _repository.GetAllCompanies(includeCars);
            if (includeCars)
            {
                
                return Ok(_mapper.Map<IEnumerable<Company>>(results));
            }
            else
            {
                return Ok(_mapper.Map<IEnumerable<CompRequest>>(results));
            }
        }
        //public IActionResult GetComapnies()
        //{
        //    var results = _repository.GetAllCompanies();
        //    return Ok(_mapper.Map<IEnumerable<Company>>(results));
        //}
        [HttpGet]
        [Route("{Name}")]
        public IActionResult GetCompanyByName(string Name)
        {
            var company = _repository.GetCompanyByName(Name);
            if (company == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CompRequest>(company));
        }
        
        [HttpPost]
        public IActionResult AddCompany([FromBody]CompRequest addCompRequest)
        {
            {
                var company = _mapper.Map<CompRequest, Company>(addCompRequest);
                _repository.AddCompany(company);
                if (_repository.SaveAll())
                {
                    return Ok(company);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }   
        }

        [HttpPut]
        [Route("{Name}")]
        public async Task<ActionResult<Company>> UpdateCompany([FromRoute]string Name,[FromBody]CompRequest com)
        {
            try
            {
                if (Name != com.Name)
                    return BadRequest("ID mismatch");

                var CompanyToUpdate = _repository.GetCompanyByName(Name);

                if (CompanyToUpdate == null)
                    return NotFound($"Company with Name = {Name} not found");

                await _repository.UpdateCompany(Name,com);
                return Created($"/api/Comp/{com.Name}", _mapper.Map<CompRequest, Company>(com));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");

            }
        }

        [HttpDelete]
        [Route("{Name}")]
        public async Task<ActionResult<Company>> DeleteCompany(string Name)
        {
            return await _repository.DeleteCompany(Name);
        }
    }
}
