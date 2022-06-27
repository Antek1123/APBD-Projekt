using System.Net.Http.Json;
using projektApbd.Shared.Models.DTOs;

namespace projektApbd.Client.Services
{
    public interface ICompanyService
    {
        public Task<List<string>> GetTickers();
        public Task<Company> PostCompany(string ticker);
        public Task<List<DailyOpenClose>> PostDailyOpenCloses(string ticker, DateTime from, DateTime to);
    }
    public class CompanyService : ICompanyService
    {
        private readonly IHttpService _httpService;
        public CompanyService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<string>> GetTickers()
        {
            return await _httpService.Get<List<string>>("https://localhost:7040/api/Company/tickers");
        }

        public async Task<Company> PostCompany(string ticker)
        {
            return await _httpService.Post<Company>("https://localhost:7040/api/Company", ticker);
        }

        public async Task<List<DailyOpenClose>> PostDailyOpenCloses(string ticker, DateTime from, DateTime to)
        {
            return await _httpService.Post<List<DailyOpenClose>>($"https://localhost:7040/api/Company/{ticker}", new DailyOpenCloseRequest
            { 
                From = from, 
                To = to 
            });
        }
    }
}