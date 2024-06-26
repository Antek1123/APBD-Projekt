﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projektApbd.Server.Services;
using projektApbd.Server.Exceptions;
using projektApbd.Shared.Models.DTOs;
using projektApbd.Server.Authorization;

namespace projektApbd.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCompany([FromBody] string ticker)
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
                Company company = await _service.GetCompany(ticker);

                if (company == null)
                    throw new NotFoundException("object not exists in database");

                return Ok(company);
            }
        }

        [HttpPost("ticker")]
        [Authorize]
        public async Task<IActionResult> AddDailyOpenClose([FromBody] DailyOpenCloseRequest request)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(Urls.GetDailyOpenCloseUrl(request.Ticker, request.From.ToString("yyyy-MM-dd"), request.To.ToString("yyyy-MM-dd")));
                    PolygonAggregatesResponse? polygonAggregatesResponse = JsonConvert.DeserializeObject<PolygonAggregatesResponse>(response);
                    if (polygonAggregatesResponse != null)
                    {
                        foreach (var dailyOpenClose in polygonAggregatesResponse.Results)
                        {
                            await _service.AddDailyCloseValues(dailyOpenClose, _service.GetCompany(request.Ticker).Result.Id);
                            await _service.SaveChanges();
                        }
                        return Ok(response);
                    } else
                    {
                        return BadRequest(response);
                    }
                }
            } catch (TooManyRequestException)
            {
                var dailyOpenCloses = await _service.GetDailyOpenCloses(_service.GetCompany(request.Ticker).Result.Id, request.From, request.To);
                return Ok(dailyOpenCloses);
                //todo dokonczyc
            }
        }

        [HttpGet("{ticker}")]
        [Authorize]
        public async Task<IActionResult> GetCompanyByTicker(string ticker)
        {
            return Ok(await _service.GetCompany(ticker));
        }

        [HttpGet("{idCompany}/{dateFrom}/{dateTo}")]
        [Authorize]
        public async Task<IActionResult> GetDailyOpenCloses(int idCompany, DateTime dateFrom, DateTime dateTo)
        {
            return Ok(await _service.GetDailyOpenCloses(idCompany, dateFrom, dateTo));
        }

        [HttpGet("tickers")]
        [Authorize]
        public async Task<IActionResult> GetAllTickers()
        {
            return Ok(await _service.GetAllTickers());
        }
    }
}
