using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.ViewComponents
{
    public class LoadedProjectsViewComponent : ViewComponent
    {
         private readonly RokonoControlContext Context;
         private readonly IConfiguration Configuration;

        public LoadedProjectsViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(int projectId)
        {
            
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var Id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context,Configuration))
            {
                var projects =  context.GetUserProjects(Id);
                var current = projects.FirstOrDefault(x=>x.Id == projectId);
                ViewData["Projects"] = projects;
                ViewData["SelectedIndex"] = projects.IndexOf(current);
     
            }
            return View();
        }
    }
}