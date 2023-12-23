using AuthReadyAPI.DataLayer.Models;
using AuthReadyAPI.DataLayer.Models.Middleware;
using AuthReadyAPI.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace AuthReadyAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {context.Request.Path}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            var details = new ErrorDetailsClass
            {
                Type = "Failure",
                Message = ex.Message
            };

            switch (ex)
            {
                case NotFound NFE:
                    statusCode = HttpStatusCode.NotFound;
                    details.Type = "Not Found";
                    break;

                default:
                    break;
            }

            string response = JsonConvert.SerializeObject(details);
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(response);
        }
    }
}
