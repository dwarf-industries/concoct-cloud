using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.ViewComponents
{
    public class NotesViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration configuration;

        public NotesViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
        }

        public IViewComponentResult Invoke(int projectId)
        {
            
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var Id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context,configuration))
            {
                var Notes =  context.GetUserNotes(Id, projectId);
                ViewData["Notes"] = Notes;
                
            }
            return View();
        }
        
    }
}