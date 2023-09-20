using Common.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class HelperFunctions : IHelperFunctions
    {
        public string EvaluateCollectionName<T>()
        {
            var collectionName = typeof(T).ToString().ToLower().Split('.');
            if (collectionName.Contains("user"))
                return "users";
            else if (collectionName.Contains("personaldetails"))
                return "personal_details";
            else if (collectionName.Contains("myskills"))
                return "my_skills";
            else if (collectionName.Contains("myservices"))
                return "my_services";
            else if (collectionName.Contains("blacklistedtokens"))
                return "blacklisted_tokens";
            else if (collectionName.Contains("experiences"))
                return "experiences";
            else if (collectionName.Contains("educations"))
                return "educations";
            else if (collectionName.Contains("certifications"))
                return "certifications";
            else
                return "";
        }

        public string GenerateToken(string id, string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.JwtSecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", id),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                JwtSettings.JwtIssuer,
                JwtSettings.JwtAudience,
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string? GetUserIdFromToken(string token)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetTokenValidationParameters();
            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            var subClaim = claimsPrincipal.FindFirst("id");
            return subClaim?.Value;
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = JwtSettings.JwtIssuer,
                ValidAudience = JwtSettings.JwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.JwtSecretKey))
            };
        }
    }
}
