namespace Platform.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Rokono_Control;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    public class UserListViewComponent : ViewComponent
    {

        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;

        public UserListViewComponent(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(int projectId)
        {
            
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var Id = int.Parse(user.Value);
            var result = new List<OutgoingUserAccounts>();
            using(var context = new DatabaseController(Context,Configuration))
            {
                var users = context.GetProjectUsers(projectId);
                users.ForEach(x =>{
                    if(Program.Members.Any(y => y.Name == x.Name) || x.AccountId == Id)
                    {
                        var current= x;
                        current.Online = true;
                        result.Add(current);
                    }
                    else
                        result.Add(x);
                });
                ViewData["UserList"] = result;
            }
            return View();
        }
        
    }
}