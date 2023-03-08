using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.TokenException
{
    public class InvalidConfirmEmailTokenException : ProjectException
    {
        public InvalidConfirmEmailTokenException() : base($"Unable to confirm email.") { }


    }
}
