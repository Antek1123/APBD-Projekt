using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projektApbd.Server.Services;
using System.Text.Json;

namespace projektApbd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany(string ticker)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync($"https://api.polygon.io/v3/reference/tickers/{ticker}?apiKey={GetApiKey()}");
                jsonCompany jsonCompany = JsonConvert.DeserializeObject<jsonCompany>(response);

                Shared.Models.DTOs.Company company = jsonCompany.results;
                await _service.AddCompany(company);
                await _service.SaveChanges();

                return Ok(company);
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetCompanyByTicker(string ticker)
        {
            return Ok(await _service.GetCompany(ticker));
        }

        private string GetApiKey()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()["PolygonApi:ApiKey"];
        }
    }

    public class jsonCompany
    {
        public Shared.Models.DTOs.Company results { get; set; }
    }
}
