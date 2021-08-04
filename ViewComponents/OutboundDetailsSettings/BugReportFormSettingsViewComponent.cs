namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Rokono_Control.Models;
    [ViewComponent(Name = "BugReportFormSettings")]

    public class BugReportFormSettingsViewComponent : ViewComponent
    {
        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;

        public BugReportFormSettingsViewComponent(RokonocontrolContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(int projectId)
        {
            using(var context = new OutboundDetailsContext(Context,Configuration))
                ViewData["RuleValueBugReport"] = context.GetProjectActiveRule(projectId,"BugReport");

            using(var context = new ApiKeysContext(Context, Configuration))
            {
                var result = context.GetProjectApiKey(projectId, "BugReport");
                ViewData["ProjectKey"] = result;
                if(result == null)
                    return View("/Home/Error");
            }
            ViewData["ProjectId"] = projectId;
            return View("/Views/Shared/Components/OutboundDetailsSettings/BugReportFormSettings/Default.cshtml");
        }
    }
}