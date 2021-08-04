namespace Platform.ViewComponents.SurveyComponents.SurveyInput
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    [ViewComponent(Name = "SurveyInput")]
    public class SurveyInputViewComponent : ViewComponent
    {
        
        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;

        public SurveyInputViewComponent(RokonocontrolContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }
        public IViewComponentResult Invoke(int id)
        {
            ViewData["Id"] = IdGenerator.GetRandomId();
            return View("/Views/Shared/Components/Survey/SurveyInput/Default.cshtml");
        }
    }
}