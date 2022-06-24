using Microsoft.IdentityModel.Tokens;
using projektApbd.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace projektApbd.Server.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        public Task<string> GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[] {new Claim("id", user.Id.ToString())}),
                    Expires = DateTime.UtcNow.AddDays(GetExpireDays()),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(GetKey()),
                        SecurityAlgorithms.HmacSha512Signature
                        )
            };

            var token = handler.CreateToken(tokenDescriptor);
            return Task.Run(() => handler.WriteToken(token));

        }

        public int? ValidateToken(string token)
        {
            if(token == null)
                return null;

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            try
            {
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(GetKey()),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken securityToken);

                JwtSecurityToken jwtSecurityToken = (JwtSecurityToken) securityToken;
                int userId = int.Parse(jwtSecurityToken.Claims.First(e => e.Type == "id").Value);

                return userId;
            } catch(Exception)
            {
                return null;
            }
        }

        private static byte[] GetKey()
        {
            return Encoding.ASCII.GetBytes(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()["Authentication: Key"]);
        }

        private static int GetExpireDays()
        {
            return int.Parse(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build()["Authentication: ExpireDays"]);
        }
    }
}
