using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.TokenException
{
    public class InvalidRefreshTokenException : ProjectException
    {
        public InvalidRefreshTokenException() : base($"Invalid refresh token.") { }
    }
}
