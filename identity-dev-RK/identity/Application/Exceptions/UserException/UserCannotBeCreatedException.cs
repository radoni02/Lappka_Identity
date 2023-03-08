using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.UserException
{
    public class UserCannotBeCreatedException : ProjectException
    {
        public UserCannotBeCreatedException() : base($"User cannot be created") { }
    }
}
