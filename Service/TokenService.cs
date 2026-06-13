using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;
namespace api.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        private readonly SymmetricSecurityKey _key;
        
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
            var signingKey = _configuration.GetValue<string>("JWT:Signingkey")
                ?? throw new InvalidOperationException("JWT signing key is not configured.");
            _key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(signingKey));
        }
        public string CreateToken(AppUser user)
        {
            var email = user.Email ?? throw new InvalidOperationException("User email is required.");
            var userName = user.UserName ?? throw new InvalidOperationException("User name is required.");
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.GivenName, userName)
            };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _configuration.GetValue<string>("JWT:Issuer") ?? throw new InvalidOperationException("JWT issuer is not configured."),
                Audience = _configuration.GetValue<string>("JWT:Audience") ?? throw new InvalidOperationException("JWT audience is not configured.")
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

       
    }
}