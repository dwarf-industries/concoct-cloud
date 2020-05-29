using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Platform.DataHandlers;
using Platform.Models;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.Controllers
{
    public class ChatController : Controller
    {
        RokonoControlContext Context;
        IConfiguration Configuration;

        public ChatController(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        
        [HttpPost]
        [Authorize (Roles = "User")]
//        [ValidateAntiForgeryToken]
        public List<ChatRoomRights> GetChatRoomRights([FromBody] IncomingIdRequest request)
        {
            var result = new List<ChatRoomRights>();
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
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
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
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
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                result = context.GetChatChannels(request.Id, id);
            }
            return result;
    }
        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public List<OutgoingChatItem> AddNewCategory([FromBody] IncomingIdRequest request)
        {
          
            var result = new  List<OutgoingChatItem>();
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                context.AddNewChatChannel(request);
                result = context.GetChatChannels(request.Id,id);
            }
            return result;
        }

        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]

        public List<OutgoingChatItem> AddNewChatRoom([FromBody] IncomingIdRequest request)
        {
            
            var result = new  List<OutgoingChatItem>();
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                context.AddNewChatRoom(request);
                result = context.GetChatChannels(request.WorkItemType,id);
            }
            return result;
        }

        
        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public ChatRoomRights AssignUserTag([FromBody] IncomingIdRequest request)
        {
          
            var result = default(ChatRoomRights);
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
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
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                result = context.GetChatNotifications(request.Id, id);
            }
            return result;
        }

        [HttpPost]
        [Authorize (Roles = "ChatAdministrator")]
//        [ValidateAntiForgeryToken]
        public OutgoingJsonData DeleteUserTag([FromBody] IncomingIdRequest request)
        {
          
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
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
            
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
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
        

            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var id = int.Parse(user.Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                context.InserTag(request);
            }
            return new OutgoingJsonData{ Data = "Ok"};
        }
        [HttpGet]
        [Authorize (Roles = "User")]
//        [ValidateAntiForgeryToken]
        public IActionResult GetChatRoom(int id, int projectId) 
        {
            var user =  Request.HttpContext.User.Claims.ElementAt(1);
            var userId = int.Parse(user.Value);
            using(var context = new  DatabaseController(Context,Configuration))
            {
                context.UserChatChannelRead(userId, projectId, id);
            }
            return ViewComponent("ChatWIndow", new IncomingIdRequest{
                Id = id,
                ProjectId = projectId
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