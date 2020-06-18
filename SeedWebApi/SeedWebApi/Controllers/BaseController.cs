using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SeedWebApi.Controllers
{
    [Produces("application/json")]
    public class BaseController : Controller
    {
        protected IHostingEnvironment _hostingEnvironment;
        public BaseController(IHostingEnvironment hostingEnvironment)
        {
            if (hostingEnvironment == null) throw new ArgumentException("Invalid argument HostingEnvironment");
            _hostingEnvironment = hostingEnvironment;
        }

        protected async Task<IActionResult> ExecuteAsync<T>(Func<Task<T>> function, string errorMessage)
        {
            async Task<IActionResult> FunctionCall()
            {
                var result = await function();
                if (result == null)
                {
                    return NoContent();
                }

                return Ok(result);
            }

            return await ExecuteFunctionAsync(FunctionCall, errorMessage);
        }

        private async Task<IActionResult> ExecuteFunctionAsync(Func<Task<IActionResult>> function, string errorMessage)
        {
            try
            {
                return await function();
            }
            catch (Exception exception)
            {
                return CustomObjectResult(exception);
            }
        }

        protected ObjectResult CustomObjectResult(Exception exception)
        {
            var error = new
            {
                message = exception.Message
            };
            
            //NOTE: Custom Exception object can be built here and accordingly set it's StatusCode as desired
            return new ObjectResult(error)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
