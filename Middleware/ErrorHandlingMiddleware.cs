using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BookManagement.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {

                await _next(context);

                #region Go Direct to view
                //if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
                //{
                //    context.Response.ContentType = "text/html";

                //    var notFoundPage = context.RequestServices.GetRequiredService<IWebHostEnvironment>()
                //                    .ContentRootPath + @"\views\shared\404.cshtml";

                //    await context.Response.WriteAsync(System.IO.File.ReadAllText(notFoundPage));
                //}
                #endregion

                #region Go with controller
                if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
                {
                    var currentUrl = context.Request.Path.Value;
                    var redirectUrl = $"/Home/NotFound?Page={currentUrl}";

                    context.Response.Redirect(redirectUrl);
                }
                #endregion
            }
            catch
            {
                #region Go Direct to View
                //context.Response.ContentType = "text/html";

                //var errorPage = context.RequestServices.GetRequiredService<IWebHostEnvironment>()
                //                .ContentRootPath + @"\views\shared\500.cshtml";

                //await context.Response.WriteAsync(System.IO.File.ReadAllText(errorPage));
                #endregion

                #region Go With Controller
                var currentUrl = context.Request.Path.Value;
                var redirectUrl = $"/Home/InternalServer?Page={currentUrl}";

                context.Response.Redirect(redirectUrl);
                #endregion

            }
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
