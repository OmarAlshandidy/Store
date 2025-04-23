﻿using Domain.Exceptions;
using Shared.ErrorModels;

namespace Store.G04.Api.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next,ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
          _logger = logger;
        }

        public async Task InvokeAsync (HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if(context.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    await HandlingNotFoundEndPointAsync(context);
                }
            }
            catch (Exception  ex)
            {
                // log Execption 
                _logger.LogError(ex, ex.Message);
                await HandlingErrorAsync(context, ex);

            }
            }

        private static async Task HandlingErrorAsync(HttpContext context, Exception ex)
        {
            // 1. Set Statue Code For Response
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            // 2. Set Content Type Code For Response
            context.Response.ContentType = "application/json";
            // 3. Response Object (Body)
            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                ErrorMessage = ex.Message
            };
            response.StatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                BadHttpRequestException => StatusCodes.Status400BadRequest,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ValidationException => HandlingValidationException((ValidationException)ex, response),


                _ => StatusCodes.Status500InternalServerError
            };
            context.Response.StatusCode = response.StatusCode;
            //4. Return Response
            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task HandlingNotFoundEndPointAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status404NotFound,
                ErrorMessage = $"End Point{context.Request.Path} is Not Found"
            };
            await context.Response.WriteAsJsonAsync(response);
        }
        private static int HandlingValidationException(ValidationException ex ,ErrorDetails response )
        {
            response.Errors = ex.Errors;
            return StatusCodes.Status400BadRequest;
        }

    }
}
