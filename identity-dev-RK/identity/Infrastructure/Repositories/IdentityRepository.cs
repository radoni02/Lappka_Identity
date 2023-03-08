using Core.Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly ApplicationDbContext _db;

        public IdentityRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<AppUser> FindByIdAsync(Guid id)
        {
            var user = await _db.appUsers.FindAsync(id);
            return user;
        }
        public async Task UpdateAsync(AppUser user)
        {
            _db.appUsers.Update(user);
            await _db.SaveChangesAsync();
        }
        public async Task<AppUser> FindByEmailAsync(string Email)
        {
            var user = await _db.appUsers.FirstOrDefaultAsync(a => a.Email == Email);
            return user;
        }
        public async Task<bool> CheckUserEmailAvailability(string Email)
        {
            var user = await _db.appUsers.FirstOrDefaultAsync(a => a.Email == Email);
            if(user is null)
            {
                return true;
            }
            return false;
        }

    }
}
