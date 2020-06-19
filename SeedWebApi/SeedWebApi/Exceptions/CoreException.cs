using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SeedWebApi.Exceptions
{
    [Serializable]
    public class CoreException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public List<ApiError> Errors { get; set; }

        public CoreException() { StatusCode = HttpStatusCode.InternalServerError; }

        public CoreException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
        {
            StatusCode = statusCode;
        }
        public CoreException(string message, Exception inner, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message, inner)
        {
            StatusCode = statusCode;
        }

        protected CoreException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

}
