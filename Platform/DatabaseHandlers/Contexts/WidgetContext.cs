using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Platform.Models;
using Rokono_Control.Models;

namespace Platform.DatabaseHandlers.Contexts
{
    public class WidgetContext : IDisposable
    {
        RokonoControlContext Context;
        IConfiguration Configuration;
        private bool disposedValue;
        private bool disposedValue1;

        public WidgetContext(RokonoControlContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue1)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue1 = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~WidgetContext()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        private int ParseInt(string x) => int.Parse(x);
        internal object GetBurndownChartData(IncomingBurndownChartSetting request)
        {
            var result = new object();
            var bindingCollection = new List<AssociatedBoardWorkItems>();
            bindingCollection = Context.AssociatedBoardWorkItems.Include(x=>x.WorkItem).Where(x => x.ProjectId == request.ProjectId)
                                                                .ToList()
                                                                .Where(x => CompareDate(x.WorkItem.StartDate, request.StartDate))
                                                                .ToList();
            if(request.BacklogBindingType == 1)
            {
                bindingCollection = bindingCollection.Where(x=>x.WorkItem.WorkItemTypeId == request.BacklogSelectedType).ToList();
            }
            else
            {
                bindingCollection = bindingCollection.Where(x=>x.WorkItem.WorkItemTypeId == request.CountWItemSelected).ToList();
            }
            if(request.BurndownOnSelect == 1)
            {
                result = bindingCollection.OrderBy(x=>x.WorkItem.StartDate).GroupBy(x=>x.WorkItem.StartDate).Select(group=> new {
                    x = group.FirstOrDefault().WorkItem.StartDate,
                    y = group.Count()
                }).ToList();
            }
            else
            {
                switch(request.SumWItemSelected)
                {
                    case 1:
                        result = bindingCollection.OrderBy(x=>x.WorkItem.StartDate).GroupBy(x=>x.WorkItem.StartDate).Select(group=> new{
                            x = group.FirstOrDefault().WorkItem.StartDate,
                            y = group.Sum(item => item.WorkItem.ItemPriority)
                        }).ToList();
                    break;
                    case 2:
                     result = bindingCollection.OrderBy(x=>x.WorkItem.StartDate).GroupBy(x=>x.WorkItem.StartDate).Select(group=> new{
                            x = group.FirstOrDefault().WorkItem.StartDate,
                            y = group.Sum(item => ParseInt(item.WorkItem.StackRank))
                        }).ToList();
                    break;
                    case 3:
                     result = bindingCollection.OrderBy(x=>x.WorkItem.StartDate).GroupBy(x=>x.WorkItem.StartDate).Select(group=> new {
                            x = group.FirstOrDefault().WorkItem.StartDate,
                            y = group.Sum(item => ParseInt(item.WorkItem.StoryPoints))
                        }).ToList();
                    break;
                    default:

                    break;
                }
            }
            return result;
        
        }

        private bool CompareDate(DateTime? startDate, DateTime EndDate)
        {
            if(startDate != null)
                return startDate > EndDate;

            return false;
        }
    }
}