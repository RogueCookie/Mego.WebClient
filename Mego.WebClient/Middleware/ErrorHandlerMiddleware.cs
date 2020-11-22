using System;
using System.Threading.Tasks;
using Mego.Database.Models.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Mego.WebClient.Middleware
{
    /// <summary>
    /// This layer intercepts and processes all errors 
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// The constructor of this class by convention receives a delegate request 
        /// </summary>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// By convention, for a given middleware, there is invoke method that takes a context and whatever from DI
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger">Log message from some service</param>
        /// <returns>Specific error type with message</returns>
        public async Task Invoke(HttpContext context, ILogger<ErrorHandlerMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                HandleException(context, exception, logger);
            }
        }

        /// <summary>
        /// Compare what kind of exception we have and build response we are interesting in
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception">Exception which occurred</param>
        /// <param name="logger">Log message from service</param>
        /// <returns>Particular error with log message</returns>
        private static void HandleException(HttpContext context, Exception exception, ILogger<ErrorHandlerMiddleware> logger)
        {
            logger.LogError(exception.Message);


            if (exception is ItemNotFoundException)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}