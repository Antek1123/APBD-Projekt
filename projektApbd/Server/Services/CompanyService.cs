using projektApbd.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace projektApbd.Server.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _context;

        public CompanyService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddCompany(Shared.Models.DTOs.Company model)
        {
            var company = new Company
            {
                Listdate = model.Listdate,
                Ticker = model.Ticker,
                Name = model.Name,
                Url = model.Url, 
                Country = model.Country,
                Industry = model.Industry,
                Sector = model.Sector,  
                Logo = model.Logo,
                Employees = model.Employees,
                Phone = model.Phone,
                Ceo = model.Ceo,
                Description = model.Description,
                Exchange = model.Exchange,
                HqAddress = model.HqAddress,
                HqState = model.HqState,
                HqCountry = model.HqCountry,
                Active = model.Active
            };
            await _context.AddAsync(company);
        }

        public async Task<Company> GetCompany(string ticker)
        {
            return await _context.Companies.FirstOrDefaultAsync(e => e.Ticker == ticker);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
