namespace Platform.ViewComponents
{
    using System;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.Models;

    public class UserRightToggleViewComponent : ViewComponent
    {
        private readonly RokonoControlContext Context;
        private readonly IConfiguration Configuration;
        private  AutherizationManager AutherizationManager;
        private int UserId;
 
        public UserRightToggleViewComponent(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            ViewData["ControlName"] = request.Phase;
            ViewData["IsEnabled"] = request.WorkItemType; 
            using(var context = new UsersContext(Context, Configuration))
            {
                if(request.WorkItemType == 1)
                    ViewData["Enabled"] = GetControlState(context.GetProectNotificationSetting(request.Phase,
                                                                                               request.ProjectId,
                                                                                               UserId),request.Phase);
                else
                    ViewData["Enabled"] =  GetControlState(AutherizationManager.ValidateUserRights(request.ProjectId, UserId, context),
                                                           request.Phase);
            }
            return View();
        }

        private int GetControlState(UserRights rights, string phase)
        {
            var result = default(int);
            switch(phase)
            {
                case "ChatChannelsRule":
                    result = rights.ChatChannelsRule;
                break;
                case "Documentation":
                    result = rights.Documentation.Value;
                break;
                case "ManageIterations": 
                    result = rights.ManageIterations;
                break;
                case "ManageUserdays":
                    result = rights.ManageUserdays;
                break;
                case "UpdateUserRights":
                    result = rights.UpdateUserRights;
                break;
                case "ViewOtherPeoplesWork":
                    result = rights.ViewOtherPeoplesWork;
                break;
                case "WorkItemRule":
                    result = rights.WorkItemRule;
                break;  
            }
            return result;
        }

        private int GetControlState(NotificationRights notificationRights, string phase)
        {
            var result = default(int);
            switch(phase)
            {
                case "PersonalMessage":
                    result = notificationRights.PersonalMessageNenabled.Value;
                break;
                case "NewWorkItem":
                    result = notificationRights.CreateWorkItemNenabled.Value;
                break;
                case "UpdatedWorkItem":
                    result = notificationRights.UpdateWorkItemNenabled.Value;
                break;
                case "PublicFeedback":
                    result = notificationRights.FeedbackNenabled.Value;
                break;
                case "PublicBugreport":
                    result = notificationRights.BugReportNenabled.Value;
                break;
                case "PublicDiscussion":
                    result = notificationRights.PublicDiscussionMnenabled.Value;
                break;
                case "ChatChannelMessage":
                    result = notificationRights.ChatChannelNenabled.Value;
                break;
                case "ChangelogGenerated":
                    result = notificationRights.ChanegelogNenabled.Value;
                break;
            }

            return result;
        }
    }
}