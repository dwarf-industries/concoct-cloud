namespace Platform.ViewComponents.SurveyComponents.SurveyDropdown
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Rokono_Control.Models;

    [ViewComponent(Name = "SurveyDropdownSettings")]
    public class SurveyDropdownSettingsViewComponent : ViewComponent
    {
        
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public SurveyDropdownSettingsViewComponent(RokonoControlContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }
        public IViewComponentResult Invoke(IncomingIdRequest data)
        {
            ViewData["Id"] = IdGenerator.GetRandomId();
            ViewData["QuestionId"] = IdGenerator.GetRandomId();
            ViewData["PageId"] = data.Id;
            ViewData["ComponentId"] = data.UserId;
            return View("/Views/Shared/Components/Survey/SurveyDropdown/Settings.cshtml");
        }
    }
}