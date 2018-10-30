using Etosha.Server.Extensions;
using Etosha.Web.Api.Extensions;
using Etosha.Web.Api.Hubs;
using Etosha.Web.Api.Infrastructure.SampleData;
using Etosha.Web.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace Etosha.Web.Api
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;

        public Startup(ILogger<Startup> logger, IConfiguration configuration)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEtoshaServices();
            services.AddEntityFramework(Configuration.GetConnectionString("DefaultConnection"));
            services.AddIdentityFramework(Configuration.GetSection("PasswordOptions").Get<PasswordOptions>());
            services.AddSingleton<IWebTokenBuilder, WebTokenBuilder>();
            services.AddJsonWebTokenConfiguration(Configuration);
            services.AddSignalR();
            services.AddScoped<StockTickerHub>();
            services.AddSingleton<StockTicker>();
            services.AddMvcCore()
              .AddJsonOptions(settings =>
              {
                  settings.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
              })
              .AddJsonFormatters()
              .AddAuthorization()
              .AddDataAnnotations()
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient(s => s.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = "client-app/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSignalR(routes =>
            {
                routes.MapHub<StockTickerHub>("/stocks");
            });

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseMvc();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
