using projektApbd.Shared.Models;

namespace projektApbd.Server.Services

{
    public class UserCompanyService : IUserCompanyService
    {
        private readonly AppDbContext _context;

        public UserCompanyService(AppDbContext context)
        {
            _context = context;
        }

    }
}
