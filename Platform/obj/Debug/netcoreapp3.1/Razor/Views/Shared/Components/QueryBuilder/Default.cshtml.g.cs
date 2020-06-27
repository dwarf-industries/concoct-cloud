#pragma checksum "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/QueryBuilder/Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "dd2f03182207b26b960897e9e69836bd9af5c88a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_QueryBuilder_Default), @"mvc.1.0.view", @"/Views/Shared/Components/QueryBuilder/Default.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"dd2f03182207b26b960897e9e69836bd9af5c88a", @"/Views/Shared/Components/QueryBuilder/Default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a3d88b77ca5ff650022e07aede809867a9af767", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_QueryBuilder_Default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 2 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/QueryBuilder/Default.cshtml"
  
    var ProjectId = ViewData["ProjectId"] as int?;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    .e-query-builder .e-control-wrapper.e-slider-container.e-horizontal {
        height: 0;
    }

    #ruleContent {
        border: 1px solid #d3d3d3;
        width: 100%;
        height: 500px;
        overflow: auto;
    }
	
	.highcontrast textarea#ruleContent {
	  background-color: #000;
	}
    #querybuilder{
        overflow-x: hidden;
    }
    #querybuilder .e-group-header{
        padding-left: 14%;
    }
</style>

<div class=""row"" style=""padding: 0; margin: 0;"">
    <div class=""control-section"" id=""QueryHolder"">
        <div id=""querybuilder"" class=""row"" style=""width:100%;height: 67vh;overflow-x: hidden;"">
        </div>
    </div>
</div>
<div class=""row"" id=""ColumnHolder"" style=""padding: 0; margin-left: 9%; margin-right:9%;""> 
    <select id=""TableColumns"" style=""padding: 5%;""> 
         
    </select>
</div>
<div class=""row"">
    <button  style=""width: auto; margin-left: 30%"" onclick=""AddComponent()"" class=""btn btn-primary e-control e-btn e-lib e-outline e-primary"">
        Add Component");
            WriteLiteral(@"
    </button>
</div>
<script>
    $(""#ColumnHolder"").hide();
    var elem;
    var dropDownObj;
    var boxObj;
    var ticksSlider;
    var element;
    var qryBldrObj;
    var Current; 
    var TableName;
    var LastRule;
    var RowEnabled;
   
    function GetFilterData(current)
    {
        console.log(current);
        InitQueryColumn(current);
        InitQueryFilter(current);
");
            WriteLiteral(@"      
    }

    function InitQueryColumn(data)
    {
        $(""#ColumnHolder"").html("""");

        var result = ""<select id=\""TableColumns\"" style=\""padding: 5%;\"">"";
            
        data.forEach(x=>{
            console.log(x.label);
            result += ""<option value=""+x.label+"">""+x.label+""</option>"";
        });
        result += ""</select>"";
        $(""#ColumnHolder"").html(result);
        var bindingColumnSelect = new ej.dropdowns.ComboBox({
            popupHeight: '230px',
            index: 0,
            change : DropBindingdownChanged,
            placeholder: 'Select a binding column',
        });
        bindingColumnSelect.appendTo('#TableColumns');
        $(""#ColumnHolder"").show();
    }

    function InitQueryFilter(data)
    {

    $(""#QueryHolder"").html(""<div id=\""querybuilder\"" class=\""row\"" style=\"" width:100%;\""></div>"");
    var columnData = [
            { field: 'EmployeeID', label: 'Employee ID', type: 'number' },
            { field: 'FirstName', label: 'First Name', type: '");
            WriteLiteral(@"string' },
            { field: 'TitleOfCourtesy', label: 'Title Of Courtesy', type: 'boolean', values: ['Mr.', 'Mrs.'] },
            { field: 'Title', label: 'Title', type: 'string' },
            { field: 'HireDate', label: 'Hire Date', type: 'date', format: 'dd/MM/yyyy' },
            { field: 'Country', label: 'Country', type: 'string' },
            { field: 'City', label: 'City', type: 'string' }
        ];
       
        var qryBldrObj = new ej.querybuilder.QueryBuilder({
            width: '100%',
            height: '67vh',
            columns: data,
            ruleChange: RuleChanged,
            created: createdControl
        });
        qryBldrObj.appendTo('#querybuilder');
        document.getElementById(""#querybuilder"").style.property = ""overflow-x:hidden;"";

    }
    function RuleChanged(args)
    {
        LastRule = args.rule;
    }

    function createdControl() {
        if (ej.base.Browser.isDevice) {
            qryBldrObj.summaryView = true;
        }
    }

    function AddComponen");
            WriteLiteral(@"t()
    {
        var currentDataSet;
        var last = Components[Components.length - 1];

        var local = {
            ""ControlName"":Current,
            ""TableName"" : TableName,
            ""rule"":LastRule,
            ""NewRow"":RowEnabled,
            ""Column"":$(""#RowWidth"").val(),
            ""BindingRow"":$(""#RowHeight"").val(),
            ""Index"" : Components.length
        };
        
        $.ajax({
            type: 'POST',
            url: '/Widget/GetQueryData',
            data: JSON.stringify(local),
            contentType: ""application/json; charset=utf-8"",
            dataType: ""json"",
            success: function (response) {
 
            },  
            error: function (xhr, status, error) {
                console.log(error);
            }
        });
      
    }   
    function DropBindingdownChanged(args)
    {
        console.log(args);
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