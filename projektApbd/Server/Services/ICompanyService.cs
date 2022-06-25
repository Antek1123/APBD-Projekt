using projektApbd.Shared.Models;

namespace projektApbd.Server.Services
{
    public interface ICompanyService
    {
        public Task<Company> GetCompany(string ticker);
        public Task<Company> AddCompany(Shared.Models.DTOs.Company model);

        public Task<DailyOpenClose> AddDailyCloseValues(Shared.Models.DTOs.PolygonAggregates aggregates, int companyId);
        public Task SaveChanges();
    }
}
