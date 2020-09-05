namespace Platform.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Platform.Models;
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

        public IActionResult CreateNewSurvey(int projectId) 
        {
            ViewData["ProjectId"] = projectId;
            using(var context = new SurveyContext(Context,Config))
                ViewData["BuildingBlocks"] = context.GetSurveyComponents();
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetProjectSurveyElement(string element)
        { 
            
            var id = default(int);
            if(element.Contains("_"))
                int.TryParse(element.Split("_")[1], out id);
            var componentName = string.Empty;
            using(var context = new SurveyContext(Context,Config))
            {
                var component = context.GetSurveyComponents().FirstOrDefault(x=>x.Id == id);
                componentName = component != null ? component.ComponentInternalName : ""; 
            }
            if(!string.IsNullOrEmpty(componentName))
                return await Task.Run(() => ViewComponent(componentName, new IncomingIdRequest{
                    
                }));
             
            return await Task.Run(() => ViewComponent("NullComponent", new IncomingIdRequest{
                 
            }));
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectSurveyElementSetting(string element)
        { 
            
            var id = default(int);
            var currentElement = element.Split(';');

            if(currentElement[0].Contains("_"))
                int.TryParse(currentElement[0].Split("_")[1], out id);
            var componentName = string.Empty;
            var componentId = default(int);
            using(var context = new SurveyContext(Context,Config))
            {
                var component = context.GetSurveyComponents().FirstOrDefault(x=>x.Id == id);
                componentName = component != null ? component.ComponentInternalName : ""; 
                componentId = component != null ? component.Id : default(int); 
            }
            if(!string.IsNullOrEmpty(componentName))
                return await Task.Run(() => ViewComponent($"{componentName}Settings", new IncomingIdRequest{
                    Id = int.Parse(currentElement[1]),
                    UserId = componentId
                }));
             
            return await Task.Run(() => ViewComponent("NullComponent", new IncomingIdRequest{
                 
            }));
        }

        
        [HttpPost]
        public async Task<List<Surveys>> GetProjectSurveys([FromBody] IncomingIdRequest request)
        {
            var result = default(List<Surveys>);
            using(var context = new SurveyContext(Context,Config))
            {
                result = await context.GetProjectSurveys(request.ProjectId);                
            }
            return result;
        }

        [HttpPost]
        public async Task<OutgoingJsonData> SaveSurveyComponent([FromBody] AssociatedPageSurveyComponents request)
        {
            
            using(var context = new SurveyContext(Context,Config))
            {
                await Task.Run(() =>  context.SaveSurveyComponent(request));              
            }
            return new OutgoingJsonData{ Data = "Success"};
        }

        
    }
}