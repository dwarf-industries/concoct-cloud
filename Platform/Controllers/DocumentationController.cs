using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rokono_Control.Models;

namespace Platform.Controllers
{
    public class DocumentationController : Controller
    {
        RokonoControlContext Context;
        IConfiguration Configuration;

        public DocumentationController(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}