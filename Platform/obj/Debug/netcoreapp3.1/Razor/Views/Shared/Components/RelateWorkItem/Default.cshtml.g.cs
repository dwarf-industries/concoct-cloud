#pragma checksum "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/RelateWorkItem/Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "31de609bfc749fd334fefb8ade458db5263a5f88"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_RelateWorkItem_Default), @"mvc.1.0.view", @"/Views/Shared/Components/RelateWorkItem/Default.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"31de609bfc749fd334fefb8ade458db5263a5f88", @"/Views/Shared/Components/RelateWorkItem/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a3d88b77ca5ff650022e07aede809867a9af767", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_RelateWorkItem_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/RelateWorkItem/Default.cshtml"
   
    var projectId = ViewData["ProjectId"] as int?;
    var WorkItemData = ViewData["WorkItemId"] as int?;
 

#line default
#line hidden
#nullable disable
            WriteLiteral(@" <div class=""modal bd-example-modal-lg"" id=""WorItemsGridPanel"" tabindex=""-1"" role=""dialog"" aria-labelledby=""myLargeModalLabel"" aria-hidden=""true"">
    <div class=""modal-dialog modal-lg"">
        <div class=""modal-content"">
            <div id=""ModalAdd"">
                <div class=""ModalInnerFix"">
                    <div class=""row"">
                        <p>Relation Type</p>
                    </div>
                    <div class=""row"">
                                        
                        <select class=""form-control-sm form-control""  id=""WorkItemRelationsGrid"">
                        
                        </select>
                    
                    </div>
                    <div class=""row"">
                        
                        <div class=""e-input-group e-float-icon-left"">
                            <span class=""e-input-group-icon e-input-picture"" onclick=""CallItemSelect();""></span>
                            <div class=""e-input-in-wrap"">
                           ");
            WriteLiteral(@" <input class=""e-input"" id=""workItemInputObj"" type=""text"" placeholder=""Upload Picture"" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id=""ModalSelect"" hidden>
                <div class=""ModalInnerFix"">
                    <h3> Select work item</h3>
                    <div class=""row"">
                        <div class=""control-section"">
                            <div class=""content-wrapper"">
                                <div id=""Grid""></div>
                            </div>
                        </div>
                    </div>
                    <div class=""row"">
                        <button   id=""AcceptWItemChange"">Add</button>
                        <button   id=""CancelAddWItemBtn"" onclick=""CancelGridGlicked()"">Cancel</button>

                    </div>
                </div>
            </div>
        </div>

    </div>
</div>


      <!-- end of main-content -->
<script src=""http");
            WriteLiteral(@"s://code.jquery.com/jquery-3.4.1.min.js"" ></script>
<script src=""https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"" integrity=""sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo"" crossorigin=""anonymous""></script>
<script src=""https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"" integrity=""sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6"" crossorigin=""anonymous""></script>

<script src=""/js/RelateWorkItems.js"" ></script>

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