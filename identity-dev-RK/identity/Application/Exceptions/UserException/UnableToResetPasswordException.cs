using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.UserException
{
    public class UnableToResetPasswordException : ProjectException
    {
        public UnableToResetPasswordException() : base($"Unable to reset password.") { }
    }
}
