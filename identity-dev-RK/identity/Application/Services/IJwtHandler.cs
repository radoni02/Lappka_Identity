using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IJwtHandler
    {
        string CreateRefreshToken();
        string CreateAccessToken(Guid userId, string role);
        TokenValidationParameters Parameters { get; }
        JwtSecurityToken DecodeToken(string token);
        Guid ReadAccessToken(string token);
        (string, Guid) ReadAccessTokenForEmail(string token);
        int GetExpiredDays();
        string GenerateConfirmUpdateEmailToken(Guid id, string newEmail);
    }
}
