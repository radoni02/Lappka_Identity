using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions.NotificationException
{
    public class FailToSendEmailException : ProjectException
    {
        public FailToSendEmailException(string description, Exception inner = null, HttpStatusCode code = HttpStatusCode.InternalServerError)
            : base($"Failed to send an email. " + description, code) { }
    }
}
