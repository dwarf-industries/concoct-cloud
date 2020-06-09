
namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.Models;

    public class ChatUserPersonaBoxViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;
        private  AutherizationManager AutherizationManager;
        private int UserId;
        private UserRights Rights;
        public ChatUserPersonaBoxViewComponent(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            using(var context = new UsersContext(Context,Configuration))
            {
                ViewData["GetChatRights"] = context.GetUserRights(UserId,request.ProjectId);
                Rights = context.GetUserRights(UserId, request.ProjectId);
            }
            using(var context = new ChatContext(Context,Configuration))
            {
                ViewData["ProjectRight"] = Rights;
                ViewData["ProjectId"] = request.ProjectId;
                ViewData["UserRights"] = context.GetUserChatRights(request.Id, request.ProjectId);
                ViewData["UserId"] = request.Id;
            }
            return View();
        }
    }
}