using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.ViewComponents.LayoutComponents
{
    [ViewComponent(Name = "GetProjectLogo")]
    public class GetActiveProjectLogoViewComponent : ViewComponent
    {
        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;

        public GetActiveProjectLogoViewComponent(RokonocontrolContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }
        public IViewComponentResult Invoke(int id)
        {
            using(var context = new DatabaseController(Context,Configuration))
            {
                var projectDetails = context.GetProjectData(id);
                ViewData["ImageLocation"] = projectDetails.ImageLocation;
            }
            return View("/Views/Shared/Components/LayoutComponents/ProjectSpecific/ProjectImage.cshtml");
        }
    }
}