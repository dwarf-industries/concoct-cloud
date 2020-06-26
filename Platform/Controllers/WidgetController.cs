namespace Platform.Controllers {
    using System.Collections.Generic;
    using System.Data;
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

        public IActionResult LoadWidget(string phase, int queryId, int projectId) 
        {
            return ViewComponent(GetControlName(phase), new IncomingIdRequest{
                 Phase = phase,
                 UserId = queryId,
                 ProjectId = projectId
            });
        }

        [HttpGet]
        public IActionResult LoadQueryBinder(int projectId, string control, string field ) 
        {
            return ViewComponent("QueryBuilder", new IncomingIdRequest{
                __RequestVerificationToken  = control,
                Phase = field,
                ProjectId = projectId
            });
        }
        private string GetControlName(string control)
        {
            var result = string.Empty;
            switch(control)
            {
                case "AssignedToMe":
                    result = "AssignedToMe";
                break;
                case "BuildHistory":
                    result = "BuildHistory";
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
        public DataTable GetQueryDataById([FromBody] IncomingIdRequest request)
        {
            var result = new DataTable();
            using(var context = new DatabaseController(Context,Configuration))
            {
                result = context.GetUserQueryData(UserId,request.Id);
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