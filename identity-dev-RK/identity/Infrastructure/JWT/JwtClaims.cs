using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.JWT
{
    public static class JwtClaims
    {
        public const string Email = "email";
        public const string NewEmail = "newEmail";
        public const string UserId = "nameid";
        public const string LoginProvider = "loginProvider";
    }
}
