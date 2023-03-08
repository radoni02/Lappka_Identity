using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.UserException
{
    public class UserNotFoundByEmailException : ProjectException
    {
        public UserNotFoundByEmailException(string email,Exception inner = null) 
            : base($"User with email:{email} not found.",HttpStatusCode.NotFound)
        {
        }
    }
}
