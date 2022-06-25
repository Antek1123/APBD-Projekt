using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projektApbd.Server.Services;
using projektApbd.Server.Exceptions;
using projektApbd.Shared.Models.DTOs;

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
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(Urls.GetCompanyUrl(ticker));
                    PolygonResponse polygonResponse = JsonConvert.DeserializeObject<PolygonResponse>(response);

                    PolygonCompany company = polygonResponse.results;

                    var output = new Company
                    {
                        Ticker = company.Ticker,
                        Name = company.Name,
                        Homepage_url = company.Homepage_url,
                        Locale = company.Locale,
                        Logo_Url = company.branding.Logo_Url,
                        Phone_Number = company.Phone_Number,
                        Description = company.Description,
                        Currency_Name = company.Currency_Name,
                        Active = company.Active
                    };

                    return Ok(await _service.AddCompany(output));
                }
            } catch (TooManyRequestException)
            {
                Shared.Models.Company company = await _service.GetCompany(ticker);

                if (company == null)
                    throw new NotFoundException("object not exists in database");

                return Ok(company);
            }
        }

        [HttpPost("{ticker}/{dateFrom}/{dateTo}")]
        public async Task<IActionResult> AddDailyOpenClose(string ticker, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(Urls.GetDailyOpenCloseUrl(ticker, dateFrom.ToString("yyyy-MM-dd"), dateTo.ToString("yyyy-MM-dd")));
                    PolygonAggregatesResponse polygonAggregatesResponse = JsonConvert.DeserializeObject<PolygonAggregatesResponse>(response);
                    foreach (var dailyOpenClose in polygonAggregatesResponse.Results)
                    {
                        await _service.AddDailyCloseValues(dailyOpenClose, _service.GetCompany(ticker).Result.Id);
                        await _service.SaveChanges();
                    }
                    return Ok(response);
                }
            } catch (TooManyRequestException)
            {
                var dailyOpenCloses = _service.GetDailyOpenCloses(_service.GetCompany(ticker).Result.Id, dateFrom, dateTo);
                return Ok(dailyOpenCloses);
                //todo dokonczyc
            }
        }

        [HttpGet("{ticker}")]
        public async Task<IActionResult> GetCompanyByTicker(string ticker)
        {
            return Ok(await _service.GetCompany(ticker));
        }

        [HttpGet("{idCompany}/{dateFrom}/{dateTo}")]
        public async Task<IActionResult> GetDailyOpenCloses(int idCompany, DateTime dateFrom, DateTime dateTo)
        {
            return Ok(await _service.GetDailyOpenCloses(idCompany, dateFrom, dateTo));
        }
    }
}
