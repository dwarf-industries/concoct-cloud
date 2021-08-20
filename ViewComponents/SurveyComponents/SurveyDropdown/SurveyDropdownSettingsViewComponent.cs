namespace Platform.ViewComponents.SurveyComponents.SurveyDropdown
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Platform.Models;
    using Rokono_Control.Models;

    [ViewComponent(Name = "SurveyDropdownSettings")]
    public class SurveyDropdownSettingsViewComponent : ViewComponent
    {
        
        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;

        public SurveyDropdownSettingsViewComponent(RokonocontrolContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }
        public IViewComponentResult Invoke(GenericIdRequest current)
        {

            ViewData["Id"] = IdGenerator.GetRandomId();
            ViewData["QuestionId"] = IdGenerator.GetRandomId();//current.Data[1];
            ViewData["RenderedQuestionId"] = current.Data[0];
            ViewData["ComponentId"] = current.Data[1];
            ViewData["PageId"] = int.Parse(current.Data[3].ToString());
            ViewData["ParentId"] = current.Data[4];
            ViewData["NodeValue"] = current.Data[5];

            
            return View("/Views/Shared/Components/Survey/SurveyDropdown/Settings.cshtml");
        }
    }
}