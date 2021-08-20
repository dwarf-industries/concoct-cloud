namespace Platform.ViewComponents.Documentation
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.Models;

    [ViewComponent(Name = "DocumentationCategory")]
    public class DocumentationCategoryViewComponent : ViewComponent
    {
     

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            ViewData["ProjectId"] = request.Id;

            if (request.Data as UserRights != null)
            {
                var cData = request.Data as UserRights;
                ViewData["ManagementRights"] = cData.Documentation == 1 ? true : false;

            }
            else
                ViewData["ManagementRights"] = false;


            return View("/Views/Shared/Components/Documentation/DocumentationCategory/Default.cshtml");
        }
    }
}