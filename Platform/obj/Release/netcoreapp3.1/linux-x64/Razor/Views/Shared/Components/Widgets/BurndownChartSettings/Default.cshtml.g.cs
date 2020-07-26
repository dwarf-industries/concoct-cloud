#pragma checksum "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "75f44756f397f4f86eb48946c5cefe5794dadb55"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Widgets_BurndownChartSettings_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"75f44756f397f4f86eb48946c5cefe5794dadb55", @"/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a3d88b77ca5ff650022e07aede809867a9af767", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Widgets_BurndownChartSettings_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
  
    var Teams = ViewData["Teams"] as List<Teams>;
    var WorkItemTypes = ViewData["WorkItemTypes"] as List<WorkItemTypes>;
    var CountWItems = ViewData["CountWItems"] as List<Platform.Models.OutgoingBindingCollection>;
    var BurndownOn = ViewData["BurndownOn"] as List<Platform.Models.OutgoingBindingCollection>;
    var SumWItems = ViewData["SumWItems"] as List<Platform.Models.OutgoingBindingCollection>;
    var Backlogs = ViewData["Backlog"]  as List<Platform.Models.OutgoingBindingCollection>;
    var ProjectId = ViewData["ProjectId"] as int?;
    var ViewComponentId = ViewData["ViewComponentId"] as int?;
    var Dashboard = ViewData["Dashboard"] as int?;
 

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    #dialogIterations{
        height:98vh;
    }

    .controlContainer .row{
        padding-top:4%;
        padding-bottom: 4%;
    }
</style>

<div class=""controlContainer"">
    <div class=""row"">
        <input type=""text"" id='ChartTitle' />

    </div>
    <div class=""row"">
             <select class=""form-control-sm form-control"" id=""Teams"">
");
#nullable restore
#line 31 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                 foreach(var team in @Teams)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "75f44756f397f4f86eb48946c5cefe5794dadb554857", async() => {
#nullable restore
#line 33 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                                        Write(team.TeamName);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 33 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                       WriteLiteral(team.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 34 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </select>
        </div>
    </div>
    <div class=""radio-control"">
        <div class=""row"">
                <input id=""radio1"" type=""radio"" onclick=""ChangeBurndownDataType(1)"">
                <select class=""form-control-sm form-control"" id=""Backlog"">
");
#nullable restore
#line 42 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                     foreach(var backlog in @Backlogs)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "75f44756f397f4f86eb48946c5cefe5794dadb557613", async() => {
#nullable restore
#line 44 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                                               Write(backlog.Name);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 44 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                           WriteLiteral(backlog.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 45 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </select>\n            </div>\n        </div>\n        <div class=\"row\">\n                <input id=\"radio2\" type=\"radio\" onclick=\"ChangeBurndownDataType(2)\">\n                <select class=\"form-control-sm form-control\" id=\"WorkItemTypes\">\n");
#nullable restore
#line 52 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                     foreach(var workItemType in @WorkItemTypes)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "75f44756f397f4f86eb48946c5cefe5794dadb5510385", async() => {
#nullable restore
#line 54 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                                                    Write(workItemType.TypeName);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 54 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                           WriteLiteral(workItemType.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 55 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </select>\n            </div>\n        </div>\n    </div>\n    <div class=\"row\">\n        <div class=\"col-md-3\">\n            <select class=\"form-control-sm form-control\" id=\"BurndownOn\">\n");
#nullable restore
#line 63 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                 foreach(var burndownOn in @BurndownOn)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "75f44756f397f4f86eb48946c5cefe5794dadb5513103", async() => {
#nullable restore
#line 65 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                                              Write(burndownOn.Name);

