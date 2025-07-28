using Haelya.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Haelya.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Laisse la requête passer
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur non gérée est survenue.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code = exception switch
            {
                BrandNameAlreadyUsedException => HttpStatusCode.Conflict,
                CategoryNameAlreadyUsedException => HttpStatusCode.Conflict,
                NotFoundException => HttpStatusCode.NotFound,
                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                EmailAlreadyUsedException => HttpStatusCode.Conflict,
                UserNotFoundException => HttpStatusCode.NotFound,
                InvalidCredentialsException => HttpStatusCode.Unauthorized,
                IncorrectPasswordException => HttpStatusCode.Unauthorized,
                UnauthorizedActionException => HttpStatusCode.Forbidden,
                UserAlreadyDeletedException => HttpStatusCode.Gone,
                RefreshTokenNotFoundException => HttpStatusCode.NotFound,
                _ => HttpStatusCode.InternalServerError
            };

            var response = new
            {
                error = _env.IsDevelopment() ? exception.Message : "Une erreur interne est survenue.",
                type = _env.IsDevelopment() ? exception.GetType().Name : "InternalServerError"
            };

            var result = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
