#pragma checksum "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/SearchBox/Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "884373fefb6d5619313530de85c259fde303f171"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_SearchBox_Default), @"mvc.1.0.view", @"/Views/Shared/Components/SearchBox/Default.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"884373fefb6d5619313530de85c259fde303f171", @"/Views/Shared/Components/SearchBox/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a3d88b77ca5ff650022e07aede809867a9af767", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_SearchBox_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/SearchBox/Default.cshtml"
  
    var ProjectId = ViewData["ProjectId"] as int?;
    var Iteration  = ViewData["CurrentIteration"] as int?;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""row"" style=""margin:0;"">
    <input type=""text"" id=""SearchBox"" style=""width:100%; height:30px;"" >
</div>

<script>

    var SearchBox = new ej.inputs.TextBox({
        placeholder: 'Search work item by title',
        floatLabelType: 'Auto'
    });
    SearchBox.appendTo('#SearchBox');




$(""#SearchBox"").on('keyup', function (e) {
       if (e.keyCode === 13) {
        window.location.href =""/Backlogs/Index?projectId=");
#nullable restore
#line 23 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/SearchBox/Default.cshtml"
                                                    Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral("&&boardId=0&&iteration=");
#nullable restore
#line 23 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Shared/Components/SearchBox/Default.cshtml"
                                                                                     Write(Iteration);

#line default
#line hidden
#nullable disable
            WriteLiteral("&&phase=\"+$(\"#SearchBox\").val();\n    }\n});\n\n</script>");
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
