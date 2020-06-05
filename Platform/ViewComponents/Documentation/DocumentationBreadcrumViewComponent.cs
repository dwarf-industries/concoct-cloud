

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
                if(request.Id != 0)
                    ViewData["PageData"] = context.GetDocumentationCategoryName(request.Id);
                else
                {
                    var defaultCategory = context.GetDocumentationDefaultCategory(request.ProjectId);
                    ViewData["PageData"] =  context.GetDocumentationDefaultCategoryName(request.ProjectId); 
                }
                

            }
            return View("/Views/Shared/Components/Documentation/DocumentationBreadcrum/Default.cshtml");
        }
    }
}