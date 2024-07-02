using System.Net;

namespace PI.Domain.Exceptions
{
    public class ValidateException : Exception
    {
   
        public HttpStatusCode StatusCode { get; }

        public ValidateException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public static void ThrowIfNull(object? obj, string message)
        {
            if (obj == null)
            {
                throw new ValidateException(message, HttpStatusCode.BadRequest);
            }
        }

        public static void ThrowIfNullOrEmpty(string? obj, string message)
        {
            if (string.IsNullOrEmpty(obj))
            {
                throw new ValidateException(message, HttpStatusCode.BadRequest);
            }
        }

        //public static void ThrowIfFalse(bool obj, string message)
        //{
        //    if (obj == false)
        //    {
        //        throw new ValidateException(message, HttpStatusCode.BadRequest);
        //    }
        //}

        public static void ThrowIf(bool obj, string message)
        {
            if (obj == true)
            {
                throw new ValidateException(message, HttpStatusCode.BadRequest);
            }
        }

        public override string ToString()
        {
            return base.ToString() + $"\nErrorMessages: {this.Message}";
        }
    }
}
