using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth_Exam.Infrastructure.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Auth_Exam.Core.TokenProviders
{
    public class TokenProvider
    {
        private readonly IConfiguration? _configuration;

        public TokenProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user, IList<string> roles)
        {
            //get settings from AppSettings.json
            var key = _configuration!["JWT:Key"];
            var issuer = _configuration["JWT:Issuer"];
            var audience= _configuration["JWT:Audience"];


            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Id)
            };


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            //Create signin key (converts the key string to bytes)
            var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!));

            // Create the credentials that will sign the token
            var credentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);


            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials

            };


            //the actual token
            var tokenHandler =new  JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //convert token to string (the actual jwt)
            return tokenHandler.WriteToken(token);
        

        }

    }
}