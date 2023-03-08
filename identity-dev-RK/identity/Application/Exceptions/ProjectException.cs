using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ProjectException : Exception
    {
        public HttpStatusCode ErrorCode { get; }
        public object ExceptionData { get; protected set; }

        public ProjectException(string message, HttpStatusCode errorCode = HttpStatusCode.BadRequest, Exception innerException = null)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
}
