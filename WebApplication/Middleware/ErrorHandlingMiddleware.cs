using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using WebApplication.Utils;

namespace WebApplication.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code;

            switch (exception)
            {
                case UnauthorizedAccessException _:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case ApplicationException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError; // 500 if unexpected
                    break;
            }

            var result = JsonConvert.SerializeObject(GetErrorStatus(exception.Message));
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int) code;
            return context.Response.WriteAsync(result);
        }

        private static Status GetErrorStatus(string message)
        {
            return new Status(message, false);
        }
    }
}