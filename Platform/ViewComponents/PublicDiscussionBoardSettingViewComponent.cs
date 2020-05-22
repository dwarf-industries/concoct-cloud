
namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;
    
    public class PublicDiscussionBoardSettingViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public PublicDiscussionBoardSettingViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(int projectId)
        {
            using(var context = new DatabaseController(Context, Configuration))
            {
                ViewData["RuleValuePublicMessage"] = context.GetProjectActiveRule(projectId,"PublicMessage");

                var result = context.GetProjectApiKey(projectId, "PublicMessage");
                ViewData["ProjectKeyPublicMessage"] = result;
                if(result == null)
                    return View("/Home/Error");
            }
            ViewData["ProjectId"] = projectId;
            return View();
        }
    }
}