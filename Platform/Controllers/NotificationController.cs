using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Platform.Models;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.Controllers
{
    public class NotificationController :Controller
    {

        RokonoControlContext Context;
        IConfiguration Configuration;

        public NotificationController(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }
        

        [HttpPost]
        public JsonResult GenerateBacklogReport([FromBody] IncomingEmailReportRequest request)
        {
            var currentUser = this.User;
            var id = int.Parse(currentUser.Claims.ElementAt(1).Value);
            var account = default(UserAccounts);
            using(var context = new DatabaseController(Context, Configuration))
            {
                account = context.GetUserAccount(id);
                var getBacklogWorkItems = context.BackgroundWorkItems(request.Items);
                using(var notificationManager = new DataHandlers.NotificationHandler(Configuration))
                {
                    notificationManager.GeneraBacklogReport(getBacklogWorkItems, account);
                }
            }
           
            return Json(new object{});
        }
        [HttpPost]
        public OutgoingJsonData AddNewNote([FromBody] IncomingNoteRequest note)
        {
            var currentUser = this.User;
            var id = int.Parse(currentUser.Claims.ElementAt(1).Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                 context.AddNewUserNote(note, id);
            }

            return new OutgoingJsonData { Data = ""};
        }

        [HttpPost]
        public OutgoingJsonData NotificationRead([FromBody] IncomingIdRequest request)
        {
            var currentUser = this.User;
            var id = int.Parse(currentUser.Claims.ElementAt(1).Value);
            using(var context = new DatabaseController(Context,Configuration))
            {
                context.NotificationRead(request.Id, id);
            }
            return new OutgoingJsonData{};
        }

        [HttpPost]
        public OutgoingJsonData ChangeNotePosition([FromBody] IncomingNoteRequest note)
        {
            var currentUser = this.User;
            var id = int.Parse(currentUser.Claims.ElementAt(1).Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                 context.ChangeNotePosition(note);
            }

            return new OutgoingJsonData { Data = ""};
        }
        [HttpPost]
        public OutgoingJsonData DeleteNote([FromBody] IncomingNoteRequest note)
        {
            var currentUser = this.User;
            var id = int.Parse(currentUser.Claims.ElementAt(1).Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                 context.DeleteNote(note);
            }

            return new OutgoingJsonData { Data = ""};
        }
        [HttpPost]
        public OutgoingJsonData EditNote([FromBody] IncomingNoteRequest note)
        {
            var currentUser = this.User;
            var id = int.Parse(currentUser.Claims.ElementAt(1).Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                 context.EditNote(note);
            }

            return new OutgoingJsonData { Data = ""};
        }

        [HttpPost]
        public List<Notifications> GetUserNotifications([FromBody] IncomingNoteRequest note)
        {
            var result = new List<Notifications>();
            var currentUser = this.User;
            var id = int.Parse(currentUser.Claims.ElementAt(1).Value);
            using(var context = new DatabaseController(Context, Configuration))
            {
                result = context.GetAllUserNotifications(id, note.ProjectId);
            }

            return result;
        }

        
    }
}