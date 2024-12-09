using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy.TodoAppNTier.UI.GlobalExceptionLogs
{
    public class GlobalExceptionsLogsMiddleware
    {
        private readonly RequestDelegate _next;
        ILogger<GlobalExceptionsLogsMiddleware> _logger;



        //public GlobalExceptionsLogsMiddleware(RequestDelegate next, ILogger<GlobalExceptionsLogsMiddleware> logger)
        //{
        //    _next = next;
        //    _logger = logger;
        //}
        public GlobalExceptionsLogsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {

                //string message = "{Request} HTTP " + context.Request.Method + " -" + context.Request.Path;
                //message += " {Ip Address} " + context.Connection.RemoteIpAddress.MapToIPv4().ToString();
                //_logger.LogError(message);

                //message = "{Response} HTTP " + context.Request.Method + " -" + context.Request.Path + " responded " + context.Response.StatusCode;
                //message += " {Ip Address} " + context.Connection.RemoteIpAddress.MapToIPv4().ToString();

                //message += "\nException Message: " + exception.Message;
                //message += "\nStack Trace: " + exception.StackTrace;

                ////context.Response.ContentType = "text/plain";
                ////context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                ////await context.Response.WriteAsync(exception.Message);
                //_logger.LogError(message);




                Log
                    .ForContext("ErrorMessage", exception.Message)
                    .ForContext("ErrorTime", DateTime.Now)
                    .ForContext("PageName", context.Request.Path)
                    .ForContext("UserName", context.User.Identity?.Name ?? "Anonymous")
                    .Error(exception, "An error occurred. HTTP {Method} - {Path} responded {StatusCode} from {IpAddress}",
                           context.Request.Method,
                           context.Request.Path,
                           context.Response.StatusCode,
                           context.Connection.RemoteIpAddress?.MapToIPv4().ToString());
                context.Response.Redirect("/Error");



            }
        }
    }
}
