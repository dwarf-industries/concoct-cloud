
namespace Platform.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.Models;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;

    public class OutboundDetailsController : Controller
    {
        RokonocontrolContext Context;
        IConfiguration Configuration;

        public OutboundDetailsController(RokonocontrolContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IActionResult RelatedProject(int projectId)
        {
            using(var context = new DatabaseController(Context,Configuration))
            {
                ViewData["ProjectName"] = context.GetProjectName(projectId);
                ViewData["ProjectId"] = projectId;
            }
            using(var context = new OutboundDetailsContext(Context,Configuration))
                ViewData["EnabledFeatures"] = context.GetProjectOutboundFeatures(projectId);

            return View();
        }
        
        [HttpPost]
        public List<PublicMessages> GetPublicDiscussions([FromBody] IncomingIdRequest request)
        {
            //Temporary disable due to changes over the new dicussions widget
            var result = new List<PublicMessages>();
            using(var context = new ChatContext(Context,Configuration))
            {
             //   result = context.GetAllPublicMessagesForProject(request.Id,0);
            }
            return result;
        }
        [HttpPost]
        public PublicMessages AddNewPublicMessage([FromBody] IncomingPublicMessage message)
        {
            var result = new PublicMessages();
            using(var context = new ChatContext(Context,Configuration))
            {
                result = context.AddNewPublicMessage(message);
            }
            return result;
        }

        [HttpPost]
        public PublicMessages LeaveFeedback([FromBody] IncomingPublicMessage message)
        {
            var result = new PublicMessages();
            using(var context = new OutboundDetailsContext(Context,Configuration))
            {
                result = context.AddNewFeedback(message);
            }
            return result;
        }

        [HttpPost]
        public OutgoingJsonData AddNewBugReport([FromBody] IncomingNewBugReport report)
        {
            var result = new OutgoingJsonData();
            using(var context = new OutboundDetailsContext(Context, Configuration))
            {
                var title = $"{Guid.NewGuid()}";
                context.AddNewBugReport(report, title);
            }
            return result;
        }
    }
}