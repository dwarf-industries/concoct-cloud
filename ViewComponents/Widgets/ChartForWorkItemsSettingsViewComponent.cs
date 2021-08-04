 
namespace Platform.ViewComponents.Widgets
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.DatabaseHandlers.Contexts;
    using Platform.Models;
    using Rokono_Control.Models;

    [ViewComponent(Name = "ChartForWorkItemsSettings")]
    public class ChartForWorkItemsSettingsViewComponent : ViewComponent
    {
                private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;

        public ChartForWorkItemsSettingsViewComponent(RokonocontrolContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(IncomingIdRequest request)
        {
            ViewData["ProjectId"] = request.ProjectId;
            ViewData["ViewComponentId"] = request.Id;
            ViewData["Dashboard"] = request.WorkItemType;
            using (var context = new WorkItemsContext(Context, Configuration))
                ViewData["WorkItemTypes"] = context.GetAllWorkItemTypes();
            using (var context = new UsersContext(Context, Configuration))
                ViewData["UserQueries"] = context.GetSharedQueries(request.ProjectId);
            InitCountWItems();
            ViewData["BurndownOn"] = GetBundownOn();
            ViewData["GroupBy"] = GetGroupBy();
            ViewData["SumWItems"] = GetSumWItems();
            return View("/Views/Shared/Components/Widgets/ChartForWorkItemsSettings/Default.cshtml");
        }

        private List<OutgoingBindingCollection> GetGroupBy()
        {
            return new List<OutgoingBindingCollection>{
                new OutgoingBindingCollection{
                    Id = 1,
                    Name = "State"
                },
                new OutgoingBindingCollection{
                    Id = 2,
                    Name = "Priority"
                },
                new OutgoingBindingCollection{
                    Id = 3,
                    Name = "Severity"
                },
                new OutgoingBindingCollection{
                    Id = 4,
                    Name = "Stack Rank"
                }
            };
        }

        private void InitCountWItems()
        {
            ViewData["CountWItems"] = new List<OutgoingBindingCollection> { new OutgoingBindingCollection { Id = 1, Name = "Work Item Type" } };
        }

        private List<OutgoingBindingCollection> GetSumWItems()
        {
            return new List<OutgoingBindingCollection>{
                new OutgoingBindingCollection{
                    Id= 1,
                    Name = "Priority"
                },
                new OutgoingBindingCollection{
                    Id = 2,
                    Name = "Stack Rank"
                },
                new OutgoingBindingCollection{
                    Id = 3,
                    Name = "Story Points"
                }
            };
        }

        private static List<OutgoingBindingCollection> GetBundownOn()
        {
            return new List<OutgoingBindingCollection>{ 
                new OutgoingBindingCollection{
                    Id = 1,
                    Name = "Count"
                },
                new OutgoingBindingCollection{
                    Id= 2,
                    Name = "Sum"
                }
            };
        }
    }
}