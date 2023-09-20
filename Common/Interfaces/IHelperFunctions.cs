using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IHelperFunctions
    {
        string EvaluateCollectionName<T>();
        string GenerateToken(string id, string email);
        string? GetUserIdFromToken(string token);
        TokenValidationParameters GetTokenValidationParameters();
    }
}
