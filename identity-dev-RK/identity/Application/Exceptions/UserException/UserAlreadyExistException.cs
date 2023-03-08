using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.UserException
{
    public class UserAlreadyExistException : ProjectException
    {
        public UserAlreadyExistException(string email) : base($"Email have already been taken.",System.Net.HttpStatusCode.NotAcceptable) { }
    }
}
