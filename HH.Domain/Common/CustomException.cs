using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Common
{
    public class CustomException : Exception
    {
        public string? ErrorMessages { get; }

        public HttpStatusCode StatusCode { get; }

        public CustomException(string message, string? errors, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
            : base(message)
        {
            ErrorMessages = errors;
            StatusCode = statusCode;
        }
    }
}
