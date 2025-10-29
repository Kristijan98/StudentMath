using Microsoft.IdentityModel.Tokens;
using StudentMath.Core.Domain;
using System.Security.Claims;
using System.Text;

namespace StudentMath.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(InMemoryUser user)
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtKey"] ?? "ThisIsASuperSecretKey1234567890!!!");
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public InMemoryUser ValidateUser(string username, string password)
        {
            return UserStore.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
