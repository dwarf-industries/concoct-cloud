namespace Platform.ViewComponents.SurveyComponents.SurveyInput
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Rokono_Control.Models;

    [ViewComponent(Name = "SurveyInputSettings")]
    public class SurveyInputSettingsViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public SurveyInputSettingsViewComponent(RokonoControlContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }
        public IViewComponentResult Invoke(int id)
        {
            ViewData["Id"] = IdGenerator.GetRandomId();
            return View("/Views/Shared/Components/Survey/SurveyInput/Settings.cshtml");
        }
    }
}