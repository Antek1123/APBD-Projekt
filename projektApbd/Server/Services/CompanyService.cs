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

        public async Task<Shared.Models.Company> AddCompany(Shared.Models.DTOs.Company model)
        {
            var company = new Company
            {
                Id = model.Id,
                Ticker = model.Ticker,
                Name = model.Name,
                Homepage_url = model.Homepage_url,
                Locale = model.Locale,
                Logo_Url = model.Logo_Url,
                Phone_Number = model.Phone_Number,
                Description = model.Description,
                Currency_Name = model.Currency_Name,
                Active = model.Active
            };

            if (IsCompanyExists(company.Ticker).Result)
            {
                _context.Entry(company).State = EntityState.Modified;
            } else
            {
                await _context.AddAsync(company);
            }

            await SaveChanges();
            return company;
        }

        public async Task<DailyOpenClose> AddDailyCloseValues(Shared.Models.DTOs.PolygonAggregates aggregates, int companyId)
        {
            var dailyOpenClose = new DailyOpenClose
            {
                Id = companyId,
                Date = UnixTimeToDateTime(aggregates.T),
                Open = aggregates.O,
                High = aggregates.H,
                Low = aggregates.L,
                Close = aggregates.C,
                Volume = aggregates.V
            };
            await _context.AddAsync(dailyOpenClose);
            return dailyOpenClose;
    
        }

        public async Task<Company> GetCompany(string ticker)
        {
            return await _context.Companies.FirstOrDefaultAsync(e => e.Ticker == ticker);
        }

        public async Task<List<DailyOpenClose>> GetDailyOpenCloses(int idCompany, DateTime dateFrom, DateTime dateTo)
        {
            return await _context.DailyOpenCloses.Where(e => e.Date > dateFrom && e.Date < dateTo && e.Id == idCompany).ToListAsync();
        }

        public async Task<bool> IsCompanyExists(string ticker)
        {
            return await _context.Companies.AnyAsync(e => e.Ticker == ticker);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
        private DateTime UnixTimeToDateTime(long unixtime)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dtDateTime.AddMilliseconds(unixtime).ToLocalTime();
        }

    }
}
