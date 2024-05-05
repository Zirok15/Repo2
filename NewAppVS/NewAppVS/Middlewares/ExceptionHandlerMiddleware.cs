using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using UPB.BusinessLogic.Managers.Exceptions;

namespace NewAppVS.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                await ProcessError(httpContext, ex);
            }
        }

        public Task ProcessError(HttpContext context, Exception ex)
        {
            HttpStatusCode newStatusCode;
            switch (ex)
            {
                case BackingServiceException:
                    newStatusCode = HttpStatusCode.OK;
                    break;
                default:
                    newStatusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            /*var errorBody = new
            {
                Code = newStatusCode,
                Message = ex.Message,
            };*/

            context.Response.StatusCode = (int) newStatusCode;
            string errorBodyJSON = $"{{\r\n Code = {(int)newStatusCode},\r\n Message = {ex.Message},\r\n }};";
            return context.Response.WriteAsync(errorBodyJSON);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
