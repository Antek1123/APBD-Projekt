using projektApbd.Shared.Models;
using projektApbd.Server.Exceptions;
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

        public async Task<Company> AddCompany(Shared.Models.DTOs.Company model)
        {
            var company = new Company
            { 
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

            if (await IsCompanyExists(company.Ticker))
            {
                return await GetCompany(company.Ticker);
            }
            else
            {
                await _context.AddAsync(company);
                await SaveChanges();
                return company;
            }
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

            if (await IsDailyOpenCloseExists(dailyOpenClose.Id, dailyOpenClose.Date))
            {
                return await GetDailyOpenClose(dailyOpenClose.Id, dailyOpenClose.Date);
            }
            else
            {
                await _context.AddAsync(dailyOpenClose);
                await SaveChanges();
                return dailyOpenClose;
            }
        }

        public async Task<Company> GetCompany(string ticker)
        {
            Company? company = await _context.Companies.FirstOrDefaultAsync(e => e.Ticker.Equals(ticker));
            if (company == null)
                throw new NotFoundException("Company not exists");

            return company;
        }

        public async Task<DailyOpenClose> GetDailyOpenClose(int idCompany, DateTime date)
        {
            DailyOpenClose? dailyOpenClose = await _context.DailyOpenCloses.FirstOrDefaultAsync(e => e.Id == idCompany && e.Date.Date.Equals(date.Date));
            if (dailyOpenClose == null)
                throw new NotFoundException("Daily open close not exists.");

            return dailyOpenClose;
        }

        public async Task<List<DailyOpenClose>> GetDailyOpenCloses(int idCompany, DateTime dateFrom, DateTime dateTo)
        {
            return await _context.DailyOpenCloses.Where(e => e.Date > dateFrom && e.Date < dateTo && e.Id == idCompany).ToListAsync();
        }

        public async Task<bool> IsCompanyExists(string ticker)
        {
            return await _context.Companies.Where(e => e.Ticker.Equals(ticker)).AnyAsync();
        }

        public async Task<bool> IsDailyOpenCloseExists(int companyId, DateTime date)
        { 
            return await _context.DailyOpenCloses.AnyAsync(e => e.Date.Date.Equals(date.Date) && e.Id == companyId);
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
