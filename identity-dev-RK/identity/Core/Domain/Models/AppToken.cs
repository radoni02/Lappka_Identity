using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class AppToken : IdentityUserToken<Guid>
    {
        public DateTime CreatedAt { get; private set; } =DateTime.UtcNow;
    }
}
