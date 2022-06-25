namespace projektApbd.Server.Controllers
{
    public static class Urls
    {
        
        public static string GetCompanyUrl(string ticker)
        {
            return $"https://api.polygon.io/v3/reference/tickers/{ticker}?apiKey={GetApiKey()}";
        }

        public static string GetDailyOpenCloseUrl(string ticker, string dateFrom, string dateTo)
        {
            return $"https://api.polygon.io/v2/aggs/ticker/{ticker}/range/1/day/{dateFrom}/{dateTo}?adjusted=true&sort=asc&limit=120&apiKey={GetApiKey()}";
        }

        private static string GetApiKey()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()["PolygonApi:ApiKey"];
        }
    }
}
