using APISpaceSRM.Data.Interfaces;
using APISpaceSRM.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APISpaceSRM.Data.Repository
{
    public class JWTRepository : JWTInterface
    {
        private readonly IConfiguration _configuration;
        public JWTRepository(IConfiguration configuration) {
            this._configuration = configuration;
        }

        public async Task<string> GetTenantTocken(Guid Id)
        {
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public async Task<Guid> GetTenantIdFromToken(string token)
        {
            
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var handler = new JwtSecurityTokenHandler(); 
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            //Помилка стається тут 
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            return Guid.Parse(claims.Identity.Name);
          
        }
    }
}
