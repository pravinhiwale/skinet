using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
            /// RequestDelegate
            // IHostEnvironment to check if we are Dev environment
        }

        //use middleware method
        public async Task InvokeAsync(HttpContext context){
            try {
                await _next(context);

                /*now if there is no exception we want the middleware to move on to the next piece of middleware.
                So what we'll do is we'll use next and in the try we'll say await next and then pass in the context.
                And this means if there is no exception then the request moves on to its next stage.
                */
            }
            catch(Exception ex){
                /*
                Now if there is an exception then we want to catch it.And this is where we can use our own exception handling response.
                Now our logging system is just the console and we will see it inside then if we get an error and what
                we'll also do is write our own response into the context response so that we can send it to the client.
                */
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType="application/json";
                context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                var response = _env.IsDevelopment() 
                            ? new ApiException((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString())
                            : new ApiException((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
                var json = JsonSerializer.Serialize(response,options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}