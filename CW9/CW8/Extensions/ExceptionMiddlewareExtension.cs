using Microsoft.AspNetCore.Diagnostics;
using System.Runtime.CompilerServices;

namespace CW9.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder appbuilder)
        {
            appbuilder.UseExceptionHandler(apperror =>
            {
                apperror.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await File.AppendAllTextAsync("./logs.txt", contextFeature.Error.ToString() + "\n");
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Error");
                    }
                });
            });

        }
    }
}
