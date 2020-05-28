using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        public IActionResult GetUserDirectMessageControl(int projectId,int id) 
        {
            return ViewComponent("ChatUserPersonaBox", new IncomingIdRequest{
                Id = id,
                ProjectId = projectId
            });
        }
        [HttpGet]
        public IActionResult InvokeChatNavigation(int projectId) 
        {
            return ViewComponent("ChatNavigation", projectId);
        }

        [HttpGet]
        public IActionResult InvokeChatUserListUpdate(int projectId) 
        {
            return ViewComponent("UserList", projectId);
        }
        [HttpGet]
        public IActionResult GetChatSettings(int id, int chatRoom) 
        {
            return ViewComponent("ChatSettings", new IncomingIdRequest{
                ProjectId =id,
                Id = chatRoom
            });
        }

        [HttpGet]
        public IActionResult GetChatRoomSettings(int id, int chatRoom) 
        {
            return ViewComponent("ChatRoomSetting", new IncomingIdRequest{
                ProjectId =id,
                Id = chatRoom
            });
        }

        
        
        [HttpGet]
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
        public IActionResult LoadChatControl(int projectId) 
        {
            return ViewComponent("ChatSidebar", projectId);
        }
    }
}