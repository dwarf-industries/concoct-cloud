#pragma checksum "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "23edfdaf2aa5e1aa5f7c8d55ad5347b6ff5718ae"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Boards_Sprints), @"mvc.1.0.view", @"/Views/Boards/Sprints.cshtml")]
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
#line 1 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/_ViewImports.cshtml"
using Rokono_Control;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/_ViewImports.cshtml"
using Rokono_Control.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"23edfdaf2aa5e1aa5f7c8d55ad5347b6ff5718ae", @"/Views/Boards/Sprints.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a3d88b77ca5ff650022e07aede809867a9af767", @"/Views/_ViewImports.cshtml")]
    public class Views_Boards_Sprints : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/Sprints/Sprints.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#nullable restore
#line 1 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
  
    ViewData["Title"] = "Board";
    Layout = "~/Views/Shared/_LayoutDashboard.cshtml";
    var ProjectId = ViewData["ProjectId"] as int?;
    var WorkItemTypes = ViewData["WorkItemTypes"] as List<WorkItemTypes>;
    var ProjectName = ViewData["ProjectName"] as string;
    var Iteration = ViewData["Iteration"] as int?;
    var Person = ViewData["Person"] as int?;
    var ViewRights = ViewData["GetUserViewRights"] as int?;
 

#line default
#line hidden
#nullable disable
            WriteLiteral("<script src=\"https://code.jquery.com/jquery-3.4.1.min.js\"></script>\n\n<link href=\"https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css\" rel=\"stylesheet\">\n\n\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "23edfdaf2aa5e1aa5f7c8d55ad5347b6ff5718ae4590", async() => {
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
<!-- end of sidebar element -->
 
 
        <div id=""spinner"">
            <div id=""loader""></div>
        </div>
        <div id=""Content"" hidden>

    <div class=""main-card mb-12 card"">
            <div class=""card-body text-center"">

                <div class=""page-title-heading"">
                    <div class=""page-title-icon"">
                        <i class=""pe-7s-graph text-success"">
                        </i>
                    </div>
                    <div>
                        Project Dashboard
                        <div class=""page-title-subheading"">
                            ");
#nullable restore
#line 36 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                       Write(ProjectName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                        </div>\n                    </div>\n                </div>\n            </div>\n        </div>\n        <div class=\"row\">\n\n\n            <div class=\"col-md-5\">\n                <button type=\"button\" id=\"iconbtn\" class=\"btn btn-primary\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1537, "\"", 1686, 3);
            WriteAttributeValue("", 1547, "CallUrl(\'/Dashboard/AddNewWorkItem?projectId=\'+", 1547, 47, true);
#nullable restore
#line 46 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
WriteAttributeValue("", 1594, ProjectId, 1594, 10, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1604, "+\'&workItemType=\'+7+\'&boardId=\'+0+\'&parentId=0&returnUrl=\'+window.location.href+\')", 1604, 82, true);
            EndWriteAttribute();
            WriteLiteral(">New Sprint</button>\n                <button id=\"backlogBtn\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1747, "\"", 1831, 3);
            WriteAttributeValue("", 1757, "CallUrl(\'/Boards/ProjectBacklog?projectId=\'+", 1757, 44, true);
#nullable restore
#line 47 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
WriteAttributeValue("", 1801, ProjectId, 1801, 10, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1811, "+\'&&workItemType=7\')", 1811, 20, true);
            EndWriteAttribute();
            WriteLiteral(@">View as backlog</button>
                <button id=""capacityBtn"" onclick=""CallUrl()"">Capacity</button>
                <button id=""analitycsBtn"" onclick=""CallUrl()"">Analitycs</button>
                <button id=""CloseIteration"" onclick=""ChangeIteration()"">Close Iteration</button>

            </div>
            <div class=""col-md-3"">

            </div>
            <div class=""col-md-4"">

                ");
#nullable restore
#line 58 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
           Write(await Component.InvokeAsync("IterationDropdown", new IncomingIdRequest{

                    ProjectId = @ProjectId.Value,
                    Id = @Iteration.Value,
                    Phase = "Sprints"
                }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

                <button type=""button"" id=""Persons"" class=""btn btn-primary"">Persons</button>
                <div>
                    <label for=""publicCheck"" style=""padding: 10px 70px 10px 0"">Public</label>
                    <input id=""publicCheck"" type=""checkbox"" onchange=""MakeBoardPublic()"" />
                    <p id=""GeneratedLink""></p>
                </div>
            </div>




        </div>
        <div class=""row"">
            <div class=""cols-sample-area"" id=""board"">

                <div id=""KanbanHolder"">
                    <div id=""Kanban""></div>
                </div>

            </div>
        </div>
        <div class=""row text-center"">
            <h2>Empty User Stories</h2>
        </div>
        <div class=""row"">
            <div class=""control-section"" style=""height:97vh;"">
                <div class=""content-wrapper"">
                    <div id=""TreeGrid""></div>
                </div>
            </div>
        </div>


    <!-- Modals -->
    <div id=""AssignModel"">
        <di");
            WriteLiteral(@"v class=""btn-group dropdown-split-primary"" style=""text-align:center;"">
            <div class=""row"">
                <div id=""ProjectUsers""></div>
            </div>
            <div class=""row"">
                <button   id=""assignBtn"" onclick=""AssignNewUser()"">Assign</button>
                <button   id=""cancelBtn"" onclick=""CancelAssign()"">Cancel</button>

            </div>
        </div>
    </div>

    <div id=""newWorkItemModal"">
        <div class=""btn-group dropdown-split-primary"" style=""text-align:center;"">
             <button   id=""taskBtn"" onclick=""NewTask()"">Task</button>
            <button   id=""issueBtn"" onclick=""NewIssue()""></i>Issue</button>
            <button   id=""bugBtn""  onclick=""NewBug()"">Bug</button>
        </div>
    </div>

    <div id=""dialogIterations"">
        <div id=""IterationContent"">
            
        </div>
        <div  class=""row"">
            <div id=""Rowbtn"">
                <button class=""btn btn-primary e-control e-btn e-lib e-outline e-primary"" style=""width: 100%;");
            WriteLiteral(@""" onclick=""CloseIterationModal()"">Cancel</button>
            </div>
            <div id=""Rowbtns"" style=""display: ruby;"">
                <button class=""btn btn-primary e-control e-btn e-lib e-outline e-primary"" style=""width: 50%;"" onclick=""SaveNewIterationSettings()"">Confirm</button>
                <button class=""btn btn-primary e-control e-btn e-lib e-outline e-primary"" style=""width: 50%;"" onclick=""CloseIterationModal()"">Cancel</button>

            </div>
        </div>

    </div>

        </div>
    
 
<style>
       .swimlane-template {
        display: inline-block;
        font-size: 15px;
        font-weight: 500;
        width: 95%;
    }

  

    .swimlane-template span {
        padding-left: 10px;
    }
</style>


<script type=""text/javascript"">

        $(""#Rowbtns"").hide();
        var workItemName;
        var publicCheck = new ejs.buttons.Switch({ checked: false });
        publicCheck.appendTo('#publicCheck');

        var dialogObj;
        var iconbtn = new ej.buttons.Button({ cssClass: ");
            WriteLiteral(@"'e-outline', isPrimary: true });
        iconbtn.appendTo('#iconbtn');
        var backlogBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
        backlogBtn.appendTo('#backlogBtn');
        var capacityBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
        capacityBtn.appendTo('#capacityBtn');
        var analitycsBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
        analitycsBtn.appendTo('#analitycsBtn');
        var CloseIteration = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
        CloseIteration.appendTo('#CloseIteration');

        var assignBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
        assignBtn.appendTo('#assignBtn');

        var cancelBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
        cancelBtn.appendTo('#cancelBtn');

    // DropDownButton items declaration
        var dialogNewWI;
        var taskBtn = new ej.buttons.Button({ cssClass");
            WriteLiteral(@": 'e-outline', isPrimary: true });
        taskBtn.appendTo('#taskBtn');
        var issueBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
        issueBtn.appendTo('#issueBtn');
        var bugBtn = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
        bugBtn.appendTo('#bugBtn');
          
        var dialogIterations = new ej.popups.Dialog({
                width: '90vh',
                header: 'Attention, closing the active Iteration will move all work items that arent closed to the next iteration pleaes check your planning boards for items that havent been cloed',
                isModal: true,
                animationSettings: { effect: 'None' },
                visible: false,
                open: dialogOpenIterations,
                close: dialogCloseIterations
            });
        dialogIterations.appendTo('#dialogIterations');


        ej.base.enableRipple(true);
        var selectedChanging;
        var selectedRowData;
        var card;
        ");
            WriteLiteral(@"var gData;
        window.getTags = function (data) {
            var tagDiv = '<div class=""e-tags"">';
            var tags = data.Tags.split(',');
            for (var i = 0; i < tags.length; i++) {
                var tag = tags[i];
                tagDiv = tagDiv.concat('<div class=""e-tag e-tooltip-text"">' + tag + '</div>');
            }
            return tagDiv.concat('</div>');
        };

        $.ajax({
            type: 'GET',
            url: '/Accounts/GetProjectUsers?projectId=");
#nullable restore
#line 219 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                                                 Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
            contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            success: function (response) {
                InitliazePUsersGrid(response);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
        $(function() {
            
            dialogNewWI = new ej.popups.Dialog({
                width: '600px',
                header: 'Choose you the type of work item that you want to associate with the current user story!',
                isModal: true,
                animationSettings: { effect: 'None' },
                visible: false,

                open: dialogOpenWorkItem,
                close: dialogCloseWorkItem
            });
            dialogNewWI.appendTo('#newWorkItemModal');

            dialogObj = new ej.popups.Dialog({
                width: '600px',
                header: 'Chose a user',
                isModal: true,
                animationSettings: { effect: 'None' },");
            WriteLiteral(@"
                visible: false,
                open: dialogOpen,
                close: dialogClose
            });
            dialogObj.appendTo('#AssignModel');
            dialogObj.hide();
            var incomingSprintDTO =
            {
                ""ProjectId"": ");
#nullable restore
#line 256 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                        Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\n                \"WorkItemTypeId\": $(\"#ItemPriority :selected\").val(),\n                \"IterationId\": ");
#nullable restore
#line 258 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                          Write(Iteration);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\n                \"PersonId\": ");
#nullable restore
#line 259 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                       Write(Person);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\n                \"All\" : ");
#nullable restore
#line 260 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                   Write(ViewRights);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            }



             $.ajax({
                type: 'POST',
                url: '/Boards/GetSprints',
                data: JSON.stringify(incomingSprintDTO),
                contentType: ""application/json; charset=utf-8"",
                dataType: ""json"",
                success: function(response) {
                    GenerateKaban(response);
                },
                error: function(xhr, status, error) {
                    console.log(error);
                }
            });
     
            $.ajax({
                type: 'POST',
                url: '/Boards/GetPersons',
                data: JSON.stringify(iterationDTO),
                contentType: ""application/json; charset=utf-8"",
                dataType: ""json"",
                success: function(response) {
                    var Persons = new ej.splitbuttons.DropDownButton({ items: response, iconCss: 'e-ddb-icons e-profile',cssClass: 'e-outline', select:PersonClicked });
                    Persons.appendTo('#Persons');
   ");
            WriteLiteral("             },\n                error: function(xhr, status, error) {\n                    console.log(error);\n                }\n            });\n\n            \n            var dto = {\n                \"id\": ");
#nullable restore
#line 296 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                 Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@",
                ""WorkItemType"":7
            }
            console.log(dto);
            $.ajax({
                type: 'POST',
                url: '/Backlogs/GetEmptyStories',
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
          
            console.log(dto);
          



		 });


         function GenerateKaban(response)
         {
             $(""#KanbanHolder"").html(""<div id=\""Kanban\""></div>"");
             $(""#KanbanHolder"").html();

            var data = ej.base.extend([], response, null, true); // To maintain the property changes, extend the object.
            console.log(data);
            var kanbanObj = new ej.kanban.Kanban({
                dataSource: data,
");
            WriteLiteral(@"                dragStop: DragStopped,
                actionComplete: CardDropCheck,
                actionBegin: SwimlineBegin,
                cardDoubleClick: CardSelected,
                keyField: 'status',
                enableTooltip: true,
                columns: [
                    { headerText: 'To Do', keyField: 'Open', template: '#headerTemplate', allowToggle: true },
                    { headerText: 'In Progress', keyField: 'InProgress', template: '#headerTemplate', allowToggle: true },
                    { headerText: 'In Review', keyField: 'Testing', template: '#headerTemplate', allowToggle: true },
                    { headerText: 'Done', keyField: 'Done', template: '#headerTemplate', allowToggle: true }
                ],
                cardSettings: {
                    contentField: 'summary',
                    headerField: 'id',
                    template: '#cardTemplate',
                    selectionType: 'Multiple'
                },
                  swimlaneSettings: {
 ");
            WriteLiteral(@"                   keyField: 'assignee',
                    template: '#swimlaneTemplate',
                    showEmptyRow: true               
                },
            });


            kanbanObj.appendTo('#Kanban');
            ShowContent();
          }

       
        function CardSelected(args)
        {
            console.log(args);
            console.log(""/Dashboard/EditWorkItem?projectId=");
#nullable restore
#line 365 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                                                      Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral("&&workItem=\"+args.data.innerId);\n            window.location.href = \"/Dashboard/EditWorkItem?projectId=");
#nullable restore
#line 366 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                                                                 Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"&&workItem=""+args.data.innerId+""&returnUrl=""+window.location.href;

        }
        function CardDropCheck(args)
        {
            console.log(args);
            if(args.requestType == ""cardChanged"" && card)
            {
                card.Board = args.changedRecords[0].status;
                ValidateCardChanged();
            }
        }
        function ValidateCardChanged()
        {
            console.log(""Posting"");
            $.ajax({
                type: 'POST',
                url: '/Boards/ChangeWorkItemBoard',
                data: JSON.stringify(card),
                contentType: ""application/json; charset=utf-8"",
                dataType: ""json"",
                success: function(response) {
                    //window.location.href = ""/Dashboard/AssignAccountProjects?id=""+response;
                },
                error: function(xhr, status, error) {
                    console.log(error);
                }
            });
        }
         function DragStopped(args)
         {");
            WriteLiteral("\n            console.log(args);\n            card = {\n                \"CardId\" :args.data[0].innerId,\n                \"Board\" :args.data[0].status,\n                \"ProjectId\" : ");
#nullable restore
#line 401 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                         Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            }

         }
       

         function PersonClicked(args) {
             console.log(args);
         }

        function ChangeOwner(data)
        {

            selectedChanging = data;
            console.log(data);
        //    selectedChangingId = id;
            console.log(""Changing Owner"");
            dialogObj.show();
        }

         function CallUrl(url) {
             window.location.href = url;
    }

    function AssignNewUser()
    {
        selectedChanging.textContent = selectedRowData.name;
        ChangeCardOwner(selectedChanging.id, selectedRowData.name);
        CancelAssign();
    }
    function CancelAssign()
    {
        dialogClose();
        dialogObj.hide();

    }
    function dialogClose() {
            document.getElementById('AssignModel').style.display = 'none';

    }
    function dialogOpen() {
 
        document.getElementById('AssignModel').style.display = 'block';
    }


    function dialogOpenWorkItem()
    {
        document.getElementById('newWorkI");
            WriteLiteral(@"temModal').style.display = 'block';
    }

    function dialogCloseWorkItem()
    {
        document.getElementById('newWorkItemModal').style.display = 'none';
    }
    function InitliazePUsersGrid(data)
    {
        console.log(data);
         var ProjectUsers = new ej.treegrid.TreeGrid({
            dataSource: data,
            allowSelection: true,
            allowFiltering: true,
            allowSorting: true,
            toolbar: ['Search'],
            rowSelected: UserRowSelected,
            recordDoubleClick: UserSelected,
            filterSettings: { type: 'Menu' },
            enableHover: false,
            columns: [
                 { field: 'name', headerText: 'Email', width: 110 },
            ]
        });

        ProjectUsers.appendTo('#ProjectUsers');
    }
    function UserRowSelected(args)
    {
        selectedRowData = args.data;
        console.log(args);
    }
    function UserSelected(args)
    {
        selectedChanging.textContent = args.rowData.name;
        CancelAssign();");
            WriteLiteral(@"
        ChangeCardOwner(selectedChanging.id, args.rowData.name);
    }
    function InitiliazeGrid()
    {
        console.log(data);


        var treeGridObj = new ej.treegrid.TreeGrid({
            dataSource: data,
            allowSelection: true,
            allowFiltering: true,
            allowSorting: true,
            toolbar: ['Search'],
            recordDoubleClick: EditWorkItem,
            enableVirtualization: true,
            filterSettings: { type: 'Menu' },
            selectionSettings: { persistSelection: true, type: ""Multiple"" },
            enableHover: false,
            allowPaging: true,
            pageSettings: { pageSize: 20 },
            columns: [
                { type: ""checkbox"", field: """", allowFiltering: false, allowSorting: false, width: '60' },
                { field: 'workItemType.TypeName', headerText: 'Type', width: 60 },
                { field: 'title', headerText: 'Title', width: 125 },
                { field: 'description', headerText: 'Description', width: 1");
            WriteLiteral(@"80 },
                { field: 'assignedAccountNavigation.Email', headerText: 'AssignedTo', width: 110 },
            ]
        });

        treeGridObj.appendTo('#TreeGrid');


    }

    function EditWorkItem(args)
    {
        console.log(args);
        window.location.href = ""/Dashboard/EditWorkItem?projectId=");
#nullable restore
#line 522 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                                                             Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"&&workItem=""+args.rowData.id+""&returnUrl=""+window.location.href;
    }

    function ChangeCardOwner(id,name)
    {
        var incomingSprintDTO =
        {
            ""CardId"": id,
            ""Name"": name
        }

        $.ajax({
            type: 'POST',
            url: '/Boards/ChangeCardOwner',
            data: JSON.stringify(incomingSprintDTO),
            contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            success: function(response) {
                
            },
            error: function(xhr, status, error) {
                console.log(error);
            }
        });
    }

    function MakeBoardPublic()
    {
        var isChecked;
        if (publicCheck.properties.checked === true)
             isChecked = 1;
        else
            isChecked = 0;


          var incomingSprintDTO =
        {
            ""ProjectId"": ");
#nullable restore
#line 559 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                    Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@",
            ""isChecked"": isChecked
        }

        $.ajax({
            type: 'POST',
            url: '/Boards/MakeBoardPublic',
            data: JSON.stringify(incomingSprintDTO),
            contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            success: function(response) {
                $(""#GeneratedLink"").html(response.data);
            },
            error: function(xhr, status, error) {
                console.log(error);
            }
        });
    }
    function NewWorkItem(btn)
    {
        console.log(btn.id);
        workItemName = btn.id;
        dialogNewWI.show();

    }

    function SwimlineBegin(args)
    {
 
    }
    function ChangeIteration()
    {
        dialogIterations.show();
        $(""#IterationContent"").load(""/Boards/GetIterationChanger?projectId=");
#nullable restore
#line 592 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                                                                      Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\");\n");
            WriteLiteral("    }\n\n    function NewTask(){\n        window.location.href = \"/Dashboard/AddNewWorkItem?projectId=");
#nullable restore
#line 597 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                                                               Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral("&workItemType=\"+3+\"&boardId=0&parentId=0&&returnUrl=\"+window.location.href+\"&title=\"+workItemName;\n    }\n    function NewIssue(){\n        window.location.href = \"/Dashboard/AddNewWorkItem?projectId=");
#nullable restore
#line 600 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                                                               Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral("&workItemType=\"+6+\"&boardId=0&parentId=0&&returnUrl=\"+window.location.href+\"&title=\"+workItemName;\n    }\n    function NewBug(){\n        window.location.href = \"/Dashboard/AddNewWorkItem?projectId=");
#nullable restore
#line 603 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Boards/Sprints.cshtml"
                                                               Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"&workItemType=""+1+""&boardId=0&parentId=0&&returnUrl=""+window.location.href+""&title=""+workItemName;
    }

    function dialogOpenIterations()
    {
        document.getElementById('dialogIterations').style.display = 'block';
    }

    function dialogCloseIterations()
    {
        document.getElementById('dialogIterations').style.display = 'none';
    }

    function CloseIterationModal()
    {
        $(""#Rowbtns"").hide();
        $(""#Rowbtn"").show();
        dialogIterations.hide();
    }
</script>


<script id=""headerTemplate"" type=""text/x-jsrender"">
    <div class=""header-template-wrap"">
        <div class=""header-icon e-icons ${keyField}""></div>
        <div class=""header-text"">${headerText}</div>

    </div>
</script>
<script id=""cardTemplate"" type=""text/x-jsrender"">
    <div class='card-template ${priority}' style='content: ""\e511 "";'>
        <div class='card-header e-tooltip-text'>
            <div class=""row"" style=""display:flex;"">
                <div class=""col-2"">
                    <span class");
            WriteLiteral(@"=""e-search ${priority}""></span>
                </div>
                <div class=""col-10"" style=""width:100%; text-align:center;"">
                    <span style=""text-align:center;"">${id} ${title}</span>
                </div>
            </div>
        </div>


        <div class='card-template-wrap'>
            <div class=""row"" style=""padding:2%;"">
                <div class='e-text'>${summary}</div>

            </div>
            <div class=""row"" style=""padding:2%;"">
                <div class='Interactice' onclick='ChangeOwner(this)' id='${id}'>${assgignedAccount}</div>
            </div>
            <div class=""row status"" style=""display:flex;padding:2%;"">

                <div class=""col-3"">
                    <span class='status'></span><div class='e-text' style=""text-align:left;"">Status:</div>
                </div>
                <div class=""col-9"" style=""width:100%;"">
                    <div class='Interactice'>${status}</div>
                </div>
            </div>

        </div>

    </d");
            WriteLiteral(@"iv>
</script>

<script id=""swimlaneTemplate"" type=""text/x-jsrender"">
     <div style=""width:100%; display:inline-block"">
        <span >${textField}</span>
        <button class=""e-control e-btn e-lib e-outline e-primary"" style=""margin-left:10px;"" id=""${textField}""  onclick='NewWorkItem(this)' ><span class=""e-icons plus"">New Work Item</span></button>
    </div>  
</script>");
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