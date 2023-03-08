using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.UserException
{
    public class CheckPasswordSignInException : ProjectException
    {
        public CheckPasswordSignInException(Exception inner = null) : base($"Wrong Password.") { }
    }
}
