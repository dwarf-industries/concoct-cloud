#pragma checksum "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Widgets/WidgetBuilder/Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ee7797cfbc8c3c6238209a17783c751382b2e50d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Widgets_WidgetBuilder_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Widgets/WidgetBuilder/Default.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ee7797cfbc8c3c6238209a17783c751382b2e50d", @"/Views/Shared/Components/Widgets/WidgetBuilder/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a3d88b77ca5ff650022e07aede809867a9af767", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Widgets_WidgetBuilder_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""row"">
    <div class=""col-md-2"">
        <div class=""control-section"">
            <div class=""content-wrapper"">
                <div id=""ControlListGrid"" style=""width: auto;height: 87vh;overflow-y: scroll;"">
                </div>
            </div>
        </div>
    </div>
    <div class=""col-md-8"">

    </div>
    <div class=""col-md-2"">

    </div>
</div>


<script>


   
    $.ajax({
        type: 'GET',
        url: '/Accounts/GetWidgetComponents',
        contentType: ""application/json; charset=utf-8"",
        dataType: ""json"",
        
        success: function (response) {
            console.log(response);
            InitializeControlList(response);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
    function InitializeControlList(data)
    {
        var ControlListGrid = new ej.grids.Grid({
            dataSource: data,
            rowSelected : ControlSelected,
            columns: [
                { field: 'controlName', he");
            WriteLiteral(@"aderText: 'Control Name', width: 120, textAlign: 'Right' },
               
            ]
        });
        ControlListGrid.appendTo('#ControlListGrid');
    }

    function ControlSelected(args)
    {
        console.log(args);
        AddNewDashboardPanel(""test"");

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