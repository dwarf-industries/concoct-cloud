namespace Platform.ViewComponents.Documentation
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.Models;

    [ViewComponent(Name = "DocumentationPage")]
    public class DocumentationPageViewComponent : ViewComponent
    {
        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;
        AutherizationManager AutherizationManager;
        int UserId;

        public DocumentationPageViewComponent(RokonocontrolContext context, IConfiguration configuration, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = configuration;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }
        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            ViewData["ProjectId"] = request.ProjectId;
            using (var context = new UsersContext(Context, Configuration))
                ViewData["UserRights"] = AutherizationManager.ValidateUserRights(request.ProjectId, UserId, context);
            using(var context = new DocumentationContext(Context,Configuration))
            {
                if (request.Id == 0)
                    request.Id = context.GetDocumentationDefaultCategory(request.ProjectId);
                ViewData["PageData"] = context.GetDocumentationPages(request);
            }
            ViewData["CategoryId"] = request.Id;
            return View("/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml");
        }
 
         
    }
}