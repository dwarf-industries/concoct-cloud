namespace Rokono_Control
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Platform.Hubs;
    using Rokono_Control.DataHandlers.Implementations;
    using Rokono_Control.DataHandlers.Interfaces;
    using Rokono_Control.Models;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public StartupConfig StartConfiguration {get; set;}
        public IConfiguration Configuration;
        public static StartupConfig Config { get; set; }
 
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var fileData = File.ReadAllText("appsettings.Development.json");
            StartConfiguration = JsonConvert.DeserializeObject<StartupConfig>(fileData);
            Config = StartConfiguration;
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
            System.Console.WriteLine(StartConfiguration.ConnectionStrings.RokonocontrolContext);
            services.AddEntityFrameworkSqlServer()
         .  AddDbContext<RokonocontrolContext>((serviceProvider, options) =>
            options.UseSqlServer(StartConfiguration.ConnectionStrings.RokonocontrolContext)
                .UseInternalServiceProvider(serviceProvider));


 
            services.AddRazorPages();
            services.AddSignalR();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();
            services.AddScoped<IAutherizationManager, AutherizationManager>();
            services.AddScoped<ICustomLogger, Logger>();
            services.AddHttpContextAccessor();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            var projects = new List<Projects>();
 
            using (var context = new RokonocontrolContext())
            {
                projects = context.Projects.Include(x => x.Repository).ToList();
            }

            Task.Run(() => RepositoryManager.InitRepositories(projects, Program.ServerOS));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
              //  ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseExceptionHandler("/home/error");

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
                endpoint.MapControllerRoute(name: "Login",
                pattern: "/{*data}",
                defaults: new { controller = "Organization", action = "Index" });
            });

       }
    }
}

