using BookManagement.Models;
using BookManagement.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace BookManagement.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LogService _logService;

        public ErrorHandlingMiddleware(RequestDelegate next, LogService logService)
        {
            _next = next;
            _logService = logService;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                #region for db log
                //Log log = new Log()
                //{
                //    ResponseCode = context.Response.StatusCode,
                //    ResponseUrl = context.Request.Path.Value
                //};
                //int result = _logService.Create(log);

                //if (result == 0)
                //{
                //    var redirectUrl = $"/Home/InternalServer?Page={context.Request.Path.Value}";
                //    context.Response.Redirect(redirectUrl);
                //}
                #endregion

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
                   // context.Response.HttpContext.Session.SetString("NotFoundMessage", currentUrl);
                    context.Response.Redirect(redirectUrl);
                }
                #endregion
            }
            catch
            {
                #region for db log
                //Log log = new Log()
                //{
                //    ResponseCode = 500,
                //    ResponseUrl = context.Request.Path.Value
                //};
                //int result = _logService.Create(log);

                //if (result == 0)
                //{
                //    var url = $"/Home/InternalServer?Page={context.Request.Path.Value}";
                //    context.Response.Redirect(url);
                //}
                #endregion

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
