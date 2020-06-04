

namespace Platform.ViewComponents.Documentation 
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

        [ViewComponent(Name = "DocumentationBreadcrum")]

    public class DocumentationBreadcrumViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public DocumentationBreadcrumViewComponent(RokonoControlContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }
        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            ViewData["ProjectId"] = request.ProjectId;
            using(var context = new DatabaseController(Context, Configuration))
            {
                ViewData["PageData"] = context.GetDocumentationPage(request.Id); 
            }
            return View("/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml");
        }
    }
}