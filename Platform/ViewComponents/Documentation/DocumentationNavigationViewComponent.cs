

namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.Models;

    [ViewComponent(Name = "DocumentationNavigation")]
    public class DocumentationNavigationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            ViewData["ProjectId"] = id;
            return View("/Views/Shared/Components/Documentation/DocumentationNavigation/Default.cshtml");
        }
    }
}