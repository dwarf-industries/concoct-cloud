using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rokono_Control.Models;
using RokonoControl;

namespace Rokono_Control
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddDbContext<RokonoControlContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("RokonoControlContext")));
            services.AddRazorPages();
            
            services.AddSignalR();
             services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();
 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
              //  ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
          

            app.UseSignalR(x => x.MapHub<ChatHub>("/ChatHub"));
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();          

            app.UseEndpoints(endpoint =>
            {
                endpoint.MapRazorPages();
                endpoint.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                
            });
       }
    }
}

