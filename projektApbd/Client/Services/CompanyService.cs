using System.Net.Http.Json;
using projektApbd.Shared.Models.DTOs;

namespace projektApbd.Client.Services
{
    public interface ICompanyService
    {
        public Task<List<string>> GetTickers();
        public Task<Company> GetCompany(string ticker);
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

        public async Task<Company> GetCompany(string ticker)
        {
            return await _httpService.Get<Company>($"https://localhost:7040/api/Company/{ticker}");
        }
    }
}