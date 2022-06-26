using System.Net.Http.Json;

namespace projektApbd.Client.Services
{
    public class CompanyService
    {
        private readonly HttpClient _htttpClient;
        public CompanyService(HttpClient httpclient)
        {
            _htttpClient = httpclient;
        }
        public async Task<List<string>> GetTickers()
        {
            var list = await _htttpClient.GetFromJsonAsync<List<string>>("api/tickers");
            if (list == null)
                throw new Exception();
            return list;
        }
    }
}
