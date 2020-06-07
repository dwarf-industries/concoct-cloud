namespace Platform.ViewComponents.Documentation
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    [ViewComponent(Name = "DocumentationPage")]
    public class DocumentationPageViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;
        AutherizationManager AutherizationManager;
        int UserId;

        public DocumentationPageViewComponent(RokonoControlContext context, IConfiguration configuration, IAutherizationManager autherizationManager)
        {
            Context = context;
            Configuration = configuration;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,Request);
        }
        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
             ViewData["ProjectId"] = request.ProjectId;
             using (var context = new DatabaseController(Context, Configuration))
            {
                ViewData["UserRights"] = AutherizationManager.ValidateUserRights(request.ProjectId, UserId, context);
                if (request.Id == 0)
                    request.Id = context.GetDocumentationDefaultCategory(request.ProjectId);
                ViewData["PageData"] = context.GetDocumentationPages(request);
            }
            ViewData["CategoryId"] = request.Id;
            return View("/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml");
        }
 
         
    }
}