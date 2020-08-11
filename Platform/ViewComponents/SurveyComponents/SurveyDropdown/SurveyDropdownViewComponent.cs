namespace Platform.ViewComponents.SurveyComponents.SurveyDropdown
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DataHandlers;
    using Rokono_Control.Models;

    [ViewComponent(Name = "SurveyDropdown")]
    public class SurveyDropdownViewComponent: ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public SurveyDropdownViewComponent(RokonoControlContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }
        public IViewComponentResult Invoke(int id)
        {
            ViewData["Id"] = IdGenerator.GetRandomId();
            return View("/Views/Shared/Components/Survey/SurveyDropdown/Default.cshtml");
        }
    }
}