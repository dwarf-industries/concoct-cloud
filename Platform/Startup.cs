namespace Rokono_Control
{
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
    using Newtonsoft.Json;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Platform.Hubs;
    using Rokono_Control.Models;
    using RokonoControl;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public StartupConfig StartConfiguration {get; set;}
        public IConfiguration Configuration;

 
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var fileData = File.ReadAllText("appsettings.Development.json");
            StartConfiguration = JsonConvert.DeserializeObject<StartupConfig>(fileData);
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
            System.Console.WriteLine(StartConfiguration.ConnectionStrings.RokonoControlContext);
            services.AddDbContext<RokonoControlContext>(options =>
                options.UseSqlServer(StartConfiguration.ConnectionStrings.RokonoControlContext)
            );
            services.AddRazorPages();
            services.AddSignalR();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();
            services.AddScoped<IAutherizationManager, AutherizationManager>();
            services.AddHttpContextAccessor();
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
            app.UseCors("CorsPolicy");


            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();          
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapHub<MessageHub>("/messageHub");
                endpoint.MapRazorPages();
                endpoint.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                
            });

       }
    }
}

