using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Platform.DatabaseHandlers.Contexts;
using Platform.DataHandlers;
using Platform.DataHandlers.Interfaces;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace RokonoControl
{
    public class ChatHub : Hub
    {
        RokonoControlContext RokonoContext;
        IConfiguration Configuration;

        public AutherizationManager AutherizationManager { get; }
        public int UserId { get; }

        public ChatHub(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager,IHttpContextAccessor httpContextAccessor)
        {
            RokonoContext = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager) autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }


        [Authorize(Roles = "User")]
        public  Task Send( string message)
        {

            var user = Context.User;
            if (user != null)
            {
                var username = user.Claims.FirstOrDefault();// Call the broadcastMessage method to update clients.
                
                return Clients.Others.SendAsync("ReciveMessage", username.Value.ToString(), message);
            }
            return null;
        }

        [Authorize(Roles = "User")]
        public Task NotificationRecived()
        {
          var user = Context.User;
            if (user != null)
            {
                var res = string.Empty;
            
                using(var context = new NotificationContext(RokonoContext,Configuration))
                {
                    var notifications = context.GetNewNotifications(UserId);
                    res = JsonConvert.SerializeObject(notifications);
                }
                return Clients.Caller.SendAsync("ReciveNotification", res);
            }
            return null;
        }
    }
}