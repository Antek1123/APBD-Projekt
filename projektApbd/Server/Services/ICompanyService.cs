﻿using projektApbd.Shared.Models;

namespace projektApbd.Server.Services
{
    public interface ICompanyService
    {
        public Task<Shared.Models.DTOs.Company> GetCompany(string ticker);
        public Task<bool> IsCompanyExists(string ticker);
        public Task<bool> IsDailyOpenCloseExists(int userId, DateTime date);
        public Task<Shared.Models.DTOs.Company> AddCompany(Shared.Models.DTOs.Company model);
        public Task<List<DailyOpenClose>> GetDailyOpenCloses(int idCompany, DateTime dateFrom, DateTime dateTo);
        public Task<DailyOpenClose> GetDailyOpenClose(int idCompany, DateTime date);
        public Task<DailyOpenClose> AddDailyCloseValues(Shared.Models.DTOs.PolygonAggregates aggregates, int companyId);
        public Task<List<string>> GetAllTickers();
        public Task SaveChanges();
    }
}
