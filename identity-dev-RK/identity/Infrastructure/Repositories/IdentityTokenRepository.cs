using Core.Domain.Models;
using Core.Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class IdentityTokenRepository : ITokenRepository
    {
        private readonly ApplicationDbContext _db;

        public IdentityTokenRepository(ApplicationDbContext db)
        {
            _db = db;
        }
       

        public async Task AddRefreshTokenAsync(AppToken token)
        {
            await _db.appTokens.AddAsync(token);
            await _db.SaveChangesAsync();
        }

        public async Task RemoveRefreshTokenAsync(AppToken token)
        {
             _db.appTokens.Remove(token);
            await _db.SaveChangesAsync();
        }

        public async Task<AppToken> GetRefreshToken(string token, Guid userId)
        {
            return await _db.appTokens.FirstOrDefaultAsync(x => x.UserId==userId&& x.Value==token);
        }
    }
}
