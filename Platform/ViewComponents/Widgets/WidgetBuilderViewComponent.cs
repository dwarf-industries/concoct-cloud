namespace Platform.ViewComponents.Widgets
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    [ViewComponent(Name = "WidgetBuilder")]
    public class WidgetBuilderViewComponent : ViewComponent
    {
                   private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public WidgetBuilderViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke()
        {
           
            return View("/Views/Shared/Components/Widgets/WidgetBuilder/Default.cshtml");
        }
    }
}