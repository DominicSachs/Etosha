using Etosha.Server.Extensions;
using Etosha.Web.Api.Extensions;
using Etosha.Web.Api.Hubs;
using Etosha.Web.Api.Infrastructure.SampleData;
using Etosha.Web.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Etosha.Web.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddEtoshaServices();
            services.AddEntityFramework(_configuration.GetConnectionString("DefaultConnection"));
            services.AddIdentityFramework(_configuration.GetSection("PasswordOptions").Get<PasswordOptions>());
            services.AddSingleton<IWebTokenBuilder, WebTokenBuilder>();
            services.AddJsonWebTokenConfiguration(_configuration);
            services.AddSignalR();
            services.AddScoped<StockTickerHub>();
            services.AddSingleton<StockTicker>();
            services.AddControllers();
            services.AddAuthentication();
            services.AddAuthorization();            
            services.AddTransient(s => s.GetService<IHttpContextAccessor>().HttpContext.User);
            services.AddSpaStaticFiles(config => config.RootPath = "client-app/dist" );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowCredentials().AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));
            app.UseRouting();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<StockTickerHub>("/stocks");
            });
            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}
