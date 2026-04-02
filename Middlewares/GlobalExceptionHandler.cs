using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace PetAPI.Middlewares;

public static class GlobalExceptionHandler
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var error = contextFeature.Error;

                    Console.WriteLine($"[ERROR] {error.Message} | {error.StackTrace}");

                    var statusCode = error switch
                    {
                        ArgumentException => (int)HttpStatusCode.BadRequest,
                        InvalidOperationException => (int)HttpStatusCode.Conflict,
                        KeyNotFoundException => (int)HttpStatusCode.NotFound,
                        _ => (int)HttpStatusCode.InternalServerError
                    };

                    context.Response.StatusCode = statusCode;

                    var response = new
                    {
                        StatusCode = statusCode,
                        Message = error.Message,
                        Details = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development" ? error.StackTrace : null
                    };

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                }
            });
        });
    }
}