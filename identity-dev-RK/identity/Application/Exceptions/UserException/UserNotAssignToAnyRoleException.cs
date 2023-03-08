using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.UserException
{
    public class UserNotAssignToAnyRoleException : ProjectException
    {
        public UserNotAssignToAnyRoleException(Exception inner = null) : base($"User not assign to any role.",System.Net.HttpStatusCode.MethodNotAllowed) {  }
    }
}
