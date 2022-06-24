namespace projektApbd.Server.Authorization
{
    public interface IJwtUtils
    {
        public Task<string> GenerateToken(projektApbd.Shared.Models.User user);
        public int? ValidateToken(string token);
    }
}
