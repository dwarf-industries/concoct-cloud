using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Platform.DataHandlers;
using Platform.DataHandlers.Interfaces;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rokono_Control.Controllers
{
    public class OrganizationController : Controller
    {
        RokonocontrolContext Context;
        IConfiguration Configuration;
        AutherizationManager AutherizationManager { get; set; }
        private int UserId { get; set; }
        HttpContext HttpContext { get; set; }

        public OrganizationController(RokonocontrolContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId, httpContextAccessor.HttpContext.Request);
            HttpContext = httpContextAccessor.HttpContext;
        }

        public IActionResult Index(string data)
        {
            using (var context = new DatabaseController(Context, Configuration))
            {
                var projectId = context.GetProjectByOrganization(data);
                ViewData["ProjectId"] = projectId;
                AutherizationManager.SignOut(HttpContext);

                if (projectId == 0)
                    return View("Error");

            }

            var buildData = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
            ViewData["OrganizationName"] = data;
            ViewData["BuildVersion"] = buildData;

            return View();
        }
    }
}
