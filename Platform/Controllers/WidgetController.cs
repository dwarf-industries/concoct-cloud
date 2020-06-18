namespace Platform.Controllers {
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.Models;

    public class WidgetController : Controller {
        RokonoControlContext Context;
        IConfiguration Configuration;
        AutherizationManager AutherizationManager { get; set; } 
        private int UserId { get; set; }

        public WidgetController (RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor) {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager) autherizationManager;
            UserId = AutherizationManager.GetCurrentUser (UserId, httpContextAccessor.HttpContext.Request);
        }
        public IActionResult Index () {
            return View ();
        }


        [HttpGet]
        public IActionResult GetBindingOptions(int projectId, string phase) 
        {
            return ViewComponent("DataBinder", new IncomingIdRequest{
                ProjectId = projectId,
                Phase = phase
            });
        }
        [HttpGet]

        public IActionResult LoadWidget(string phase) 
        {
            return ViewComponent("MapLabels", new IncomingIdRequest{
                 Phase = phase
            });
         }

    }
}