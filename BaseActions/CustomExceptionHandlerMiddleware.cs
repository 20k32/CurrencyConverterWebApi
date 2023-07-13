using Microsoft.AspNetCore.Http;
using FluentValidation;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;

namespace BaseActions
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate Next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
            Next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = System.Net.HttpStatusCode.InternalServerError;

            var result = string.Empty;

            switch (ex)
            {
                case ValidationException validationEx:
                    code = System.Net.HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationEx.Errors);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { Error = ex.Message });
            }

            await context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHanlderMiddlewareExtensions
    {
        public static IApplicationBuilder UseCutsomExcepionHandlerMiddleware(this IApplicationBuilder app) =>
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}
