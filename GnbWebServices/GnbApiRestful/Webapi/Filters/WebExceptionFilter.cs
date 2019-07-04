using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Webapi.Filters
{
    public class WebExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger logger;

        public WebExceptionFilter(ILogger<WebExceptionFilter> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            Exception exceprion = context.Exception;
            HttpStatusCode statusCode = GetStatusCode(context);
            LogException(exceprion, context.ActionDescriptor.DisplayName, statusCode);
            context.ExceptionHandled = true;

            // Evaluate different error types codes extending Exception class
            string result = JsonConvert.SerializeObject(new { error = exceprion.Message });
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;
            byte[] data = Encoding.UTF8.GetBytes(result);
            context.HttpContext.Response.Body.WriteAsync(data);
        }

        private void LogException(Exception exceprion, string displayName, HttpStatusCode statusCode)
        {
            string datetime = DateTime.Now.ToString("s");
            string header = $"{datetime}:{displayName}:Status Code '{(int)statusCode}:{statusCode.ToString()}'";

            // logger.LogError($"{datetime} >>>>>>>>>>>>>>>>>>>>>>>> START WEB APPLICATION ERROR >>>>>>>>>>>>>>>>>>>>>>");
            logger.LogError(exceprion, header, null);            
            // logger.LogError($"{datetime} <<<<<<<<<<<<<<<<<<<<< END WEB APPLICATION ERROR <<<<<<<<<<<<<<<<<<<<<");
        }

        private HttpStatusCode GetStatusCode(ExceptionContext context)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            
            if (context.Exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            else if (context.Exception is ArgumentNullException
                || context.Exception is ArgumentNullException
                || context.Exception is ArgumentOutOfRangeException
                || context.Exception is ValidationException)
            {
                statusCode = HttpStatusCode.BadRequest;
            }
            else if (context.Exception is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
            }
            else if (context.Exception is TimeoutException
                || context.Exception is ExternalException
                || context.Exception is COMException
                || context.Exception is SEHException)
            {
                statusCode = HttpStatusCode.GatewayTimeout;
            }

            return statusCode;
        }
    }
}