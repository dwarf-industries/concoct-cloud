#pragma checksum "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Dashboard/Project.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8feabe913be5727661fb125d104f3d4673dbb904"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Dashboard_Project), @"mvc.1.0.view", @"/Views/Dashboard/Project.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8feabe913be5727661fb125d104f3d4673dbb904", @"/Views/Dashboard/Project.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a3d88b77ca5ff650022e07aede809867a9af767", @"/Views/_ViewImports.cshtml")]
    public class Views_Dashboard_Project : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Dashboard/Project.cshtml"
  
    ViewData["Title"] = "Home Page";
    Layout = "~/Views/Shared/_LayoutProjects.cshtml";
    var result = ViewData["Projects"] as List<Projects>;
    var userName =  ViewData["Name"] as string;

#line default
#line hidden
#nullable disable
            WriteLiteral("<!-- end of sidebar element -->\n \n<div class=\"row\">\n    <div class=\"col-md-12\">\n        <div class=\"main-card mb-3 card\">\n            <div class=\"card-body text-center\">\n\n                    <h1> Welcome back ");
#nullable restore
#line 15 "/home/dwarfdevelopment/Projects/GitClones/RokonoControl/Platform/Views/Dashboard/Project.cshtml"
                                 Write(userName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" </h1>
                
            </div>
        </div>
    </div>
</div>
<div class=""row"">
<div class=""col-md-12"">
  <div class=""main-card mb-3 card"">
        <div class=""card-body""><h5 class=""card-title text-center"">Select a project</h5>
          <div class=""row"">
              <div class=""col-md-1"">
                  {Initials}
              </div>
              <div class=""col-md-3"">
                  <h1>{Project Name}</h1>
              </div>
              <div class=""col-md-8"">
                <a><i class=""fas fa-cogs""></i> Settings</a>
                <a><i class=""far fa-user""></i> Add User</a>

              </div>
          </div>
        </div>
    </div>
</div>
</div>
         
<script src=""https://code.jquery.com/jquery-3.4.1.slim.min.js"" integrity=""sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n"" crossorigin=""anonymous""></script>
<script src=""https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"" integrity=""sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3z");
            WriteLiteral(@"V9zzTtmI3UksdQRVvoxMfooAo"" crossorigin=""anonymous""></script>
<script src=""https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"" integrity=""sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6"" crossorigin=""anonymous""></script>");
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
