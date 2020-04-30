#pragma checksum "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Boards/ProjectBacklog.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "35faa03576e56a5da04f22a3e683f1682d3e1dd4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Boards_ProjectBacklog), @"mvc.1.0.view", @"/Views/Boards/ProjectBacklog.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/_ViewImports.cshtml"
using Rokono_Control;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/_ViewImports.cshtml"
using Rokono_Control.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"35faa03576e56a5da04f22a3e683f1682d3e1dd4", @"/Views/Boards/ProjectBacklog.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a3d88b77ca5ff650022e07aede809867a9af767", @"/Views/_ViewImports.cshtml")]
    public class Views_Boards_ProjectBacklog : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/Syncfusion/ej2/material.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/Syncfusion/ej2/dist/ej2.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Boards/ProjectBacklog.cshtml"
  
    ViewData["Title"] = "Backlog";
    Layout = "~/Views/Shared/_LayoutDashboard.cshtml";
    var result = ViewData["ProjectId"] as int?;
    var WorkItemType = ViewData["WorkItemType"] as int?;
    var WorkItemTypeName = ViewData["GetSelectedWorkItem"] as string;
    var returnUrl = ViewData["ReturnUrl"] as string;

#line default
#line hidden
#nullable disable
            WriteLiteral("<link href=\"https://cdn.quilljs.com/1.3.6/quill.snow.css\" rel=\"stylesheet\">\n<!-- Main Quill library -->\n<script src=\"//cdn.quilljs.com/1.3.6/quill.js\"></script>\n<script src=\"//cdn.quilljs.com/1.3.6/quill.min.js\"></script>\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "35faa03576e56a5da04f22a3e683f1682d3e1dd44966", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

<style>
    .e-gridcontent{
        max-height: 80vh;
    }
</style>

 <!-- end of sidebar element -->
 
 
        <div id=""spinner"">
             <div id=""loader""></div>
        </div>
        <div id=""Content"" style=""display: none;"">
            <div class=""col-md-12"">
                <div class=""main-card mb-3 card"">
                    <div class=""card-body"">
                        <h5 class=""card-title text-center"">Select a project</h5>
                        <div class=""row"" style=""margin-left:10px;"">

                            <div class=""col-md-5"">
                                <button type=""button"" id=""iconbtn"" class=""btn btn-primary"">New Work Item</button>

                                <button id=""QueryBtn"">View as board</button>
                                <button id=""ImportWorkItemBtn"" onclick=""GenerateMailModal()""></i>Email report</button>
                                <button id=""ExportWorkItemBtn"" onclick=""ExportWorkItems()"">Export Work Items</button>
                          ");
            WriteLiteral("  </div>\n                            <div class=\"col-md-3\">\n\n                            </div>\n                            <div class=\"col-md-4\">\n                                <button id=\"WorkItemTypeBtnFilter\" class=\"btn btn-primary\">");
#nullable restore
#line 46 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Boards/ProjectBacklog.cshtml"
                                                                                      Write(WorkItemTypeName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</button>
                            </div>
                        </div>
                        <div class=""row"">
                            <div class=""control-section"" style=""height:97vh;"">
                                <div class=""content-wrapper"">
                                    <div id=""TreeGrid""></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

    <div id=""modalDialog"">
        <div class=""btn-group dropdown-split-primary"" style=""text-align:center;"">
             <button   id=""taskBtn"" onclick=""NewTask()"">Task</button>
            <button   id=""issueBtn"" onclick=""NewIssue()""></i>Issue</button>
            <button   id=""bugBtn"" onclick=""NewBug()"">Bug</button>
        </div>
    </div>
    <div id=""emailReport"">
        <div class=""row"" style=""padding:2%;"">
            <p>
                Are you sure that you want to take the curr");
            WriteLiteral(@"ent baclog and generate a email report for all the items inside?
            </p>
        </div>
        <div class=""row"">
                <div class=""col-md-6"" style=""text-align: center;"">
                <button id=""CancelEmailReport"" onclick=""CancelEmailConfirm()"">Cancel</button>
            </div>
            <div class=""col-md-6"" style=""text-align: center;"">
                <button id=""GenerateReport"" onclick=""EmailConfirm()"">Confirm</button>
            </div>
        </div>
    </div>
    <div id=""exportDialog"">
        <div class=""row"">
            <button id=""CancelExportBtn"" style=""width: 100%;"" onclick=""CancelExport()"">Close</button>
        </div>
        <div class=""row"" style=""padding:2%;"">
            <div class=""position-relative form-group"" style=""width:100%;height:300px;"" ><label for=""exportContent""");
            BeginWriteAttribute("class", "  class=\"", 3739, "\"", 3748, 0);
            EndWriteAttribute();
            WriteLiteral(@">Manual Creation Script</label><div name=""exportContent"" id=""exportContent""  style=""max-height: 400px;""  class=""form-control""></div></div>
        </div>
 
  <script src=""https://code.jquery.com/jquery-3.4.1.min.js"" ></script>
<script src=""https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"" integrity=""sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo"" crossorigin=""anonymous""></script>
<script src=""https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"" integrity=""sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6"" crossorigin=""anonymous""></script>
    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "35faa03576e56a5da04f22a3e683f1682d3e1dd410475", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\n <script>\n    ej.treegrid.TreeGrid.Inject(ej.treegrid.Edit, ej.treegrid.CommandColumn);\n    var exportContent;\n    var treeGridObj;\n    var dialogObj;\n    var emailDialog;\n    var exportDialog;\n    var ProjectId = ");
#nullable restore
#line 105 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Boards/ProjectBacklog.cshtml"
               Write(result);

#line default
#line hidden
#nullable disable
            WriteLiteral(@";
    var boardId = 0;
    var selected;
    function OpenWorkItem(id)
    {
        console.log(id);
    }
    $( document ).ready(function() 
    {
        exportContent = new Quill('#exportContent', {
            theme: 'snow',
            height: '300px'
        });
        var id = ");
#nullable restore
#line 118 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Boards/ProjectBacklog.cshtml"
            Write(result);

#line default
#line hidden
#nullable disable
            WriteLiteral(";\n        \n          var dto = {\n            \"id\": id,\n            \"WorkItemType\": ");
#nullable restore
#line 122 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Boards/ProjectBacklog.cshtml"
                       Write(WorkItemType);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        }
        console.log(dto);
        $.ajax({
            type: 'POST',
            url: '/Backlogs/GetBacklogWorkItems',
            data: JSON.stringify(dto),
            contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            success: function (response) {
                InitiliazeGrid(response);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
        dialogObj = new ej.popups.Dialog({
            width: '600px',
            header: 'Choose you the type of work item that you want to associate with the current user story!',
            isModal: true,
            animationSettings: { effect: 'None' },
            visible: false,

            open: dialogOpen,
            close: dialogClose
        });
        dialogObj.appendTo('#modalDialog');
        emailDialog = new ej.popups.Dialog({
            width: '600px',
            header: 'Backlog email generator',
            isModal: true");
            WriteLiteral(@",
            animationSettings: { effect: 'None' },
            visible: false,

            open: EmailDialogOpen,
            close: EmailDialogClose
        });
        emailDialog.appendTo('#emailReport');
        exportDialog = new ej.popups.Dialog({
            width: '600px',
            header: 'Project Backup',
            isModal: true,
            animationSettings: { effect: 'None' },
            visible: false,
            open: ExportDialogOpen,
            close: ExportDialogClose
        });
        exportDialog.appendTo('#exportDialog');
        dialogObj.hide();
       exportDialog.hide();
        emailDialog.hide();
        
    });


    ej.base.enableRipple(true);
    //Menu Btns
    var queryBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
    queryBtn.appendTo('#QueryBtn');
    var importWorkItemBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
    importWorkItemBtn.appendTo('#ImportWorkItemBtn');
    var exportWorkItemBtn = new ej.buttons");
            WriteLiteral(@".Button({ cssClass: 'e-outline', isPrimary: true });
    exportWorkItemBtn.appendTo('#ExportWorkItemBtn');
    var recycleBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
    recycleBtn.appendTo('#RecycleBtn');


    
    //Dialog btns
    var taskBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
    taskBtn.appendTo('#taskBtn');
    var bugBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
    bugBtn.appendTo('#bugBtn');
    var issueBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
    issueBtn.appendTo('#issueBtn');
    var CancelEmailReport = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
    CancelEmailReport.appendTo('#CancelEmailReport');
    var GenerateReport = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
    GenerateReport.appendTo('#GenerateReport');

    var CancelExportBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
    CancelExportBtn.appe");
            WriteLiteral(@"ndTo('#CancelExportBtn');


    // DropDownButton items declaration
    var items = [
        {
            text: 'Feature',
            iconCss: 'e-ddb-icons e-dashboard',
          
            url:""/Dashboard/AddNewWorkItem?projectId=""+ProjectId+""&workItemType=""+2+""&boardId=""+boardId+""&parentId=0&returnUrl=""+window.location.href
        },
        {
            text: 'User Story',
            iconCss: 'e-ddb-icons e-notifications',
             url:""/Dashboard/AddNewWorkItem?projectId=""+ProjectId+""&workItemType=""+7+""&boardId=""+boardId+""&parentId=0&returnUrl=""+window.location.href

        },
        {
            text: 'Task',
            iconCss: 'e-ddb-icons e-settings',
            url:""/Dashboard/AddNewWorkItem?projectId=""+ProjectId+""&workItemType=""+3+""&boardId=""+boardId+""&parentId=0&returnUrl=""+window.location.href

        },
        {
            text: 'Bug',
            iconCss: 'e-ddb-icons e-logout',
            url:""/Dashboard/AddNewWorkItem?projectId=""+ProjectId+""&workItemType=""+1+""&boardId=""+b");
            WriteLiteral(@"oardId+""&parentId=0&returnUrl=""+window.location.href
        },
        {
            text: 'Issue',
            iconCss: 'e-ddb-icons e-logout',
            url:""/Dashboard/AddNewWorkItem?projectId=""+ProjectId+""&workItemType=""+6+""&boardId=""+boardId+""&parentId=0&returnUrl=""+window.location.href

        },
        {
            text: 'Test',
            iconCss: 'e-ddb-icons e-logout',
            url:""/Dashboard/AddNewWorkItem?projectId=""+ProjectId+""&workItemType=""+4+""&boardId=""+boardId+""&parentId=0&returnUrl=""+window.location.href
         }
        
        ];

    var btnObj = new ej.splitbuttons.DropDownButton({ items: items, iconCss: 'e-ddb-icons e-profile',cssClass: 'e-outline', select:WorkItemListClicked });
     btnObj.appendTo('#iconbtn');

     var workItemCollection = [
        {
            text: 'Epic',
            iconCss: 'e-ddb-icons e-dashboard',
            url:"" /Boards/ProjectBacklog?projectId=""+ProjectId+""&workItemType=""+5+""&boardId=""+boardId+""&parentId=0&returnUrl=""+window.location.href");
            WriteLiteral(@"
        },
        {
            text: 'Feature',
            iconCss: 'e-ddb-icons e-dashboard',
          
            url:"" /Boards/ProjectBacklog?projectId=""+ProjectId+""&workItemType=""+2+""&boardId=""+boardId+""&parentId=0&returnUrl=""+window.location.href
        },
        {
            text: 'User Story',
            iconCss: 'e-ddb-icons e-notifications',
            url:"" /Boards/ProjectBacklog?projectId=""+ProjectId+""&workItemType=""+7+""&boardId=""+boardId+""&parentId=0&returnUrl=""+window.location.href

        },
       
        
        ];

        var WorkItemTypeBtnFilter = new ej.splitbuttons.DropDownButton({ items: workItemCollection, iconCss: 'e-ddb-icons e-profile',cssClass: 'e-outline', select:WorkItemListClicked });
        WorkItemTypeBtnFilter.appendTo('#WorkItemTypeBtnFilter');

    function WorkItemListClicked(args)
    {
        console.log(args);
        var url = args.properties.url;
        window.location.href = url;
       // console.log(args);
    }

    var onClick = function(args){ 
");
            WriteLiteral(@"        var rowIndex = ej.base.closest(args.target, '.e-row').rowIndex;
        var data = treeGridObj.getCurrentViewRecords();
        console.log(data[rowIndex]);
        if(data[rowIndex].typeId === 5)
            window.location.href = ""/Dashboard/AddNewWorkItem?projectId=""+ProjectId+""&workItemType=""+2+""&boardId=""+boardId+""&parentId=""+data[rowIndex].id+""&&returnUrl=""+window.location.href;
        if(data[rowIndex].typeId === 2)
            window.location.href = ""/Dashboard/AddNewWorkItem?projectId=""+ProjectId+""&workItemType=""+7+""&boardId=""+boardId+""&parentId=""+data[rowIndex].id+""&&returnUrl=""+window.location.href;
        if(data[rowIndex].typeId === 7)
            CallChoiceMenu(data[rowIndex]);
     } 
    function CallChoiceMenu(row)
    {
        selected = row;
        dialogObj.show();
    }

    function GenerateMailModal()
    {
        emailDialog.show();
    }

    function InitiliazeGrid(data )
    {
        console.log(data);


        treeGridObj = new ej.treegrid.TreeGrid({
            data");
            WriteLiteral(@"Source: data,
            allowSelection: true,
            allowFiltering: true,
            allowSorting: true,
            allowResizing: true,
            rowSelecting : GetCurrentRow,
            childMapping: 'subtasks',
            toolbar: ['Search'],
            height: '80vh',
            recordDoubleClick: EditWorkItem,
            filterSettings: { type: 'Menu' },
            treeColumnIndex: 1,
            allowPaging: true,
            cellEdit: AddNewWorkItem,
            pageSettings: { pageCount: 2 },
            columns: [
                {
                    headerText: 'Manage Records', width: 130,
                    commands: [{ type: 'Edit', buttonOption: { iconCss: ' e-icons e-edit', cssClass: 'e-flat', click:onClick } },
                   ]
                },
                { field: 'WorkItemType.TypeName', headerText: 'Title', width: 60 },
                { field: 'title', headerText: 'Title', width: 125 },
                { field: 'description', headerText: 'Description', width: ");
            WriteLiteral(@"180 },
                { field: 'assignedAccountNavigation.Email', headerText: 'AssignedTo', width: 110 },
            ]
        });

        treeGridObj.appendTo('#TreeGrid');

        ShowContent();
    }

     
    function CreateNewWorkItem(id){
        ProjectId = ");
#nullable restore
#line 344 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Boards/ProjectBacklog.cshtml"
               Write(result);

#line default
#line hidden
#nullable disable
            WriteLiteral(@";
        boardId = 0;
        window.location.href = ""/Dashboard/AddNewWorkItem?projectId=""+ProjectId+""&workItemType=""+id+""&boardId=""+boardId+""&parentId=0&&returnUrl=""+window.location.href;
    }
    
    function GetCurrentRow(args)
    {
        console.log(args);
        selected = args.data;
    }
    function WorkItemSelected(args){
        console.log(args);
    }

    function EditWorkItem(args)
    {
        console.log(args);
        window.location.href = ""/Dashboard/EditWorkItem?projectId=");
#nullable restore
#line 361 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Boards/ProjectBacklog.cshtml"
                                                             Write(result);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"&&workItem=""+args.rowData.id+""&&returnUrl=""+window.location.href;
    }
    function load(args) {
        this.parentDetails.parentKeyFieldValue = this.parentDetails.parentRowData['id'];
    }

    function AddNewWorkItem(args)
    {
        console.log(args);
    }

    function dialogClose() {
            document.getElementById('dialogBtn').style.display = 'block';
    }
    function dialogOpen() {
        document.getElementById('dialogBtn').style.display = 'none';
    }

    // Dialog will be closed, while clicking on overlay
    function onChange(args) {
        if(args.checked) {
            dialogObj.overlayClick = function () {
                dialogObj.hide();
            };
        }
        else {
            dialogObj.overlayClick = function () {
                dialogObj.show();
            };
        }
    }


    function NewTask(){
        window.location.href = ""/Dashboard/AddNewWorkItem?projectId=""+ProjectId+""&workItemType=""+3+""&boardId=""+boardId+""&parentId=""+selected.id+""&&returnUrl=""+wind");
            WriteLiteral(@"ow.location.href;
    }
    function NewIssue(){
        window.location.href = ""/Dashboard/AddNewWorkItem?projectId=""+ProjectId+""&workItemType=""+6+""&boardId=""+boardId+""&parentId=""+selected.id+""&&returnUrl=""+window.location.href;
    }
    function NewBug(){
        window.location.href = ""/Dashboard/AddNewWorkItem?projectId=""+ProjectId+""&workItemType=""+1+""&boardId=""+boardId+""&parentId=""+selected.id+""&&returnUrl=""+window.location.href;
    }


    function EmailDialogOpen()
    {
        document.getElementById('emailReport').style.display = 'block';
    }
    function EmailDialogClose()
    {
        document.getElementById('emailReport').style.display = 'none';

    }
    function ExportDialogOpen()
    {
        document.getElementById('exportDialog').style.display = 'block';
    }
    function ExportDialogClose()
    {
        document.getElementById('exportDialog').style.display = 'none';

    }
    function EmailConfirm()
    {
        var id = ");
#nullable restore
#line 425 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Boards/ProjectBacklog.cshtml"
            Write(result);

#line default
#line hidden
#nullable disable
            WriteLiteral(@";
        
        var dto = {
            ""ProjectId"": id,
            ""Items"": treeGridObj.dataSource
        }
        console.log(dto);
        $.ajax({
            type: 'POST',
            url: '/Notification/GenerateBacklogReport',
            data: JSON.stringify(dto),
            contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            success: function (response) {
                CancelEmailConfirm();
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    }
    function ExportWorkItems()
    {
        var id = ");
#nullable restore
#line 448 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Boards/ProjectBacklog.cshtml"
            Write(result);

#line default
#line hidden
#nullable disable
            WriteLiteral(@";
    
        var dto = {
            ""ProjectId"": id,
         }
        console.log(dto);
        $.ajax({
            type: 'POST',
            url: '/Boards/ExportWorkItems',
            data: JSON.stringify(dto),
            contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            success: function (response) {
                alert(""Project has been backup up in your server backup folder. We have also generated a creation script in case something goes wrong with your server!!!"");
                $(""#exportContent"").children()[0].innerHTML = response.data;
                exportDialog.show();

            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    }

    function CancelEmailConfirm()
    {
        emailDialog.hide();
    }
     function CancelExport()
    {
        exportDialog.hide();
    }
 </script>


");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
