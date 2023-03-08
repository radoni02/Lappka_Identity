using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Repositories
{
    public interface ITokenRepository
    {
        Task AddRefreshTokenAsync(AppToken token);
        Task RemoveRefreshTokenAsync(AppToken token);
        Task<AppToken> GetRefreshToken(string token, Guid userId);
    }
}
