using System.Net.Http.Json;
using projektApbd.Shared.Models.DTOs;

namespace projektApbd.Client.Services
{
    public interface ICompanyService
    {
        public Task<List<string>> GetTickers();
        public Task<Company> PostCompany(string ticker);
        public Task PostDailyOpenCloses(string ticker, DateTime from, DateTime to);
        public Task<List<DailyOpenClose>> GetDailyOpenCloses(int id, DateTime from, DateTime to);
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

        public async Task PostDailyOpenCloses(string ticker, DateTime from, DateTime to)
        {
            await _httpService.Post($"https://localhost:7040/api/Company/ticker", new DailyOpenCloseRequest
            { 
                Ticker = ticker,
                From = from, 
                To = to 
            });
        }

        public async Task<List<DailyOpenClose>> GetDailyOpenCloses(int id, DateTime from, DateTime to)
        {
            var fromString = from.ToString("yyyy-MM-dd");
            var toString = to.ToString("yyyy-MM-dd");

            return await _httpService.Get<List<DailyOpenClose>>($"https://localhost:7040/api/Company/{id}/{fromString}/{toString}");
            //return await _httpService.Get<List<DailyOpenClose>>("https://localhost:7040/api/Company/14/2022-06-20/2022-06-27");
        }
    }
}