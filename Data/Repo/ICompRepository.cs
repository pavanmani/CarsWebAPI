using CARS.Models;

namespace CARS.Data.Repo
{
    public interface ICompRepository
    {
        IEnumerable<Company> GetAllCompanies(bool includeCars);
        public Company GetCompanyByName(string name);
        void AddCompany(Company c);
        Task<Company> UpdateCompany(string Name,CompRequest car);
        Task<Company> DeleteCompany(string Name);
        bool SaveAll();
    }
}