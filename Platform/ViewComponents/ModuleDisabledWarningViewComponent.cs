using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rokono_Control.Models;

namespace Platform.ViewComponents
{
    public class ModuleDisabledWarningViewComponent : ViewComponent 
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public ModuleDisabledWarningViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(int projectId)
        { 
            
            return View();
        }
    }
}