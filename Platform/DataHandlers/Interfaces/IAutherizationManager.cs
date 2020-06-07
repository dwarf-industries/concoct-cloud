using Rokono_Control.DatabaseHandlers;
using Rokono_Control.Models;

namespace Platform.DataHandlers.Interfaces
{
    public interface IAutherizationManager
    {
        UserRights ValidateUserRights(int projectId, int currentUser, DatabaseController context);
        int GetCurrentUser(int currentUser,  Microsoft.AspNetCore.Http.HttpRequest request);
        int GetCurrentUserContext(int currentUser, System.Security.Claims.ClaimsPrincipal user);
    }
}