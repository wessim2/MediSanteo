using MediSanteo.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MediSanteo.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = requestDelegate;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Exception occured: {Message}", ex.Message);

                var exceptionDetails = GetExceptionDetails(ex);

                var problemDetails = new ProblemDetails
                {
                    Status = exceptionDetails.Status,
                    Type = exceptionDetails.Type,
                    Detail = exceptionDetails.Detail,
                    Title = exceptionDetails.Title,
                };

                if(exceptionDetails.Errors is not null)
                {
                    problemDetails.Extensions["errros"] = exceptionDetails.Errors;
                }

                context.Response.StatusCode = exceptionDetails.Status;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        private static ExceptionDetails GetExceptionDetails(Exception ex)
        {
            return ex switch
            {
                ValidationException validationException => new ExceptionDetails(
                    StatusCodes.Status400BadRequest,
                    "ValidationFailure",
                    "Validation Error",
                    "One or more validation errors has occured",
                    validationException.Errors
                ),
                _ => new ExceptionDetails(
                    StatusCodes.Status500InternalServerError,
                    "ServerError",
                    "Server Error",
                    "An expected server error has occured",
                    null
                    )
            };
        }

        internal record ExceptionDetails (
            int Status,
            string Type,
            string Title,
            string Detail,
            IEnumerable<object>? Errors
            );

    }
}
