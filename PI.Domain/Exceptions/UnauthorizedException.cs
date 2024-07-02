using System.Net;

namespace PI.Domain.Exceptions
{
    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string message, string? errors = null)
            : base(message, errors, HttpStatusCode.Unauthorized)
        {
        }
    }
}