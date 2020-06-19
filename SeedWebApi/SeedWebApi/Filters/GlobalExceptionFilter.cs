using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SeedWebApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SeedWebApi.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private ILogger<GlobalExceptionFilter> _logger;

        /// <summary>
        /// Accepts Logger 
        /// </summary>
        /// <param name="logger"></param>
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        private void WriteResponseContent(HttpResponse response, ApiResponse apiResponse)
        {
            var jsonResponseString = apiResponse.ToJsonStringFromObject<ApiResponse>();
            response.ContentType = "application/json";
            byte[] data = jsonResponseString.ToBytesUsingUTF8Encoding();
            response.Body.Write(data, 0, data.Length);
        }

        /// <inheritdoc />
        public override void OnException(ExceptionContext context)
        {
            ApiError apiError = null;
            var ex = context.Exception as CoreException;

            if (context.Exception is CoreException)
            {
                //Known Errors
                var apiResponse = new ApiResponse(ex.StatusCode, ex.Message, ex.Errors);
                context.HttpContext.Response.StatusCode = (int)ex.StatusCode;
                WriteResponseContent(context.HttpContext.Response, apiResponse);

                _logger.LogError(ex.Message);
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                apiError = new ApiError("Unauthorized Access");
                context.HttpContext.Response.StatusCode = 401;

                // handle logging here
                _logger.LogError(ex.Message);
            }
            else
            {
                //Unhandled Errors
#if !DEBUG
                    var msg = "An unhandled error occurred.";                
                    string stack = null;
#else
                var msg = context.Exception.GetBaseException().Message;
                string stack = context.Exception.StackTrace;
#endif

                apiError = new ApiError(msg);
                apiError.Detail = stack;

                var apiResponse = new ApiResponse(HttpStatusCode.InternalServerError, msg, new List<ApiError>() { apiError });
                context.HttpContext.Response.StatusCode = 500;
                WriteResponseContent(context.HttpContext.Response, apiResponse);

                // handle logging here
                _logger.LogError(msg);
            }

            base.OnException(context);
        }
    }
}
