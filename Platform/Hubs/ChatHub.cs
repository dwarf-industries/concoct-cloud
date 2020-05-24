namespace Platform.Hubs
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Rokono_Control;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    public class ChatHub : Hub
    {
        RokonoControlContext DatabaseContext;
        IConfiguration Configuration;

        public ChatHub(RokonoControlContext context, IConfiguration config)
        {
            DatabaseContext = context;
            Configuration = config;
        }

        [Authorize(Roles = "User")]
        public  Task Send(string incomingData)
        {

            var user = Context.User;
            if (user != null)
            {
                var username = user.Claims.FirstOrDefault();// Call the broadcastMessage method to update clients.
                //ChatHandlerPackets.SendMessage($"{username.Value.ToString()}  |  {DateTime.Now.ToString()}",message, username.Value.ToString());
               // return Clients.Others.SendAsync("ReciveMessage", username.Value.ToString(), message);
            }
            return null;
        }

        public Task SendAmin(string message, string sender)
        {
            return Clients.Others.SendAsync("ReciveMessageAdmin", sender, message);
        }
        [Authorize(Roles = "User")]
        public Task NotificationRecived()
        {
          var user = Context.User;
            if (user != null)
            {
                 var res = string.Empty;
                var userId = user.Claims.ElementAt(1);// Call the broadcastMessage method to update clients.
                using(var context = new DatabaseController(DatabaseContext,Configuration))
                {
                    var notifications = context.GetNewNotifications(int.Parse(userId.Value));
                    res = JsonConvert.SerializeObject(notifications);
                }
                return Clients.Caller.SendAsync("ReciveNotification", res);
            }
            return null;
        }
        public override Task OnConnectedAsync()
        {
            
           string name = Context.User.Claims.FirstOrDefault().Value;
           Groups.AddToGroupAsync(Context.ConnectionId, name);
            Program.Members.Add(new Models.HubMappedMembers{
                Id = Context.ConnectionId,
                Name = name
            });
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Claims.FirstOrDefault().Value;
           // Groups.AddToGroupAsync(Context.ConnectionId, name);
            var  member = Program.Members.FirstOrDefault(x=>x.Id == Context.ConnectionId);
            Program.Members.Remove(member);
            // ChatHandlerPackets.UpdateConnectedList();
            return null;
        }  
    }
}