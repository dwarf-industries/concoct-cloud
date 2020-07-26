namespace Platform.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.Models;

    public class SurveyController : Controller
    {
        RokonoControlContext Context {get; set;}
        IConfiguration Config { get; set;}
        AutherizationManager AutherizationManager {get; set;}
        private int UserId {get; set;}
        public SurveyController(RokonoControlContext context,IConfiguration currentConfig, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Config = currentConfig;
            AutherizationManager = (AutherizationManager) autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);

            
        }

        public IActionResult Index(int projectId) 
        {
            ViewData["ProjectId"] = projectId;
            return View();
        }

        
    }
}