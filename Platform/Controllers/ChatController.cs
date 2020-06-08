namespace Platform.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.SignalR;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Platform.Hubs;
    using Platform.Models;
    using Rokono_Control;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;
    public class ChatController : Controller
    {
        RokonoControlContext Context;
        IConfiguration Configuration;
        AutherizationManager AutherizationManager;
        public int UserId;
        private IHubContext<MessageHub> HubContext { get; set; }
        public ChatController(RokonoControlContext context,
                              IConfiguration config,
                              IAutherizationManager autherizationManager,
                              IHttpContextAccessor httpContextAccessor,
                              IHubContext<MessageHub> hubContext )
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
            HubContext = hubContext;
        }

        
        [HttpPost]
        [Authorize (Roles = "User")]
//        [ValidateAntiForgeryToken]
        public List<ChatRoomRights> GetChatRoomRights([FromBody] IncomingIdRequest request)
        {
            var result = new List<ChatRoomRights>();
     
            using(var context = new DatabaseController(Context, Configuration))
            {
                result = context.GetChatRoomRights(request.Id);
            }
            return result;
        }
        
        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public List<ChatRooms> GetChatChannels([FromBody] IncomingIdRequest request)
        {
       
            var result = new List<ChatRooms>();
            using(var context = new DatabaseController(Context, Configuration))
            {
                result = context.GetAllChatChannels(request.Id);
            }
            return result;
        }
        [HttpPost]
        [Authorize (Roles = "User")]
//        [ValidateAntiForgeryToken]
        public List<OutgoingChatItem> GetChatChannelsNavigation([FromBody] IncomingIdRequest request)
        {
            var result = new List<OutgoingChatItem>();
    
            using(var context = new DatabaseController(Context, Configuration))
            {
                result = context.GetChatChannels(request.Id, UserId);
            }
            return result;
        }
        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public List<OutgoingChatItem> AddNewCategory([FromBody] IncomingIdRequest request)
        {
          
            var result = new  List<OutgoingChatItem>();
 
            using(var context = new DatabaseController(Context, Configuration))
            {
                context.AddNewChatChannel(request);
                result = context.GetChatChannels(request.Id,UserId);
            }
            return result;
        }

        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]

        public List<OutgoingChatItem> AddNewChatRoom([FromBody] IncomingIdRequest request)
        {
            
            var result = new  List<OutgoingChatItem>();
 
            using(var context = new DatabaseController(Context, Configuration))
            {
                context.AddNewChatRoom(request);
                result = context.GetChatChannels(request.WorkItemType,UserId);
            }
            return result;
        }

        
        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public ChatRoomRights AssignUserTag([FromBody] IncomingIdRequest request)
        {
          
            var result = default(ChatRoomRights);
            using(var context = new DatabaseController(Context, Configuration))
            {
                result = context.AssignUserTag(request.Id, request.ProjectId, request.UserId);

                
            }
            return result;
        }
        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public List<AssociatedUserChatNotifications> GetChannelNotifications([FromBody] IncomingIdRequest request)
        {
            var result = new  List<AssociatedUserChatNotifications>();
 
            using(var context = new DatabaseController(Context, Configuration))
            {
                result = context.GetChatNotifications(request.Id, UserId);
            }
            return result;
        }

        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public OutgoingJsonData DeleteUserTag([FromBody] IncomingIdRequest request)
        {
          
 
            using(var context = new DatabaseController(Context, Configuration))
            {
                context.RemoveUserTag(request.Id, request.UserId, request.ProjectId);
            }
            return new OutgoingJsonData{ Data = "Ok"};
        }

        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public OutgoingJsonData TagUpdate([FromBody] IncomingChatRoomRights request)
        {           
 
            using(var context = new DatabaseController(Context, Configuration))
            {
                
                context.UpdateTag(request);
            }
            return new OutgoingJsonData{ Data = "Ok"};
        }
        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public OutgoingJsonData TagSave([FromBody] IncomingChatRoomRights request)
        {
            using(var context = new DatabaseController(Context, Configuration))
            {
                context.InserTag(request);
            }
            return new OutgoingJsonData{ Data = "Ok"};
        }
        [HttpPost]
        [Authorize (Roles = "User")]
//        [ValidateAntiForgeryToken]
        public OutgoingJsonData NewPersonalMessage([FromBody] IncomingChatMessage request)
        {
            using(var context = new DatabaseController(Context, Configuration))
            {
               var values = context.NewChatPersonalMessage(request,UserId);
               var reciverData = Program.Members.FirstOrDefault(x=>x.Name == values.Item2);
               MessageHub.SendNewNotification(HubContext, reciverData, values.Item1);
            }
            return new OutgoingJsonData{ Data = "Ok"};
        }
        [HttpGet]
        [Authorize (Roles = "User")]
//        [ValidateAntiForgeryToken]
        public IActionResult GetChatRoom(int id, int projectId, int isPersonal) 
        {
            using(var context = new  DatabaseController(Context,Configuration))
            {
                context.UserChatChannelRead(UserId, projectId, id);
            }
            return ViewComponent("ChatWIndow", new IncomingIdRequest{
                Id = id,
                ProjectId = projectId,
                UserId = isPersonal
            });
        }

        [HttpGet]
        [Authorize (Roles = "User")]
//        [ValidateAntiForgeryToken]
        public IActionResult GetUserDirectMessageControl(int projectId,int id) 
        {
            return ViewComponent("ChatUserPersonaBox", new IncomingIdRequest{
                Id = id,
                ProjectId = projectId
            });
        }
        [HttpGet]
        [Authorize (Roles = "User")]
//        [ValidateAntiForgeryToken]
        public IActionResult InvokeChatNavigation(int projectId) 
        {
            return ViewComponent("ChatNavigation", projectId);
        }

        [HttpGet]
        [Authorize (Roles = "User")]
//        [ValidateAntiForgeryToken]
        public IActionResult InvokeChatUserListUpdate(int projectId) 
        {
            return ViewComponent("UserList", projectId);
        }
        [HttpGet]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public IActionResult GetChatSettings(int id, int chatRoom) 
        {
         
            return ViewComponent("ChatSettings", new IncomingIdRequest{
                ProjectId =id,
                Id = chatRoom
            });
        }

        [HttpGet]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public IActionResult GetChatRoomSettings(int id, int chatRoom) 
        {
            
            
            return ViewComponent("ChatRoomSetting", new IncomingIdRequest{
                ProjectId =id,
                Id = chatRoom
            });
         }

        
        
        [HttpGet]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public IActionResult GetTagProperties(int id, int projectId, int option, int chatRoom) 
        {
            
            return ViewComponent("GetTagProperties", new IncomingIdRequest{
                Id = id,
                ProjectId = projectId,
                WorkItemType = option,
                UserId = chatRoom
            });
            
        }
        [HttpGet]
        [Authorize (Roles = "User")]
        public IActionResult LoadChatControl(int projectId) 
        {
            return ViewComponent("ChatSidebar", projectId);
        }
    }
}