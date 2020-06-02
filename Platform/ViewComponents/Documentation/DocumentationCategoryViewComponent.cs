using Microsoft.AspNetCore.Mvc;

namespace Platform.ViewComponents.Documentation
{
    [ViewComponent(Name = "DocumentationCategory")]
    public class DocumentationCategoryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            ViewData["ProjectId"] = id;
            return View("/Views/Shared/Components/Documentation/DocumentationCategory/Default.cshtml");
        }
    }
}