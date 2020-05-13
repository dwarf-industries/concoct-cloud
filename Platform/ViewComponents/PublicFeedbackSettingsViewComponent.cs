using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.ViewComponents
{
    public class PublicFeedbackSettingsViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public PublicFeedbackSettingsViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(int projectId)
        {
            using(var context = new DatabaseController(Context, Configuration))
            {
                ViewData["RuleValueFeedbackPage"] = context.GetProjectActiveRule(projectId,"FeedbackPage");
                var result = context.GetProjectApiKey(projectId, "FeedbackPage");
                ViewData["ProjectKeyFeedback"] = result;
                if(result == null)
                    return View("/Home/Error");
             }
            ViewData["ProjectId"] = projectId;
            return View();
        }
    }
}