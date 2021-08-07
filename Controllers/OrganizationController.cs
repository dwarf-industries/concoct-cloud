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

        public OrganizationController(RokonocontrolContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId, httpContextAccessor.HttpContext.Request);
        }

        public IActionResult Index(string data)
        {
            using (var context = new DatabaseController(Context, Configuration))
                ViewData["ProjectId"] = context.GetProjectByOrganization(data);

            var buildData = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();
            ViewData["OrganizationName"] = data;
            ViewData["BuildVersion"] = buildData;

            return View();
        }
    }
}
