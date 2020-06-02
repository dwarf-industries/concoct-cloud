using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.ViewComponents
{
    public class ViewChangelogViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public ViewChangelogViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(int changeLogId)
        {
            using(var context = new DatabaseController(Context,Configuration))
            {
                ViewData["ChangelogDetails"] = context.GetSpecificChangelog(changeLogId);
            }
            return View();
        }
    }
}