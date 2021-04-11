namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Rokono_Control.Models;
    [ViewComponent(Name = "PublicFeedbackSettings")]

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
            using(var context = new OutboundDetailsContext(Context,Configuration))
                ViewData["RuleValueFeedbackPage"] = context.GetProjectActiveRule(projectId,"FeedbackPage");

            using(var context = new ApiKeysContext(Context, Configuration))
            {
                var result = context.GetProjectApiKey(projectId, "FeedbackPage");
                ViewData["ProjectKeyFeedback"] = result;
                if(result == null)
                    return View("/Home/Error");
             }
            ViewData["ProjectId"] = projectId;
            return View("/Views/Shared/Components/OutboundDetailsSettings/PublicFeedbackSettings/Default.cshtml");
        }
    }
}