#line default
#line hidden
#nullable disable
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 65 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                       WriteLiteral(burndownOn.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 66 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            </select>
        </div>
        <div class=""col-md-2"" style=""text-align: center;font-family: ""Roboto"", ""Segoe UI"", ""GeezaPro"", ""DejaVu Serif"", ""sans-serif"", ""-apple-system"", ""BlinkMacSystemFont"";font-weight: bolder;"">
            Of
        </div>
        <div class=""col-md-7"">
            <div id=""CountWorkItems"">
                <select class=""form-control-sm form-control"" id=""CountWItems"">
");
#nullable restore
#line 75 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                     foreach(var countWItems in @CountWItems)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <option");
            BeginWriteAttribute("value", " value=\"", 2965, "\"", 2988, 1);
#nullable restore
#line 77 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
WriteAttributeValue("", 2973, countWItems.Id, 2973, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 77 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                                                   Write(countWItems.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</option>\n");
#nullable restore
#line 78 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </select>\n            </div>\n           <div id=\"SumWorkItems\">\n                <select class=\"form-control-sm form-control\" id=\"SumWItems\">\n");
#nullable restore
#line 83 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                     foreach(var sumWItem in @SumWItems)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <option");
            BeginWriteAttribute("value", " value=\"", 3306, "\"", 3326, 1);
#nullable restore
#line 85 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
WriteAttributeValue("", 3314, sumWItem.Id, 3314, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 85 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                                                Write(sumWItem.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</option>\n");
#nullable restore
#line 86 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                </select>
           </div>
        </div>
    </div>
    <div class=""row"">
        <button   class=""btn btn-primary e-control e-btn e-lib e-outline e-primary"" style=""width: 100%;""  onclick=""SaveBurndownChartSettings()"" >Add Widget</button>
    </div>
<script>
    var BacklogBindingType = 1;
    var BurndownOnSelect = 1; 

    var BacklogSelectedType = 0;
    var WorkItemTypeSelected = 0;
    var CountWItemSelected = 0;
    var SumWItemSelected = 0;
    $(""#SumWorkItems"").hide();
     var ChartTitle = new ej.inputs.TextBox({
        placeholder: 'Burndown Chart',
        floatLabelType: 'Auto',
        enableRtl: true
    });
    ChartTitle.appendTo('#ChartTitle');
    var Teams = new ej.dropdowns.DropDownList({

        // set the placeholder to DropDownList input element
        placeholder: 'Teams',
        // set the height of the popup element
        popupHeight: '200px',
        index: 0,
        
    });
    Teams.appendTo('#Teams');

    var Backlog = new ej.dropdowns.DropDownList({

");
            WriteLiteral(@"        // set the placeholder to DropDownList input element
        placeholder: 'Backlog',
        change: BacklogTypeChanged,
        // set the height of the popup element
        popupHeight: '200px',
        index: 0,
        
    });
    Backlog.appendTo('#Backlog');
   
    var WorkItemTypes = new ej.dropdowns.DropDownList({

        // set the placeholder to DropDownList input element
        placeholder: 'Work Item Types',
        // set the height of the popup element
        popupHeight: '200px',
        change: WorkItemTypeChanged,
        index: 0,
        enabled : false
        
    });
    WorkItemTypes.appendTo('#WorkItemTypes');

    var BurndownOn = new ej.dropdowns.DropDownList({

        // set the placeholder to DropDownList input element
        placeholder: 'Burndown',
        // set the height of the popup element
        popupHeight: '200px',
        change: BurndownOnChanged,
        index: 0,
        
    });
    BurndownOn.appendTo('#BurndownOn');

    var CountWItems = new ej.dr");
            WriteLiteral(@"opdowns.DropDownList({

        // set the placeholder to DropDownList input element
        placeholder: 'Work Item Types',
        // set the height of the popup element
        popupHeight: '200px',
        change: CountWItemChanged,

        index: 0,
        
    });
    CountWItems.appendTo('#CountWItems');


    var SumWItems = new ej.dropdowns.DropDownList({

        // set the placeholder to DropDownList input element
        placeholder: 'Work Item Types',
        // set the height of the popup element
        popupHeight: '200px',
        enabled: false,
        change: SumWItemChanged,

        index: 0,
        
    });
    SumWItems.appendTo('#SumWItems');


    

    var radioButton = new ej.buttons.RadioButton({ label: 'Backlog', name: 'Backlog',  value: '1', checked: true });
    radioButton.appendTo('#radio1');

    var radioButton1 = new ej.buttons.RadioButton({ label: 'Work Item Type',name: 'Work Item Type', value: '2' });
    radioButton1.appendTo('#radio2');



    function ChangeBurndow");
            WriteLiteral(@"nDataType(args)
    {
        console.log(args);
        if(args == 1)
        {
            WorkItemTypes.enabled = false;
            Backlog.enabled = true;
            radioButton.checked = true;
            radioButton1.checked = false;
        }
        else
        {
            WorkItemTypes.enabled = true;
            Backlog.enabled = false;
            radioButton.checked = false;
            radioButton1.checked = true;
        }
        BacklogBindingType = args;
    }


    function BurndownOnChanged(args)
    {
        console.log(args);
        if(args.value === ""1"")
        {
            CountWItems.enabled = true;
            SumWItems.enabled = false;
            $(""#CountWorkItems"").show();
            $(""#SumWorkItems"").hide();
            BurndownOnSelect = 1; 


        }
        else
        {
            CountWItems.enabled = false;
            SumWItems.enabled = true;
            $(""#CountWorkItems"").hide();
            $(""#SumWorkItems"").show();
            BurndownOnSelect = 2;

 ");
            WriteLiteral(@"       }
    }
    function BacklogTypeChanged(args)
    {
        console.log(args);
        BacklogSelectedType = parseInt(args.value);
    }
    function WorkItemTypeChanged(args)
    {
        WorkItemTypeSelected = parseInt(args.value);
    }
    function CountWItemChanged(args)
    {
        CountWItemSelected = parseInt(args.value);
    }

    function SumWItemChanged(args)
    {
        console.log(args);
        SumWItemSelected = parseInt(args.value);
    }

    function SaveBurndownChartSettings()
    {
        var dto = {
            ""Title"" : $(""#ChartTitle"").val(),
            ""ProjectId"": ");
#nullable restore
#line 264 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                    Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\n            \"Dashboard\" : ");
#nullable restore
#line 265 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                     Write(Dashboard);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\n            \"ViewComponentId\": ");
#nullable restore
#line 266 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/BurndownChartSettings/Default.cshtml"
                          Write(ViewComponentId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@",
            ""BacklogBindingType"" : BacklogBindingType,
            ""BacklogSelectedType"" : BacklogSelectedType,
            ""WorkItemTypeSelected"" : WorkItemTypeSelected,
            ""CountWItemSelected"" : CountWItemSelected,
            ""SumWItemSelected"" : SumWItemSelected,
            ""BurndownOnSelect"" : BurndownOnSelect
        }
        console.log(dto);
        $.ajax({
            type: 'POST',
            url: '/Widget/AppendBurndownChart',
            data: JSON.stringify(dto),
            contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            success: function (response) {
                console.log(response);
                AddNewDashboardPanel(response.associatedUserDashboardPremade[0].id, response.id);
            },
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
    }
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
