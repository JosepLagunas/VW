using System.Collections.Generic;
using System.Linq;
using Laklp.Models;
using Laklp.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VueCliMiddleware;

namespace Laklp
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
            var domain = Configuration["Authentication:Auth0:Config:Domain"];
            var clientId = Configuration["Authentication:Auth0:Config:ClientId"];
            var clientSecret = Configuration["Authentication:Auth0:Config:ClientSecret"];

            services.AddAuth0Authentication(domain, clientId, clientSecret);

            services.AddHttpContextAccessor();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            app.Use((context, next) =>
            {
                if (context.Request.Headers["X-Forwarded-Proto"] == "https")
                {
                    context.Request.Scheme = "https";
                }

                return next();
            });
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            //unknown urls fallback, applies only based on first subpath (ex. '/home')
            //after that is the user is not validated is sent to login, and
            //redirected to /home
            app.UseWhen(context => IsNonValidPath(context.Request.Path),
                builder =>
                {
                    builder.UseMvc(routes =>
                    {
                        routes.MapSpaFallbackRoute(name: "spa-notfound-fallback",
                            defaults: new {controller = "NotFound", action = "HandleNotFound"});
                    });
                }).UseWhen(context => IsValidationRequired(context.Request.Path) &&
                                      !IsUserLogged(context),
                builder =>
                {
                    builder.UseMvc(routes =>
                    {
                        routes.MapSpaFallbackRoute(name: "spa-validation-fallback",
                            defaults: new
                            {
                                controller = "Validation",
                                action = $"HandleValidation",
                            });
                    });
                });

            //validation needed urls fallback, applies only based on first subpath (ex. '/home')
            //after that if the user is not validated is sent to login, and
            //redirected to requested url, so if the url doesn't exists will be handled by the
            //unknown url fallback.
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = @"..\ClientApp";

                if (env.IsDevelopment())
                {
                    // run npm process with client app
                    spa.UseVueCli(npmScript: "serve", port: 8080, regex: "Compiled ");
                    // if you just prefer to proxy requests from client app, use proxy to SPA dev server instead:
                    // app should be already running before starting a .NET client
                    //spa.UseProxyToSpaDevelopmentServer("http://localhost:8080"); // your Vue app port
                }
            });
        }

        private static bool IsUserLogged(HttpContext context)
        {
            return context.User.TryGetAccount(context, out _);
        }

        private static bool IsNonValidPath(PathString requestPath)
        {
            if (!requestPath.HasValue) return true;
            if (ValidPaths.Contains(requestPath.Value)) return false;
            var splitUrl = requestPath.Value.Split("/");
                                       
            var firstPathPart = $"/{splitUrl[1].ToLower()}";
            return !ValidPaths.Contains(firstPathPart);
        }

        private static bool IsValidationRequired(PathString requestPath)
        {
            if (!requestPath.HasValue) return false;
            if (AuthorizationRequiredPaths.Contains(requestPath.Value)) return true;
            var splitUrl = requestPath.Value.Split("/");

            var firstPathPart = $"/{splitUrl[1].ToLower()}";
            return AuthorizationRequiredPaths.Contains(firstPathPart);
        }

        private static readonly IEnumerable<string> ValidPaths = new List<string>()
            {"/", "/home", "/sockjs-node"};

        private static readonly IEnumerable<string> AuthorizationRequiredPaths =
            new List<string>() {"/home"};
    }
}