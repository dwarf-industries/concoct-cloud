
namespace Platform.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
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
        
        public (bool, List<System.Dynamic.ExpandoObject>, List<string>, List<System.Dynamic.ExpandoObject>) AuthenicateCB(string key, string username, string password)
        {
            var attempt = false;
            var projects = new List<Projects>();
            var cResult = new List<System.Dynamic.ExpandoObject>();
            var GetOrganizationTags = new List<System.Dynamic.ExpandoObject>();
            var organizations = new List<string>();
            using (var context = new DatabaseController(Context, Configuration))
            {
                projects = context.AuthenicatedUser(key,username,password);
                if(projects != null)
                {
                    projects.ForEach(x =>
                    {

                        var workItems = context.GetAllWorkItemsForProject(x.Id);
                        dynamic extendable = new System.Dynamic.ExpandoObject();

                        extendable.ProjectName = x.ProjectName;
                        extendable.ProjectId = x.Id;
                        extendable.OrganizationName = x.OrganizationName;
                        extendable.WorkItems = workItems.Select(x => new
                        {
                            Id = x.Id,
                            WorkItemType = x.WorkItemTypeId,
                            Title = x.Title,
                            SprintId = x.Iteration
                        }).ToList();
                        
                        cResult.Add(extendable);
                    });
                    organizations = projects.Select(x => x.OrganizationName).Distinct().ToList();
                    attempt = true;

                }
                GetOrganizationTags = context.GetORganizationTags(projects.Select(x => x.OrganizationName.ToLower()).Distinct().ToList());

            }

            return (attempt, cResult, organizations, GetOrganizationTags);
        }

        [HttpPost]
        public IActionResult ConcoctBuilderSync([FromBody] IncomingIdRequest request)
        {
            using(var context = new WorkItemsContext(Context,Configuration))
            {
                var deserialize = JsonConvert.DeserializeObject<Layouts>(request.Phase);
                context.AddUpdateLayout(deserialize);
            }
            return Ok();
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