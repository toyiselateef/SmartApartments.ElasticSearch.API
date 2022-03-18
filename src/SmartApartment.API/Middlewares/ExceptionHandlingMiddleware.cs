
using Domain;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SmartApartment.API.Helper;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Middlewares
{
    public sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) => this.logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                 logger.LogError(e, e.Message);

            

                await HandleExceptionAsync(context, e);
            }
        }

        private  async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            
            httpContext.Response.ContentType = "application/json";


            httpContext.Response.StatusCode = exception switch
            {

                BadRequestException _ => StatusCodes.Status400BadRequest ,
                NotFoundException _ => StatusCodes.Status404NotFound,
                UnAuthorizedException _ => StatusCodes.Status401Unauthorized,
                ForbiddenException _ => StatusCodes.Status403Forbidden,
                UnProcessedEntityException _ => StatusCodes.Status422UnprocessableEntity,
                ServerErrorException _ => StatusCodes.Status500InternalServerError,
                _ => StatusCodes.Status500InternalServerError
            };

            Response<List<SearchResult>> APIresponse = new Response<List<SearchResult>>(ResponseTypes.invalid, null);

            logger.LogError(exception.Message);

            if (httpContext.Response.StatusCode == StatusCodes.Status500InternalServerError)
            {
                APIresponse= new Response<List<SearchResult>>(ResponseTypes.unknown, null);
            }
            else if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                APIresponse = new Response<List<SearchResult>>(ResponseTypes.notfound, null);
            }
            else if (httpContext.Response.StatusCode == StatusCodes.Status400BadRequest)
            {
                APIresponse = new Response<List<SearchResult>>(ResponseTypes.invalid, null);
            }

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(APIresponse));
        }
    }
}
