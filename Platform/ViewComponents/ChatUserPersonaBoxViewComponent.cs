
namespace Platform.ViewComponents
{
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    public class ChatUserPersonaBoxViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;
        private  AutherizationManager AutherizationManager;
        private int UserId;
 
        public ChatUserPersonaBoxViewComponent(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            using(var context = new DatabaseController(Context,Configuration))
            {
                var rights = context.GetUserRights(UserId, request.ProjectId);
                ViewData["ProjectRight"] = rights;
                ViewData["ProjectId"] = request.ProjectId;
                ViewData["UserRights"] = context.GetUserChatRights(request.Id, request.ProjectId);
                ViewData["UserId"] = request.Id;
            }
            return View();
        }
    }
}