using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Udemy.TodoAppNTier.UI.GlobalExceptionLogs
{
    public class RequestLogging
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLogging> _logger;

        public RequestLogging(RequestDelegate next, ILogger<RequestLogging> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Kullanıcı bilgisi, istek türü, path vb. bilgileri al
            var userId = context.User.Identity?.Name ?? "Unknown";
            var requestType = context.Request.Method; // GET, POST, vb.
            var requestPath = context.Request.Path;

            // Loglama işlemi
            _logger.LogInformation("Request received: User={UserId}, Path={RequestPath}, Method={RequestType}",
                userId, requestPath, requestType);

            // Diğer middleware'lerin çalışmasını sağla
            await _next(context);
        }
    }
}
