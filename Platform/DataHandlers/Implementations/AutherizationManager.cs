using System.Linq;
using Platform.DatabaseHandlers.Contexts;
using Platform.DataHandlers.Interfaces;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.DataHandlers
{
    public class AutherizationManager : IAutherizationManager
    {
        
        public  UserRights ValidateUserRights(int projectId, int currentUser, UsersContext context)
        {
            if (currentUser != default(int))
                return context.GetUserRights(currentUser, projectId);
            else
                return  new UserRights
                {
                    ChatChannelsRule = 0,
                    Documentation = 0,
                    ManageIterations = 0,
                    ManageUserdays = 0,
                    UpdateUserRights = 0,
                    ViewOtherPeoplesWork = 0,
                    WorkItemRule = 0
                };
        }

        public  int GetCurrentUser(int currentUser,  Microsoft.AspNetCore.Http.HttpRequest request)
        {
            if(request == null)
                return 0;
            if (request.HttpContext.User != null && request.HttpContext.User.Claims != null && request.HttpContext.User.Claims.Count() > 2)
            {
                var user = request.HttpContext.User.Claims.ElementAt(1);
                currentUser = int.Parse(user.Value);
            }

            return currentUser;
        }

        public  int GetCurrentUserContext(int currentUser, System.Security.Claims.ClaimsPrincipal user)
        {
            if (user != null && user.Claims != null && user.Claims.Count() > 2)
            {
                var result = user.Claims.ElementAt(1);
                currentUser = int.Parse(result.Value);
            }

            return currentUser;
        }
    }
}