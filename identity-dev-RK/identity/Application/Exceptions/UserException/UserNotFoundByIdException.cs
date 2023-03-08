using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.UserException
{
    public class UserNotFoundByIdException : ProjectException
    {
        public UserNotFoundByIdException(Guid id, Exception inner = null) 
            : base($"User with id:{id} not found.", System.Net.HttpStatusCode.NotFound) { }
    }
}
