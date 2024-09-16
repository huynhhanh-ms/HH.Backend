using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Common
{
    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string message, string? errors = null)
            : base(message, errors, HttpStatusCode.Unauthorized)
        {
        }
    }
}
