using Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Repositories
{
    public interface IIdentityRepository
    {
        Task<AppUser> FindByIdAsync(Guid id);

        Task UpdateAsync(AppUser user);

        Task<AppUser> FindByEmailAsync(string Email);
        Task<bool> CheckUserEmailAvailability(string Email);


    }
}
