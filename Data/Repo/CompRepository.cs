using System.Linq;
using CARS.Models;
using Microsoft.EntityFrameworkCore;

namespace CARS.Data.Repo
{
    public class CompRepository : ICompRepository
    {
        private readonly CarsAPIDbContext _ctx;
        public CompRepository(CarsAPIDbContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Company> GetAllCompanies(bool includeCars)
        {
           // if (includeCars)
            //{
             //return _ctx.Companies.OrderBy(c => c.Name).ToList();
            //}
           // else
            //{
                return _ctx.Companies.Include(o => o.Cars).ToList();
            //}            
            //return _ctx.Companies.Include(o=>o.cars).OrderBy(c => c.Name).ToList();
        }
        public Company GetCompanyByName(string name)
        {
            return _ctx.Companies.Where(p => p.Name == name).FirstOrDefault();
        }
        public void AddCompany(Company c)
        {
            _ctx.Add(c);
        }
        public async Task<Company> UpdateCompany(string Name,CompRequest com)
        {
            var result = await _ctx.Companies.FirstOrDefaultAsync(e => e.Name == Name);
            //car result = _ctx.Cars.Where(p => p.Id == id)
            if (result != null)
            {
                result.Name = com.Name;
                result.CEO = com.CEO;
                result.Country = com.Country;
                result.Pincode = com.Pincode;

                await _ctx.SaveChangesAsync();

                return result;
            }
            return null;
        }
        public async Task<Company> DeleteCompany(string name)
        {
            var result = await _ctx.Companies.FirstOrDefaultAsync(e => e.Name == name);
            if (result != null)
            {
                _ctx.Companies.Remove(result);
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
