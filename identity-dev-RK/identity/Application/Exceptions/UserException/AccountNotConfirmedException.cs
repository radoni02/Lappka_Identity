using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.UserException
{
    public class AccountNotConfirmedException : ProjectException
    {
        public AccountNotConfirmedException(Exception inner = null) : base($"Account not confirmed.",System.Net.HttpStatusCode.MethodNotAllowed) { }
    }
}
