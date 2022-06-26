namespace projektApbd.Client.Services
{
    public interface ICompanyService
    {
        public Task<List<string>> GetTickers();
    }
}
