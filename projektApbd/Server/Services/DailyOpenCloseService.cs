using projektApbd.Shared.Models;

namespace projektApbd.Server.Services
{
    public class DailyOpenCloseService : IDailyOpenCloseService
    {
        private readonly AppDbContext _context;

        public DailyOpenCloseService(AppDbContext context)
        {
            _context = context;
        }
    }
}
