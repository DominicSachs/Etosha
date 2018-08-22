using Etosha.Server.Extensions;
using Etosha.Web.Api.Extensions;
using Etosha.Web.Api.Hubs;
using Etosha.Web.Api.Infrastructure.SampleData;
using Etosha.Web.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System.IO;
using Microsoft.AspNetCore.Mvc;

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
      var corsBuilder = new CorsPolicyBuilder();
      corsBuilder.AllowAnyHeader();
      corsBuilder.AllowAnyMethod();
      corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:52017", "https://etosha.azurewebsites.net");
      corsBuilder.AllowCredentials();
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
        .AddCors(setup =>
        {
          setup.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
        })
        .AddAuthorization()
        .AddDataAnnotations()
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors("SiteCorsPolicy");

      app.UseSignalR(routes =>
      {
        routes.MapHub<StockTickerHub>("/stocks");
      });

      app.Use(async (context, next) =>
      {
        await next();

        if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) &&
            !context.Request.Path.Value.StartsWith("/api/") && !context.Request.Path.Value.StartsWith("/negotiate"))
        {
          context.Request.Path = "/index.html";
          await next();
        }
      });

      app.UseFileServer();
      app.UseAuthentication();
      app.UseMvcWithDefaultRoute();
    }
  }
}
