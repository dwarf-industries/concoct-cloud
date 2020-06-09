namespace Platform.Hubs
{
    using System;
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
    using Platform.Models;
    using Rokono_Control;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;
    public class MessageHub : Hub
    {
        RokonoControlContext DatabaseContext;
        IConfiguration Configuration;
        private  AutherizationManager AutherizationManager;
        private int UserId;
 
        public MessageHub(RokonoControlContext dbContext, IConfiguration config,IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            DatabaseContext = dbContext;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
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
                using(var dbContext = new ChatContext(DatabaseContext,Configuration))
                {
                    messageData.SenderName = dbContext.AddChatRoomMessage(messageData,UserId);
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
           
            var res = string.Empty;
            using(var context = new NotificationContext(DatabaseContext,Configuration))
            {
                var notifications = context.GetNewNotifications(UserId);
                res = JsonConvert.SerializeObject(notifications);
            }
            return Clients.Caller.SendAsync("ReciveNotification", res);
        }
        
       public static void SendNewNotification(IHubContext<MessageHub> hubContext, HubMappedMembers reciverData, Notifications notification)
       {
           notification.AssociatedProjectNotifications = null;
           notification.AssociatedUserNotifications = null;
            hubContext.Clients
                      .Client(reciverData.Id)
                      .SendAsync("ReciveNewNotification",
                      JsonConvert.SerializeObject(notification));
       }
        public override Task OnConnectedAsync()
        {
            
           string name = Context.User.Claims.FirstOrDefault().Value;
           Groups.AddToGroupAsync(Context.ConnectionId, name);
            Program.Members.Add(new Models.HubMappedMembers{
                Id = Context.ConnectionId,
                Name = name
            });

            Clients.Others.SendAsync("UserConnected");
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            string name = Context.User.Claims.FirstOrDefault().Value;
           // Groups.AddToGroupAsync(Context.ConnectionId, name);
            var  member = Program.Members.FirstOrDefault(x=>x.Id == Context.ConnectionId);
            Program.Members.Remove(member);
            // ChatHandlerPackets.UpdateConnectedList();
            Clients.Others.SendAsync("UserDisconnected");

            return null;
        }  
    }
}