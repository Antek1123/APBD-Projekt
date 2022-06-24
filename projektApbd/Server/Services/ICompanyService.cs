using projektApbd.Shared.Models;

namespace projektApbd.Server.Services
{
    public interface ICompanyService
    {
        public Task<Company> GetCompany(string ticker);
        public Task AddCompany(Shared.Models.DTOs.Company model);
        public Task SaveChanges();
    }
}
