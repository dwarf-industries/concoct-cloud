using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.ViewComponents
{
    public class BugReportFormViewComponent : ViewComponent
    {
        
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public BugReportFormViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(int projectId)
        {
            using(var context = new DatabaseController(Context,Configuration))
            {
                ViewData["ProjectName"] = context.GetProjectName(projectId);
            }
            ViewData["ProjectId"] = projectId;
            return View();
        }
    }
}