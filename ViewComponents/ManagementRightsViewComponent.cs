
namespace Platform.ViewComponents
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;

    public class ManagementRightsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int projectId)
        {
            var rights =  Request.HttpContext.User.Claims.ElementAt(2).Value;
            ViewData["IsAdmin"] = int.Parse(rights);
            return View();
        }
    }
}