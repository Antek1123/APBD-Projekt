using System.Net.Http.Json;

namespace projektApbd.Client.Services
{
    public interface ICompanyService
    {
        public Task<List<string>> GetTickers();
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
    }
}
