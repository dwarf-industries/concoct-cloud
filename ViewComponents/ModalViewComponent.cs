namespace Platform.ViewComponents
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control.Models;
    public class ModalViewComponent : ViewComponent
    {
        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;

        public ModalViewComponent(RokonocontrolContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(string childName)
        { 
            ViewData["RendeingComponent"] = childName;
            return View();
        }
    }
}