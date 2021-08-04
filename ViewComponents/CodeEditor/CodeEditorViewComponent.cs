using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Platform.DataHandlers;
using Platform.DataHandlers.Interfaces;
using Rokono_Control.DatabaseHandlers.Contexts;
using Rokono_Control.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rokono_Control.ViewComponents.CodeEditor
{
    [ViewComponent(Name = "CodeEditor")]

    public class CodeEditorViewComponent : ViewComponent
    {

        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;
        private AutherizationManager AutherizationManager;
        private int UserId;

        public CodeEditorViewComponent(RokonocontrolContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId, httpContextAccessor.HttpContext.Request);
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            if(request == null)
                return View("/Views/Shared/Components/CodeEditor/Default.cshtml");

            if(!string.IsNullOrEmpty(request.Phase))
            {
                ViewData["CurrentCommitDetails"] = request.Phase;
                return View("/Views/Shared/Components/CodeEditor/Default.cshtml");
            }
 
            return View("/Views/Shared/Components/CodeEditor/Default.cshtml");
        }
    }
}
