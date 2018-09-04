using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Owin;
using ToDoList.Models;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNetCore.Identity;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoList.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;

[assembly: OwinStartup(typeof(ToDoList.Startup))]
namespace ToDoList
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }


        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Encrypt encrypt = new Encrypt();
            services.AddMvc();
            services.AddDbContext<ToDoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "mysite",
                    ValidAudience = "mysite",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qwertyuiopasdfghklljukuu"))
                };
            });
        }
       




        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory logger)
        {
            logger.AddConsole();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

           
            app.UseAuthentication();
            app.UseMvc();
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("why world!");
            });
        }
    }
}
