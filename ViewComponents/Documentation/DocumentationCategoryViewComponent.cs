namespace Platform.ViewComponents.Documentation
{
    using Microsoft.AspNetCore.Mvc;

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