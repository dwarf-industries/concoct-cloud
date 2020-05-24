
namespace Platform.ViewComponents
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;
    
    public class ManagementRightsViewComponent : ViewComponent
    {
        

        public ManagementRightsViewComponent()
        {

        }

        public IViewComponentResult Invoke(int projectId)
        {
            var rights =  Request.HttpContext.User.Claims.ElementAt(2).Value;
            ViewData["IsAdmin"] = int.Parse(rights);
            return View();
        }
    }
}