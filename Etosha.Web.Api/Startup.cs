using Etosha.Server.Extensions;
using Etosha.Web.Api.Extensions;
using Etosha.Web.Api.Infrastructure.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Etosha.Web.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
      var corsBuilder = new CorsPolicyBuilder();
      corsBuilder.AllowAnyHeader();
      corsBuilder.AllowAnyMethod();
      corsBuilder.WithOrigins("http://localhost:4200");
      corsBuilder.AllowCredentials();

      services.AddMvcCore()
        .AddJsonFormatters()
        .AddCors(setup =>
        {
          setup.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
        })
        .AddAuthorization()
        .AddDataAnnotations();

      services.AddActionExecutor();
      services.AddEntityFramework(Configuration["ConnectionStrings:DefaultConnection"]);
      services.AddIdentityFramework(Configuration.GetSection("PasswordOptions").Get<PasswordOptions>());
      services.AddSingleton<IWebTokenBuilder, WebTokenBuilder>();
      services.AddJsonWebTokenConfiguration(Configuration);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      app.UseCors("SiteCorsPolicy");
      app.UseDefaultFiles();
      app.UseStaticFiles();

      app.Use(async (context, next) =>
      {
        await next();

        if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) && !context.Request.Path.Value.StartsWith("/api/"))
        {
          context.Request.Path = "/index.html";
          await next();
        }
      });

      app.UseAuthentication();
      app.UseMvcWithDefaultRoute();
    }
  }
}
