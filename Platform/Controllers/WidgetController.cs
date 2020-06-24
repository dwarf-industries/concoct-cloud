namespace Platform.Controllers {
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Platform.Models;
    using Rokono_Control.DatabaseHandlers;
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
            return ViewComponent(GetControlName(phase), new IncomingIdRequest{
                 Phase = phase
            });
        }
        private string GetControlName(string control)
        {
            var result = string.Empty;
            switch(control)
            {
                case "Map":
                    result = "MapLabels";
                break;
                case "Grid":
                    result = "GridControl";
                break;
            }
            return result;
        }

        [HttpPost]
        public List<BindingQueryProperty> GetQueryProperties([FromBody] IncomingIdRequest request)
        {
            var result = new List<BindingQueryProperty>();
            using(var context = new DatabaseController(Context,Configuration))
            {
                result = context.GetTableProperties(request.Phase);
            }
            return result;
        }

        [HttpPost]
        public List<object> GetQueryDataById([FromBody] IncomingIdRequest request)
        {
            var result = new List<object>();
            using(var context = new DatabaseController(Context,Configuration))
            {
                result = context.GetUserQueryData<object>(UserId,request.Id);
            }
            return result;
        }

        [HttpPost]
        public int GetQueryData([FromBody] IncomingWidgetCreatorRequest request)
        {
            var result = default(int);
            using(var context = new DatabaseController(Context,Configuration))
            {
                result = context.AddUserQuery(request, UserId);
            }
            return result;
        }
    }
}