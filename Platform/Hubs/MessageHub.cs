using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Platform.Models;
using Rokono_Control;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.Hubs
{
    public class MessageHub : Hub
    {
        RokonoControlContext DatabaseContext;
        IConfiguration Configuration;

        public MessageHub(RokonoControlContext dbContext, IConfiguration config)
        {
            DatabaseContext = dbContext;
            Configuration = config;
        }

       [Authorize(Roles = "User")]
       [HubMethodName("IncomingMessage")]
        public  Task NewChatChannelMessage(string incomingData)
        {
            //  if(string.IsNullOrEmpty(incomingData))
            //     return null;

            var messageData = JsonConvert.DeserializeObject<IncomingChatMessage>(incomingData);
          
            var user = Context.User;
            if (user != null)
            {
                var username = user.Claims.FirstOrDefault();// Call the broadcastMessage method to update clients.
                using(var dbContext = new DatabaseController(DatabaseContext,Configuration))
                {
                    messageData.SenderName = dbContext.AddChatRoomMessage(messageData,int.Parse(user.Claims.ElementAt(1).Value));
                }

                return  Clients.Others.SendAsync("ReciveMessage", JsonConvert.SerializeObject( new IncomingChatMessage{
                    ActiveRoom = messageData.ActiveRoom,
                    Message = messageData.Message,
                    ProjectId = messageData.ProjectId,
                    SenderName = messageData.SenderName
                }));
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