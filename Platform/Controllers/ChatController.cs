using System.Collections.Generic;
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
        public List<OutgoingChatItem> GetChatChannels([FromBody] IncomingIdRequest request)
        {
            var result = new List<OutgoingChatItem>();
            using(var context = new DatabaseController(Context, Configuration))
            {
                result = context.GetChatChannels(request.Id);
            }
            return result;
        }

        [HttpPost]
        public List<OutgoingChatItem> AddNewCategory([FromBody] IncomingIdRequest request)
        {
            var result = new  List<OutgoingChatItem>();
            using(var context = new DatabaseController(Context, Configuration))
            {
                context.AddNewChatChannel(request);
                result = context.GetChatChannels(request.Id);
            }
            return result;
        }

         [HttpPost]
        public List<OutgoingChatItem> AddNewChatRoom([FromBody] IncomingIdRequest request)
        {
            var result = new  List<OutgoingChatItem>();
            using(var context = new DatabaseController(Context, Configuration))
            {
                context.AddNewChatRoom(request);
                result = context.GetChatChannels(request.WorkItemType);
            }
            return result;
        }
    }
}