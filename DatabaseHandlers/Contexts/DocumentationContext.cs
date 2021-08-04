namespace Platform.DatabaseHandlers.Contexts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Platform.Models;
    using Rokono_Control.Models;
    public class DocumentationContext : IDisposable
    {
        RokonocontrolContext Context;
        IConfiguration Configuration;
        private bool disposedValue;

        public DocumentationContext(RokonocontrolContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        internal AssociatedDocumentationCategoryPage GetDocumentationPage(int id)
        {
            return Context.AssociatedDocumentationCategoryPage 
                                                       .FirstOrDefault(x=>x.Id == id);
            
        }
        internal void AddNewDocumentationCategory(IncomingIdRequest request)
        {
            var getDocumentation = Context.Documentation.FirstOrDefault(x=> x.ProjectId == request.ProjectId);
            Context.DocumentationCategory.Add(new DocumentationCategory{
                CategoryName = request.Phase,
                DocumentationId = getDocumentation.Id,
            });
            Context.SaveChanges();
        }

        internal List<OutgoingChatItem> GetDocumentationNavigation(int id)
        {
            var result = new List<OutgoingChatItem>();
            Context.DocumentationCategory
            .Include(x=>x.DocumentationCategoryField)
            .Select(x=>x)
            .ToList().ForEach(x=>{
                var cItem = new OutgoingChatItem
                {     
                    InternalId = x.Id,
                    // NodeId = i++,
                    NodeText = x.CategoryName,
                    IconCss = "fa-list-alt",
                    Link = "",
                    ChannelType = 0,
                    IsParent = true,
                    ParentId = x.Id,
                    IsExpand = 1,
                    NodeChild = x.DocumentationCategoryField.Select(y=> new OutgoingChatItemChild{
                        InternalId = y.Id,
                        // NodeId = i++,
                        NodeText = y.PageName,
                        IconCss = "fa fa-file",
                        Link = "",
                        ParentId = y.CategoryId.Value
                    }).ToList()
                };
                result.Add(cItem);
            });
            return result;
        }
        internal void DeleteCategoryField(int id)
        {
            var category = Context.DocumentationCategoryField.FirstOrDefault(x=>x.Id == id);
            var categoryPages = Context.AssociatedDocumentationCategoryPage.Where(x => x.CategoryField == category.Id)
                                                                           .ToList();
            Context.AssociatedDocumentationCategoryPage.RemoveRange(categoryPages);
            Context.DocumentationCategoryField.Remove(category);
            Context.SaveChanges();       
        }
        internal string GetDocumentationCategoryName(int id)
        {
            var result = Context.DocumentationCategoryField.FirstOrDefault(x=>x.Id == id);
            if(result != null)
                return result.PageName;
            return string.Empty;
        }

        internal int GetDocumentationDefaultCategory(int projectId)
        {
            var result = Context.DocumentationCategoryField.Include(x => x.Category)
                                                     .ThenInclude(Category => Category.Documentation)
                                                     .FirstOrDefault(x=>x.Category.Documentation.ProjectId == projectId);
            if(result != null)
                return result.Id;
            return 0;
        }

        internal List<AssociatedDocumentationCategoryPage> GetDocumentationPages(IncomingIdRequest request)
        {
            return Context.AssociatedDocumentationCategoryPage.Include(x => x.CategoryFieldNavigation)
                                                       .ThenInclude(CategoryFieldNavigation => CategoryFieldNavigation.Category)
                                                       .ThenInclude(Category => Category.Documentation)
                                                       .Where(x=>x.CategoryFieldNavigation.Category.Documentation.ProjectId 
                                                       == request.ProjectId && x.CategoryField == request.Id)
                                                       .ToList();
        }
        internal string GetDocumentationDefaultCategoryName(int projectId)
        {
             var result = Context.DocumentationCategoryField.Include(x => x.Category)
                                                     .ThenInclude(Category => Category.Documentation)
                                                     .FirstOrDefault(x=>x.Category.Documentation.ProjectId == projectId);
            if(result != null)
                return result.PageName;
            return string.Empty;
        }

        
        internal void DeleteCategory(int id)
        {
            var category = Context.DocumentationCategory.FirstOrDefault(x=>x.Id == id);
            
            var categoryFields = Context.DocumentationCategoryField.Where(x=>x.CategoryId == id).ToList();
            categoryFields.ForEach(x=>{
                var categoryField = x;
                var categoryPages = Context.AssociatedDocumentationCategoryPage.Where(x => x.CategoryField == categoryField.Id).ToList();
                Context.AssociatedDocumentationCategoryPage.RemoveRange(categoryPages);
                Context.DocumentationCategoryField.Remove(categoryField);
            });
            Context.DocumentationCategory.Remove(category);
            Context.SaveChanges();
        }
        internal void DeleteDocumentationPage(int id)
        {
            var current = Context.AssociatedDocumentationCategoryPage.FirstOrDefault(x=>x.Id == id);
            if(current == null)
                return;
            Context.AssociatedDocumentationCategoryPage.Remove(current);
            Context.SaveChanges();
        }
        internal void UpdateDocumentationPage(AssociatedDocumentationCategoryPage request)
        {
            var getItem = Context.AssociatedDocumentationCategoryPage.FirstOrDefault(x=>x.Id == request.Id);
            getItem.Title = request.Title;
            getItem.Content = request.Content;
            Context.Attach(getItem);
            Context.Update(getItem);
            Context.SaveChanges();
        }
        internal void AddNewDocumentationpage(AssociatedDocumentationCategoryPage request)
        {
            Context.AssociatedDocumentationCategoryPage.Add(request);
            Context.SaveChanges();
        }

        internal void AddNewDocumentationCategoryField(IncomingIdRequest request)
        {
            var getCategory = Context.DocumentationCategory.FirstOrDefault(x=>x.Id == request.Id);
            Context.DocumentationCategoryField.Add(new DocumentationCategoryField{
                CategoryId = getCategory.Id,
                PageName = request.Phase
            });
            Context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~DocumentationContext()
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
    }
}