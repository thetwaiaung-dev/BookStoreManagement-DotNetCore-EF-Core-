using BookManagement.Helper;
using BookManagement.Localize;
using BookManagement.Middleware;
using BookManagement.Repositories;
using BookManagement.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            const string defaultCulture = "en-US";

            var supportedCultures = new[]
            {
                new CultureInfo(defaultCulture),
                new CultureInfo("en-AU"),
            };

            services.Configure<RequestLocalizationOptions>(options =>
            {
                //Add cookie provider
                options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());

                options.DefaultRequestCulture = new RequestCulture(defaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.AddTransient<ISharedViewLocalizer, SharedViewLocalizer>();

            services.AddControllersWithViews();

            services.AddDbContext<BookDbContext>(opt => opt.UseSqlServer
                                                            (Configuration.GetConnectionString("con")),
                                                            ServiceLifetime.Transient,
                                                            ServiceLifetime.Transient);
            services.AddTransient<BookService>();
            services.AddTransient<CategoryService>();
            services.AddTransient<PageService>();
            services.AddTransient<AuthorService>();
            services.AddTransient<LogService>();
            services.AddTransient<LogHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();

            Log.Logger = new LoggerConfiguration()
                             .MinimumLevel.Information()
                             .WriteTo.File("logs/bookLog.txt", rollingInterval: RollingInterval.Hour)
                             .CreateLogger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            /* Localization */
            RequestLocalizationOptions options = (app.ApplicationServices.GetRequiredService
               <IOptions<RequestLocalizationOptions>>().Value);
            app.UseRequestLocalization(options);

            /* Error Handling */
            app.UseErrorHandlingMiddleware();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Book}/{action=Index}/{id?}");
            });
        }
    }
}
