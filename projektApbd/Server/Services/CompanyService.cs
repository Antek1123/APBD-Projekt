using projektApbd.Shared.Models;

namespace projektApbd.Server.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext _context;

        public CompanyService(AppDbContext context)
        {
            _context = context;
        }
    }
}
