using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.TokenException
{
    public class NullPasswordResetTokenException : ProjectException
    {
        public NullPasswordResetTokenException() : base($"Token cannot be null.") { }
    }
}
