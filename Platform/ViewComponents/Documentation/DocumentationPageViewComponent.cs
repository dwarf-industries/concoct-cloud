namespace Platform.ViewComponents.Documentation
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    [ViewComponent(Name = "DocumentationPage")]
    public class DocumentationPageViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public DocumentationPageViewComponent(RokonoControlContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }
        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            var currentUser = default(int);
            ViewData["ProjectId"] = request.ProjectId;
            currentUser = GetCurrentUser(currentUser);
            using (var context = new DatabaseController(Context, Configuration))
            {
                ValidateUserRights(request, currentUser, context);
                if (request.Id == 0)
                    request.Id = context.GetDocumentationDefaultCategory(request.ProjectId);
                ViewData["PageData"] = context.GetDocumentationPages(request);
            }
            ViewData["CategoryId"] = request.Id;
            return View("/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml");
        }

        private void ValidateUserRights(IncomingIdRequest request, int currentUser, DatabaseController context)
        {
            if (currentUser != default(int))
                ViewData["UserRights"] = context.GetUserRights(currentUser, request.ProjectId);
            else
                ViewData["UserRights"] = new UserRights
                {
                    ChatChannelsRule = 0,
                    Documentation = 0,
                    ManageIterations = 0,
                    ManageUserdays = 0,
                    UpdateUserRights = 0,
                    ViewOtherPeoplesWork = 0,
                    WorkItemRule = 0
                };
        }

        private int GetCurrentUser(int currentUser)
        {
            if (Request.HttpContext.User != null && Request.HttpContext.User.Claims != null && Request.HttpContext.User.Claims.Count() > 2)
            {
                var user = Request.HttpContext.User.Claims.ElementAt(1);
                currentUser = int.Parse(user.Value);
            }

            return currentUser;
        }
    }
}