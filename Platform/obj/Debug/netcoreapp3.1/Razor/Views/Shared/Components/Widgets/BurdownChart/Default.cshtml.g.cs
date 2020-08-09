#pragma checksum "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8ea27f42b8f8961ba5661662dfb53dd1bd3078b0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Widgets_BurdownChart_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8ea27f42b8f8961ba5661662dfb53dd1bd3078b0", @"/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a3d88b77ca5ff650022e07aede809867a9af767", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Widgets_BurdownChart_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml"
  
    var  ProjectId = ViewData["ProjectId"] as int?;
    var BindingData = ViewData["ChartBindingData"] as Platform.Models.IncomingBurndownChartSetting;
    var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
    var random = new System.Random();
    var assignedLetter =chars[random.Next(chars.Length-1)];
    var customId  =  $"AssignedChart_{assignedLetter}";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n\n\n<div class=\"control-section\">\n    <div");
            BeginWriteAttribute("id", " id=\"", 409, "\"", 423, 1);
#nullable restore
#line 14 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml"
WriteAttributeValue("", 414, customId, 414, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" align=\"center\"></div>\n</div>\n<style>\n    #control-container {\n        padding: 0px !important;\n    }\n</style>\n<script>\n\nvar dto = {\n    \"Title\" :  \'");
#nullable restore
#line 24 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml"
           Write(BindingData.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\n    \"ProjectId\": ");
#nullable restore
#line 25 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml"
            Write(BindingData.ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\n    \"Dashboard\" : ");
#nullable restore
#line 26 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml"
             Write(BindingData.Dashboard);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\n    \"ViewComponentId\": ");
#nullable restore
#line 27 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml"
                  Write(BindingData.ViewComponentId);

#line default
#line hidden
#nullable disable
            WriteLiteral(",\n    \"BacklogBindingType\" : \'");
#nullable restore
#line 28 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml"
                       Write(BindingData.BacklogBindingType);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\n    \"BacklogSelectedType\" : \'");
#nullable restore
#line 29 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml"
                        Write(BindingData.BacklogSelectedType);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\n    \"WorkItemTypeSelected\" : \'");
#nullable restore
#line 30 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml"
                         Write(BindingData.WorkItemTypeSelected);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\n    \"CountWItemSelected\" : \'");
#nullable restore
#line 31 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml"
                       Write(BindingData.CountWItemSelected);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\n    \"SumWItemSelected\" : \'");
#nullable restore
#line 32 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml"
                     Write(BindingData.SumWItemSelected);

#line default
#line hidden
#nullable disable
            WriteLiteral("\',\n    \"BurndownOnSelect\" : \'");
#nullable restore
#line 33 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml"
                     Write(BindingData.BurndownOnSelect);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'
}
console.log(dto);
$.ajax({
    type: 'POST',
    url: '/Widget/GetBurndownChartData',
    data: JSON.stringify(dto),
    contentType: ""application/json; charset=utf-8"",
    dataType: ""json"",
    success: function (response) {
        console.log(response);
        InitilizeBurdownChart(response);
    },
    error: function (xhr, status, error) {
        console.log(error);
    }
}); 
");
            WriteLiteral(@"function InitilizeBurdownChart(data)
{

     var customId = new ej.charts.Chart({
        //Initializing Primary X Axis
        primaryXAxis: {
            title: 'by dates',
            interval: 1,
            labelIntersectAction: 'Rotate45',
            valueType: 'Category',
            majorGridLines: { width: 0 }, minorGridLines: { width: 0 },
            majorTickLines: { width: 0 }, minorTickLines: { width: 0 },
            lineStyle: { width: 0 },
        },
        //Initializing Primary Y Axis
        primaryYAxis: {
            title: 'burndown',
            minimum: -3,
            maximum: 3,
            interval: 1,
            lineStyle: { width: 0 },
            majorTickLines: { width: 0 }, majorGridLines: { width: 1 },
            minorGridLines: { width: 1 }, minorTickLines: { width: 0 },
            labelFormat: '{value}B',
        },
        chartArea: {
            border: {
                width: 0
            }
        },
        //Initializing Chart Series
        series: [
        ");
            WriteLiteral(@"    {
                type: 'StackingColumn',
                dataSource: data,
                xName: 'x', yName: 'y', name: 'Private Consumption',
            },{
                type: 'Line',
                dataSource: data,
                xName: 'x', yName: 'y', name: 'Burndown rate',
                width: 2,
                marker: {
                    visible: true,
                    width: 10,
                    height: 10
                },
            }
        ],
        width: ej.base.Browser.isDevice ? '100%' : '60%',
        //Initializing Chart Title
        title: ' ',
        //Initializing Tooltip
        tooltip: {
            enable: true
        },

    });
    customId.appendTo('#");
#nullable restore
#line 108 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/Widgets/BurdownChart/Default.cshtml"
                   Write(customId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\n\n}\n</script>\n");
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
