using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace RokonoControl
{
    public class ChatHub : Hub
    {
        RokonoControlContext RokonoContext;
        IConfiguration Configuration;
        public ChatHub(RokonoControlContext context, IConfiguration config)
        {
            RokonoContext = context;
            Configuration = config;
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
                var userId = user.Claims.ElementAt(1);// Call the broadcastMessage method to update clients.
                using(var context = new DatabaseController(RokonoContext,Configuration))
                {
                    var notifications = context.GetNewNotifications(userId.Value);
                    res = JsonConvert.SerializeObject(notifications);
                }
                return Clients.Caller.SendAsync("ReciveNotification", res);
            }
            return null;
        }
    }
}