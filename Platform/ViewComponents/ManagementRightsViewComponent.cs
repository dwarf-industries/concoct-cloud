using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.ViewComponents
{
    public class ManagementRightsViewComponent : ViewComponent
    {
        

        public ManagementRightsViewComponent()
        {

        }

        public IViewComponentResult Invoke(int projectId)
        {
            var rights =  Request.HttpContext.User.Claims.LastOrDefault().Value;
            ViewData["IsAdmin"] = int.Parse(rights);
            return View();
        }
    }
}