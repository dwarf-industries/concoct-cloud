namespace RokonoControl.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.DataHandlers;
    using Platform.DataHandlers.Interfaces;
    using Rokono_Control.DatabaseHandlers;
    using Rokono_Control.Models;
    using RokonoControl.Models;

    public class CommitController : Controller
    {

        RokonoControlContext Context;
        IConfiguration Configuration;
        AutherizationManager AutherizationManager;
        private int UserId;
 
        public CommitController(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId,httpContextAccessor.HttpContext.Request);
        }
        public IActionResult Index(int projectId)
        {
            using(var context = new WorkItemsContext(Context,Configuration))
            {
                ViewData["Relationships"] = context.GetProjectRelationships();
                ViewData["DefaultIteration"] = context.GetProjectDefautIteration(projectId);
            }
            using (var context = new DatabaseController(Context,Configuration))
                ViewData["Branches"] = context.GetBranchesForProject(projectId);
            using(var context = new UsersContext(Context,Configuration))
                ViewData["Projects"] = context.GetUserProjects(UserId);
            return View();
        }


        public IActionResult CommitData(string commitId, int projectId, int branchId)
        {
    
            ViewData["CommitKey"] = commitId;
            ViewData["BranchId"] = branchId;
            ViewData["ProjectId"] = projectId;
            
            return View();
        }
        public IActionResult Files(int projectId)
        {
            ViewData["ProjectId"] = projectId;
            using(var context = new WorkItemsContext(Context,Configuration))
                ViewData["Relationships"] = context.GetProjectRelationships();
            using (var context = new DatabaseController(Context,Configuration))
            {
                var bindingBranches = context.GetProjectBranches(projectId);
                if (bindingBranches != null)
                {
                    ViewData["BranchId"] = bindingBranches.FirstOrDefault(x => x.BranchName == "master").Id;
                    ViewData["Branches"] = bindingBranches;

                }
                else
                    return View(new ClientErrorData { Title = "Invalid data result, please supply proper Project Id or contact your system administrator." });
            }
            using(var context = new WorkItemsContext(Context,Configuration))
                ViewData["DefaultIteration"] = context.GetProjectDefautIteration(projectId);
            using(var context = new UsersContext(Context,Configuration))
                ViewData["Projects"] = context.GetUserProjects(UserId);
            return View();

        }

        [HttpGet]
        public List<OutgoingCommitData> GetCommits(int projectId, int branchId)
        {
            var data = new List<OutgoingCommitData>();
            using (var context = new DatabaseController(Context,Configuration))
            {
                if (branchId != 0)
                    data = context.GetCommitData(projectId, branchId);
                else
                    data = context.GetCommitDataMaster(projectId);
            }
            return data;
        }

        [HttpGet]
        public List<CommitFileHirarhicalData> GetCommitFiles(string commitId)
        {
            var data = new List<CommitFileHirarhicalData>();
            using (var context = new DatabaseController(Context,Configuration))
            {
                data = context.GetCommitFilesHirarchy(commitId);
            }
            return data;
        }

        [HttpGet]
        public OutgoingSourceFile GetCommitFileData(int fileId, int branch)
        {
            var result = default(OutgoingSourceFile);
            using (var context = new DatabaseController(Context,Configuration))
            {
                result = context.GetSelectedFileById(fileId, branch);
            }
            return result;
        }
        [HttpGet]
        public List<CommitFileHirarhicalData> GetBranchFiles(int projectId, int branchId)
        {
            var result = new List<CommitFileHirarhicalData>();
            using (var context = new DatabaseController(Context,Configuration))
            {
                var getProject = context.GetBranchFiles(projectId, branchId);
                if (getProject != null)
                    result = context.GetFileHirarchy(getProject);
            }
            return result;
        }

        [HttpGet]
        public OutgoingSourceFile GetSelectedFile(string fileName, int projectId, int branch)
        {
            var result = default(OutgoingSourceFile);
            using (var context = new DatabaseController(Context,Configuration))
            {
                result = context.GetSelectedFileByName(fileName, projectId, branch);
            }
            return result;
        }
    }
}