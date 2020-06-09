namespace Platform.ViewComponents
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control;
    using Rokono_Control.Models;

    public class UserListViewComponent : ViewComponent
    {

        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;
        private  AutherizationManager AutherizationManager;
        private int UserId;
 
        public UserListViewComponent(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }

        public IViewComponentResult Invoke(int projectId)
        {
            var result = new List<OutgoingUserAccounts>();
            using(var context = new UsersContext(Context,Configuration))
            {
                var users = context.GetProjectUsers(projectId);
                users.ForEach(x =>{
                    if(Program.Members.Any(y => y.Name == x.Name) || x.AccountId == UserId)
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