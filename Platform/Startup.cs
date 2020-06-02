using System;
using System.Collections.Generic;
using System.IO;
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
using Platform.DataHandlers;
using Platform.Hubs;
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
            services.AddCors(o => o.AddPolicy("ApiPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("*")
                    .AllowAnyOrigin();
            }));
            services.AddDbContext<RokonoControlContext>(options =>
                options.UseSqlServer("Server=84.54.185.144;Database=RokonoControl;User ID=RGSOC;Password='EmkV3*eRjVbZ0!KpKlDHX';")
            );
            services.AddRazorPages();
            services.AddSignalR();
             services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
              //  ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles();
          

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();          
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapHub<MessageHub>("/messageHub");
                endpoint.MapRazorPages();
                endpoint.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                
            });
            app.UseCors("ApiPolicy");

       }
    }
}

