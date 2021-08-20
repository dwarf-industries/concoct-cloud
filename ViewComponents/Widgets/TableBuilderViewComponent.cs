namespace Platform.ViewComponents.Widgets
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Platform.Models;
    using Rokono_Control.Models;

    [ViewComponent(Name = "TableBuilder")]
    public class TableBuilderViewComponent : ViewComponent
    {
        private readonly RokonocontrolContext Context;
        private readonly IConfiguration Configuration;

        public TableBuilderViewComponent(RokonocontrolContext context, IConfiguration config)
        {
            Context = context;
            Configuration = config;
        }

        public IViewComponentResult Invoke(string markdown)
        {
            var tableBinding = new TableBinding();
            var rows = markdown.Split("|");
            var columns = rows[0].Split("#");
            var bindingColumns  = columns.ToList();
            bindingColumns.RemoveAt(0);
            var bindingRows = rows.ToList();
            bindingRows.RemoveAt(0);
            tableBinding.Columns = bindingColumns;
            tableBinding.Rows = bindingRows;
            ViewData["BindingTable"] = tableBinding;
            return View("/Views/Shared/Components/Widgets/TableBuilder/Default.cshtml");
        }
    }
}