using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Calender.Domain.Commands;
using Microsoft.AspNetCore.Http;

namespace Calender.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try { await next(httpContext); }
            catch (ValidationException exn)
            {
                var statusCode = HttpStatusCode.BadRequest;
                httpContext = SetErrorResponse(httpContext, statusCode);
                byte[] data = Encoding.UTF8.GetBytes(exn.ToString());
                httpContext.Response.ContentType = "application/text";
                await httpContext.Response.Body.WriteAsync(data);
            }
            catch (Exception exn)
            {
                var statusCode = HttpStatusCode.InternalServerError;
                httpContext = SetErrorResponse(httpContext, statusCode);

                await httpContext.Response.WriteAsync(statusCode.ToString());
            }
        }

        private static HttpContext SetErrorResponse
            (HttpContext httpContext, HttpStatusCode statusCode)
        {
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = (int)statusCode;
            httpContext.Response.ContentType = httpContext.Request.ContentType;

            return httpContext;
        }
    }
}
