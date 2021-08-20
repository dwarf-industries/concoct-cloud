
namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.Models;
    [ViewComponent(Name = "ChatWIndow")]

    public class ChatWIndowViewComponent : ViewComponent
    {
        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;
        private  AutherizationManager AutherizationManager;
        private int UserId;

        public ChatWIndowViewComponent(RokonocontrolContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            
            ViewData["TransferId"] = request.Id;
            ViewData["ProjectId"] = request.ProjectId;
            ViewData["IsPersonal"] = request.UserId;
            using(var context = new ChatContext(Context,Configuration))
            {
                ViewData["ChatMessages"] = context.GetCannelMessages(request.Id, request.UserId, UserId);
            }
            return View("/Views/Shared/Components/ChatComponents/ChatWIndow/Default.cshtml");
        }
    }
}