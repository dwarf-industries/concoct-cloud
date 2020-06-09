namespace Platform.Controllers
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Platform.Models;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;
    public class NotificationController :Controller
    {

        RokonoControlContext Context;
        IConfiguration Configuration;
        private  AutherizationManager AutherizationManager;
        private int UserId;
        public NotificationController(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }



        [HttpPost]
        public JsonResult GenerateBacklogReport([FromBody] IncomingEmailReportRequest request)
        {
 
            var account = default(UserAccounts);
            using(var context = new UsersContext(Context,Configuration))
                    account = context.GetUserAccount(UserId);

            using(var context = new WorkItemsContext(Context, Configuration))
            {
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
 
            using(var context = new NotesContext(Context, Configuration))
            {
                 context.AddNewUserNote(note, UserId);
            }

            return new OutgoingJsonData { Data = ""};
        }

        [HttpPost]
        public OutgoingJsonData NotificationRead([FromBody] IncomingIdRequest request)
        {
 
            using(var context = new NotificationContext(Context,Configuration))
            {
                context.NotificationRead(request.Id, UserId);
            }
            return new OutgoingJsonData{};
        }

        [HttpPost]
        public OutgoingJsonData ChangeNotePosition([FromBody] IncomingNoteRequest note)
        {
 
            using(var context = new NotesContext(Context, Configuration))
            {
                 context.ChangeNotePosition(note);
            }

            return new OutgoingJsonData { Data = ""};
        }
        [HttpPost]
        public OutgoingJsonData DeleteNote([FromBody] IncomingNoteRequest note)
        {
      
            using(var context = new NotesContext(Context, Configuration))
            {
                 context.DeleteNote(note);
            }

            return new OutgoingJsonData { Data = ""};
        }
        [HttpPost]
        public OutgoingJsonData EditNote([FromBody] IncomingNoteRequest note)
        {
 
            using(var context = new NotesContext(Context, Configuration))
            {
                 context.EditNote(note);
            }

            return new OutgoingJsonData { Data = ""};
        }

        [HttpPost]
        public List<Notifications> GetUserNotifications([FromBody] IncomingNoteRequest note)
        {
            var result = new List<Notifications>();
 
            using(var context = new NotificationContext(Context, Configuration))
            {
                result = context.GetAllUserNotifications(UserId, note.ProjectId);
            }

            return result;
        }

        
    }
}