using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public sealed class AppRole : IdentityRole<Guid>
    {
        public AppRole()
        {

        }
        public AppRole(Role role)
        {
            Name = role.ToString();
            NormalizedName = role.ToString().ToUpper();
        }
    }
}
