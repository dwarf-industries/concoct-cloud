using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Platform.DatabaseHandlers.Contexts;
using Platform.DataHandlers;
using Platform.DataHandlers.Interfaces;
using Rokono_Control.DatabaseHandlers;
using Rokono_Control.DatabaseHandlers.Contexts;
using Rokono_Control.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rokono_Control.Controllers
{
    public class RepositoriesController : Controller
    {

        RokonoControlContext Context;
        IConfiguration Configuration;
        AutherizationManager AutherizationManager { get; set; }
        private int UserId { get; set; }


        public RepositoriesController(RokonoControlContext context, IConfiguration config, IAutherizationManager autherizationManager, IHttpContextAccessor httpContextAccessor)
        {
            Context = context;
            Configuration = config;
            AutherizationManager = (AutherizationManager)autherizationManager;
            UserId = AutherizationManager.GetCurrentUser(UserId, httpContextAccessor.HttpContext.Request);
        }

        public IActionResult Index(int id)
        {
            using (var context = new DatabaseController(Context, Configuration))
            {
                ViewData["ProjectId"] = id;
                ViewData["ProjectName"] = context.GetProjectName(id);
            }
            using (var context = new WorkItemsContext(Context, Configuration))
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();

            ViewData["IsEmpty"] = true;
            return View();
        }


        public IActionResult Commits(int id)
        {
            using (var context = new DatabaseController(Context, Configuration))
            {
                
                ViewData["ProjectId"] = id;
                ViewData["ProjectName"] = context.GetProjectName(id);
            }
            using (var context = new WorkItemsContext(Context, Configuration))
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();

            
            ViewData["IsEmpty"] = true;
            return View();
        }

        public IActionResult Commit(int id, string commitId)
        {
            using (var context = new DatabaseController(Context, Configuration))
            {

                ViewData["ProjectId"] = id;
                ViewData["ProjectName"] = context.GetProjectName(id);
            }
            using (var context = new WorkItemsContext(Context, Configuration))
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();

            ViewData["CommitId"] = commitId;
            ViewData["IsEmpty"] = true;
            return View();
        }

        [HttpPost]
        public List<OutgoingCommitTemp> GetCommitsForBranch([FromBody] IncomingIdRequest request)
        {
            var branch = request.Phase;
            //We set the branch to default to master potential to fail with github anti racism bullshit
            if (string.IsNullOrEmpty(branch))
                branch = "master";

            var relationships = new List<OutgoingCommitTemp>();
            using (var context = new RepositoriesContext(Context, Configuration,new RepositoryManager()))
                relationships = context.GetCommitsForProject(request.ProjectId, branch);

            return relationships;
        }

        [HttpGet]
        public List<Branches> GetBranchesByProjectId(int projectId)
        {

            var branches = new List<Branches>();
            using (var context = new RepositoriesContext(Context, Configuration, new RepositoryManager()))
            {
                 branches = context.GetBranchesGit(projectId);
            }

            return branches;
        }
    }
}
