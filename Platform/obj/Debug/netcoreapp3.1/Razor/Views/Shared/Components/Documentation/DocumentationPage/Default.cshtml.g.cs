#pragma checksum "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b5323e209d1aab22cb865cd634c8e62d77e6b990"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Documentation_DocumentationPage_Default), @"mvc.1.0.view", @"/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b5323e209d1aab22cb865cd634c8e62d77e6b990", @"/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a3d88b77ca5ff650022e07aede809867a9af767", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Documentation_DocumentationPage_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
  
    var PagesData = ViewData["PageData"] as  List<AssociatedDocumentationCategoryPage>;
    var Category = ViewData["CategoryId"] as int?;
    var ProjectId = ViewData["ProjectId"] as int?;
    var UserRights = ViewData["UserRights"] as UserRights;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<style>
    #DocumentationPageControl img{
        width: 100%;
    }
    #DocumentationPageControl .row{
        padding: 20px;
    }
    .DocumentationMaxHeight{
        max-height: 83vh;
        overflow-y: auto;
    }

    .DocumentationMaxHeight html {
    scroll-behavior: smooth;
    }
    .SideLinkHolder{
        padding-left: 20px;
        padding-right: 20px;
        padding-top: 3px;
        padding-bottom: 3px;

        border-bottom: #e3165b 2px solid;
    }
    .SideLink{

        font-size: 18px;
        color: #797979;
    }
    .SideLink:hover{
        cursor: pointer;
        color:#e3165b;
    }
</style>
<div class=""row"">
    <div class=""col-md-9 DocumentationMaxHeight"">
");
#nullable restore
#line 42 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
         if(PagesData != null)
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 44 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
             foreach (var currentPage in PagesData)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"DocumentationSection\"");
            BeginWriteAttribute("id", " id=\"", 1108, "\"", 1136, 2);
            WriteAttributeValue("", 1113, "Current_", 1113, 8, true);
#nullable restore
#line 46 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
WriteAttributeValue("", 1121, currentPage.Id, 1121, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" >\n");
#nullable restore
#line 47 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
                     if(UserRights.Documentation == 1)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div class=\"row\" style=\"display: block ruby;\">\n                            <hr/>\n                                <button class=\"btn btn-primary e-control e-btn e-lib e-outline e-primary\" style=\"width: 50%;\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1447, "\"", 1491, 3);
            WriteAttributeValue("", 1457, "OpenEditPageModal(", 1457, 18, true);
#nullable restore
#line 51 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
WriteAttributeValue("", 1475, currentPage.Id, 1475, 15, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1490, ")", 1490, 1, true);
            EndWriteAttribute();
            WriteLiteral(">Edit page</button>\n                                <button class=\"btn btn-primary e-control e-btn e-lib e-outline e-primary\" style=\"width: 50%;\"");
            BeginWriteAttribute("onclick", " onclick=\"", 1637, "\"", 1674, 3);
            WriteAttributeValue("", 1647, "DeletePage(", 1647, 11, true);
#nullable restore
#line 52 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
WriteAttributeValue("", 1658, currentPage.Id, 1658, 15, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1673, ")", 1673, 1, true);
            EndWriteAttribute();
            WriteLiteral(">Delete page</button>\n                            <hr/>\n                        </div>\n");
#nullable restore
#line 55 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div class=\"DocumentationBody\">\n                        ");
#nullable restore
#line 57 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
                   Write(Html.Raw(currentPage.Content));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                    </div>\n                </div>\n");
#nullable restore
#line 60 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 60 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
                     
        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 62 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
         if(UserRights.Documentation == 1)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""row"">
                <hr/>
                    <button class=""btn btn-primary e-control e-btn e-lib e-outline e-primary"" style=""width: 100%;"" onclick=""OpenNewPageModal(0)"">Add new page</button>
                <hr/>
            </div>
");
#nullable restore
#line 69 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\n    <div class=\"col-md-3 DocumentationMaxHeight\">\n");
#nullable restore
#line 72 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
         if(PagesData != null)
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
             foreach (var quickLink in PagesData)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"row SideLinkHolder\">\n                    <a");
            BeginWriteAttribute("href", " href=\"", 2533, "\"", 2562, 2);
            WriteAttributeValue("", 2540, "#Current_", 2540, 9, true);
#nullable restore
#line 77 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
WriteAttributeValue("", 2549, quickLink.Id, 2549, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("  class=\"SideLink\"> ");
#nullable restore
#line 77 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
                                                                   Write(quickLink.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a>\n                </div>\n");
#nullable restore
#line 79 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 79 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
             
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </div>
</div>
<div id=""NewPageModalHodler"">
 
</div>
<link href=""https://cdn.quilljs.com/1.3.6/quill.snow.css"" rel=""stylesheet"">
<!-- Main Quill library -->
<script src=""//cdn.quilljs.com/1.3.6/quill.js""></script>
<script src=""//cdn.quilljs.com/1.3.6/quill.min.js""></script>

<script>
    function OpenNewPageModal()
    {
        $(""#NewPageModalHodler"").html("""");
        $(""#NewPageModalHodler"").load(""/Documentation/GetPageModal?id=0&&category=");
#nullable restore
#line 95 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
                                                                             Write(Category);

#line default
#line hidden
#nullable disable
            WriteLiteral("&&projectId=");
#nullable restore
#line 95 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
                                                                                                  Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""")
        .ajaxSuccess(x => {
            console.log(""Success"")
        });
        
    }

    function OpenEditPageModal(id)
    {
        console.log(id);
        $(""#NewPageModalHodler"").html("""");
        $(""#NewPageModalHodler"").load(""/Documentation/GetPageModal?id=""+id+""&&category=");
#nullable restore
#line 106 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
                                                                                  Write(Category);

#line default
#line hidden
#nullable disable
            WriteLiteral("&&projectId=");
#nullable restore
#line 106 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
                                                                                                       Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@""")
        .ajaxSuccess(x => {
            console.log(""Success"")
        });
        
    }
    function DeletePage(id)
    {
        $(""#DocumentationPageControl"").html("""");

        var data = {
            ""Id"": id
            
        }
        $.ajax({
            type: 'POST',
            url: '/Documentation/DeletePage',
            data: JSON.stringify(data),
            contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            success: function (response) {
                $(""#DocumentationPageControl"").load('/Documentation/DocumentationPage?id='+");
#nullable restore
#line 127 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
                                                                                      Write(Category);

#line default
#line hidden
#nullable disable
            WriteLiteral("+\'&&projectId=");
#nullable restore
#line 127 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/Documentation/DocumentationPage/Default.cshtml"
                                                                                                             Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\');\n\n            },\n            error: function (xhr, status, error) {\n                console.log(error);\n            }\n        }); \n     \n    }\n  \n</script>");
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
