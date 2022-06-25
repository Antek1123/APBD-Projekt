using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projektApbd.Server.Services;
using projektApbd.Shared.Models.DTOs;
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

        [HttpPost("{ticker}")]
        public async Task<IActionResult> AddCompany(string ticker)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync($"https://api.polygon.io/v3/reference/tickers/{ticker}?apiKey={GetApiKey()}");
                PolygonResponse polygonResponse = JsonConvert.DeserializeObject<PolygonResponse>(response);

                PolygonCompany company = polygonResponse.results;
                var output = await _service.AddCompany(new Company
                {
                    Id = company.Id,
                    Ticker = company.Ticker,
                    Name = company.Name,
                    Homepage_url = company.Homepage_url,
                    Locale = company.Locale,
                    Logo_Url = company.branding.Logo_Url,
                    Phone_Number = company.Phone_Number,
                    Description = company.Description,
                    Currency_Name = company.Currency_Name,
                    Active = company.Active
                });
                await _service.SaveChanges();

                return Ok(output);
            }

        }

        [HttpPost("{ticker}/{dateFrom}/{dateTo}")]
        public async Task<IActionResult> AddDailyOpenClose(string ticker, string dateFrom, string dateTo)
        {
            //https://api.polygon.io/v2/aggs/ticker/AAPL/range/1/day/2021-07-22/2021-08-22?adjusted=true&sort=asc&limit=120&apiKey=V9UeRzooTiXCR4RhnXUEpXpapduevCTA

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync($"https://api.polygon.io/v2/aggs/ticker/{ticker}/range/1/day/{dateFrom}/{dateTo}?adjusted=true&sort=asc&limit=120&apiKey={GetApiKey()}");
                PolygonAggregatesResponse polygonAggregatesResponse = JsonConvert.DeserializeObject<PolygonAggregatesResponse>(response);
                foreach (var dailyOpenClose in polygonAggregatesResponse.Results)
                {
                    await _service.AddDailyCloseValues(dailyOpenClose, _service.GetCompany(ticker).Result.Id);
                    await _service.SaveChanges();
                }
                return Ok(response);


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
}
