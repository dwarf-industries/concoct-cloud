namespace Platform.ViewComponents
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.Models;
    
    [ViewComponent(Name = "DesignerComponents")]
    public class DesignerViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;
        private  AutherizationManager AutherizationManager;
        private int UserId;
 
        public DesignerViewComponent(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId, httpContextAccessor.HttpContext.Request);
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            
          
            ViewData["User"] = UserId;
            return View("/Views/Shared/Components/DesignerComponents/Default.cshtml");
        }
    }
}