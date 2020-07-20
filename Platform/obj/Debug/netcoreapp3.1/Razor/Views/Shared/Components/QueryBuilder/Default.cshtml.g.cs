#pragma checksum "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/QueryBuilder/Default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f5ff4abaab9042335574a4c708e344155ea2a4e7"
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f5ff4abaab9042335574a4c708e344155ea2a4e7", @"/Views/Shared/Components/QueryBuilder/Default.cshtml")]
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
");
            WriteLiteral(@"<div class=""row"">
    <button  style=""width: auto; margin-left: 30%"" onclick=""AddComponent()"" class=""btn btn-primary e-control e-btn e-lib e-outline e-primary"">
        Add Component
    </button>
</div>
<script>
    $(""#ColumnHolder"").hide();
    var elem;
    var dropDownObj;
    var dropDownObjWType;
    var dropDownObjWArea;
    var dropDownObjWSeverity;
    var boxObj;
    var ticksSlider;
    var element;
    var qryBldrObj;
    var Current; 
    var TableName;
    var LastRule;
    var RowEnabled;
    var filter = [
    {
        field: 'Id',
        label: 'Id',
        type: 'number'
    },
    {
        field: 'AssignedAccount',
        label: 'Account',
        type: 'number',
        operators: [
            { key: 'Equal', value: 'equal' },
            { key: 'Not Equal', value: 'notequal' }
        ],
        template: {
            create: function () {
                elem = document.createElement('input');
                elem.setAttribute('type', 'text');
                return elem;
       ");
            WriteLiteral(@"     },
            destroy: function (args) {
                var dropdown = ej.base.getComponent(document.getElementById(args.elementId), 'dropdownlist');
                if (dropdown) {
                    dropdown.destroy();
                }
            },
            write: function (args) {
                $.ajax({
                    type: 'GET',
                    url: '/Accounts/GetProjectUsers?projectId=");
#nullable restore
#line 90 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/QueryBuilder/Default.cshtml"
                                                         Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function (ds) {
                        console.log(ds);
                        dropDownObj = new ej.dropdowns.DropDownList({
                            dataSource: ds,
                            value: args.values ? args.values.accountId : ds[0].accountId,
                            fields: { text: 'name' },
                            change: function (e) {
                                console.log(e);
                                qryBldrObj.notifyChange(e.itemData.accountId, e.element);
                            }
                        });
                        dropDownObj.appendTo('#' + args.elements.id);

                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
            
                

               
            }
        }
    },
    {
 ");
            WriteLiteral(@"       field: 'WorkItemTypeId',
        label: 'Work Item Type',
        type: 'number',
        operators: [
            { key: 'Equal', value: 'equal' },
            { key: 'Not Equal', value: 'notequal' }
        ],
        template: {
            create: function () {
                elem = document.createElement('input');
                elem.setAttribute('type', 'text');
                return elem;
            },
            destroy: function (args) {
                var dropdown = ej.base.getComponent(document.getElementById(args.elementId), 'dropdownlist');
                if (dropdown) {
                    dropdown.destroy();
                }
            },
            write: function (args) {
                $.ajax({
                    type: 'GET',
                    url: '/Boards/GetWorkItemTypes',
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function (ds) {
                        console.log(ds);
      ");
            WriteLiteral(@"                  dropDownObjWType = new ej.dropdowns.DropDownList({
                            dataSource: ds,
                            value: args.values ? args.values.id : ds[0].id,
                            fields: { text: 'typeName' },
                            change: function (e) {
                                console.log(e);
                                qryBldrObj.notifyChange(e.itemData.id, e.element);
                            }
                        });
                        dropDownObjWType.appendTo('#' + args.elements.id);

                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
            
                

               
            }
        }
    },
    {
        field: 'StateId',
        label: 'Work Item State',
        type: 'number',
        operators: [
            { key: 'Equal', value: 'equal' },
            { key: 'Not Equal', value: 'notequal' }
      ");
            WriteLiteral(@"  ],
        template: {
            create: function () {
                elem = document.createElement('input');
                elem.setAttribute('type', 'text');
                return elem;
            },
            destroy: function (args) {
                var dropdown = ej.base.getComponent(document.getElementById(args.elementId), 'dropdownlist');
                if (dropdown) {
                    dropdown.destroy();
                }
            },
            write: function (args) {
                $.ajax({
                    type: 'GET',
                    url: '/Boards/GetWorkItemStates',
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function (ds) {
                        console.log(ds);
                        dropDownObjWType = new ej.dropdowns.DropDownList({
                            dataSource: ds,
                            value: args.values ? args.values.id : ds[0].id,
                        ");
            WriteLiteral(@"    fields: { text: 'stateName' },
                            change: function (e) {
                                console.log(e);
                                qryBldrObj.notifyChange(e.itemData.id, e.element);
                            }
                        });
                        dropDownObjWType.appendTo('#' + args.elements.id);

                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
            
                

               
            }
        }
    },
    {
        field: 'Area',
        label: 'Work Item Area',
        type: 'number',
        operators: [
            { key: 'Equal', value: 'equal' },
            { key: 'Not Equal', value: 'notequal' }
        ],
        template: {
            create: function () {
                elem = document.createElement('input');
                elem.setAttribute('type', 'text');
                return elem;
            },
      ");
            WriteLiteral(@"      destroy: function (args) {
                var dropdown = ej.base.getComponent(document.getElementById(args.elementId), 'dropdownlist');
                if (dropdown) {
                    dropdown.destroy();
                }
            },
            write: function (args) {
                $.ajax({
                    type: 'GET',
                    url: '/Dashboard/GetWorkItemValueAreas',
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function (ds) {
                        console.log(ds);
                        dropDownObjWArea = new ej.dropdowns.DropDownList({
                            dataSource: ds,
                            value: args.values ? args.values.id : ds[0].id,
                            fields: { text: 'areaName' },
                            change: function (e) {
                                console.log(e);
                                qryBldrObj.notifyChange(e.itemData.id, e.ele");
            WriteLiteral(@"ment);
                            }
                        });
                        dropDownObjWArea.appendTo('#' + args.elements.id);

                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
            
                

               
            }
        }
    },
    {
        field: 'Severity',
        label: 'Work Item Severity',
        type: 'number',
        operators: [
            { key: 'Equal', value: 'equal' },
            { key: 'Not Equal', value: 'notequal' }
        ],
        template: {
            create: function () {
                elem = document.createElement('input');
                elem.setAttribute('type', 'text');
                return elem;
            },
            destroy: function (args) {
                var dropdown = ej.base.getComponent(document.getElementById(args.elementId), 'dropdownlist');
                if (dropdown) {
                    dropdow");
            WriteLiteral(@"n.destroy();
                }
            },
            write: function (args) {
                $.ajax({
                    type: 'GET',
                    url: '/Dashboard/GetWorkItemSeverities',
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function (ds) {
                        console.log(ds);
                        dropDownObjWSeverity = new ej.dropdowns.DropDownList({
                            dataSource: ds,
                            value: args.values ? args.values.id : ds[0].id,
                            fields: { text: 'severityName' },
                            change: function (e) {
                                console.log(e);
                                qryBldrObj.notifyChange(e.itemData.id, e.element);
                            }
                        });
                        dropDownObjWSeverity.appendTo('#' + args.elements.id);

                    },
                    error:");
            WriteLiteral(@" function (xhr, status, error) {
                        console.log(error);
                    }
                });
            
                

               
            }
        }
    },
    {
        field: 'ReasonId',
        label: 'Work Item Reasons',
        type: 'number',
        operators: [
            { key: 'Equal', value: 'equal' },
            { key: 'Not Equal', value: 'notequal' }
        ],
        template: {
            create: function () {
                elem = document.createElement('input');
                elem.setAttribute('type', 'text');
                return elem;
            },
            destroy: function (args) {
                var dropdown = ej.base.getComponent(document.getElementById(args.elementId), 'dropdownlist');
                if (dropdown) {
                    dropdown.destroy();
                }
            },
            write: function (args) {
                $.ajax({
                    type: 'GET',
                    url: '/Dashboard/GetWorkItemRe");
            WriteLiteral(@"asons',
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function (ds) {
                        console.log(ds);
                        dropDownObjWSeverity = new ej.dropdowns.DropDownList({
                            dataSource: ds,
                            value: args.values ? args.values.id : ds[0].id,
                            fields: { text: 'reasonName' },
                            change: function (e) {
                                console.log(e);
                                qryBldrObj.notifyChange(e.itemData.id, e.element);
                            }
                        });
                        dropDownObjWSeverity.appendTo('#' + args.elements.id);

                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });
            
                

               
            }
        }
    },");
            WriteLiteral(@"
    {
        field: 'Iteration',
        label: 'Iteration',
        type: 'number',
        operators: [
            { key: 'Equal', value: 'equal' },
            { key: 'Not Equal', value: 'notequal' }
        ],
        template: {
            create: function () {
                elem = document.createElement('input');
                elem.setAttribute('type', 'text');
                return elem;
            },
            destroy: function (args) {
                var dropdown = ej.base.getComponent(document.getElementById(args.elementId), 'dropdownlist');
                if (dropdown) {
                    dropdown.destroy();
                }
            },
            write: function (args) {
                $.ajax({
                    type: 'GET',
                    url:  '/Boards/GetProjectIterations?projectId=");
#nullable restore
#line 396 "/home/dwarfdevelopment/Projects/GitClones/RC/RokonoControl/Platform/Views/Shared/Components/QueryBuilder/Default.cshtml"
                                                             Write(ProjectId);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"',
                    contentType: ""application/json; charset=utf-8"",
                    dataType: ""json"",
                    success: function (ds) {
                        console.log(ds);
                        dropDownObjWSeverity = new ej.dropdowns.DropDownList({
                            dataSource: ds,
                            value: args.values ? args.values.id : ds[0].id,
                            fields: { text: 'iterationName' },
                            change: function (e) {
                                console.log(e);
                                qryBldrObj.notifyChange(e.itemData.id, e.element);
                            }
                        });
                        dropDownObjWSeverity.appendTo('#' + args.elements.id);

                    },
                    error: function (xhr, status, error) {
                        console.log(error);
                    }
                });

            }
        }
    },
    {
        field: 'TransactionType',
       ");
            WriteLiteral(@" label: 'TransactionType',
        type: 'string',
        operators: [{
            key: 'Equal',
            value: 'equal'
        }, {
            key: 'Not Equal',
            value: 'notequal'
        }],
        template: {
            create: function () {
                elem = document.createElement('input');
                elem.setAttribute('type', 'checkbox');
                return elem;
            },
            destroy: function (args) {
                ej.base.getComponent(document.getElementById(args.elementId), 'checkbox').destroy();
            },
            write: function (args) {
                var checked = args.values === 'IsExpensive' ? true : false;
                boxObj = new ej.buttons.CheckBox({
                    label: 'Is Expensive',
                    checked: checked,
                    change: function (e) {
                        qryBldrObj.notifyChange(e.checked ? 'expensive' : 'income', e.event.target);
                    }
                });
                bo");
            WriteLiteral(@"xObj.appendTo('#' + args.elements.id);
            }
        }
    },
    {
        field: 'Description',
        label: 'Description',
        type: 'string'
    },
    {
        field: 'OriginEstimate',
        label: 'Original Estimate',
        type: 'number'
    },
    {
        field: 'Remaining',
        label: 'Remaining',
        type: 'number'
    },
    {
        field: 'Compleated',
        label: 'Compleated',
        type: 'number'
    },
    {
        field: 'TimeCapacity',
        label: 'TimeCapacity',
        type: 'number'
    },
    {
        field: 'Effort',
        label: 'Effort',
        type: 'number'
    },
    {
        field: 'StoryPoints',
        label: 'Story Points',
        type: 'number'
    },
    {
        field: 'StackRank',
        label: 'Stack Rank',
        type: 'number'
    },
    {
        field: 'Id',
        label: 'Id',
        type: 'number'
    },
    {
        field: 'RiskId',
        label: 'Risk',
        type: 'number'
    },
    {
        field: 'Reason',
");
            WriteLiteral(@"        label: 'Reason',
        type: 'string'
    },
    {
        field: 'ResolvedReason',
        label: 'Resolved Reason',
        type: 'string'
    },
    {
        field: 'AcceptanceCriteria',
        label: 'Acceptance Criteria',
        type: 'string'
    },
    {
        field: 'SystemInfo',
        label: 'System Info',
        type: 'string'
    },
    {
        field: 'StartDate',
        label: 'Start Date',
        type: 'date'
    },
    {
        field: 'DueDate',
        label: 'Due Date',
        type: 'date'
    },
    {
        field: 'EndDate',
        label: 'End Date',
        type: 'date'
    },
    {
        field: 'Amount',
        label: 'Amount',
        type: 'number',
        operators: [
            { key: 'Equal', value: 'equal' },
            { key: 'Greater than', value: 'greaterthan' },
            { key: 'Less than', value: 'lessthan' },
            { key: 'Less than or equal', value: 'lessthanorequal' },
            { key: 'Greater than or equal', value: 'greaterthanoreq");
            WriteLiteral(@"ual' },
            { key: 'Not equal', value: 'notequal' }
        ],
        template: {
            create: function () {
                elem = document.createElement('div');
                elem.setAttribute('class', 'ticks_slider');
                return elem;
            },
            destroy: function (args) {
                ej.base.getComponent(document.getElementById(args.elementId), 'slider').destroy();
            },
            write: function (args) {
                ticksSlider = new ej.inputs.Slider({
                    value: args.values,
                    min: 0,
                    max: 100,
                    tooltip: { isVisible: true, placement: 'Before', showOn: 'Hover' },
                    type: 'MinRange',
                    change: function (e) {
                        qryBldrObj.notifyChange(e.value, args.elements);
                    }
                });
                ticksSlider.appendTo('#' + args.elements.id);
            }
        }
    }
    ];
    InitQueryFilt");
            WriteLiteral("er(filter);\n");
            WriteLiteral("\n");
            WriteLiteral(@" 
    function InitQueryFilter(data)
    {
        $(""#QueryHolder"").html(""<div id=\""querybuilder\"" class=\""row\"" style=\"" width:100%;\""></div>"");
        var columnData = [
                { field: 'EmployeeID', label: 'Employee ID', type: 'number' },
                { field: 'FirstName', label: 'First Name', type: 'string' },
                { field: 'TitleOfCourtesy', label: 'Title Of Courtesy', type: 'boolean', values: ['Mr.', 'Mrs.'] },
                { field: 'Title', label: 'Title', type: 'string' },
                { field: 'HireDate', label: 'Hire Date', type: 'date', format: 'dd/MM/yyyy' },
                { field: 'Country', label: 'Country', type: 'string' },
                { field: 'City', label: 'City', type: 'string' }
        ];

        qryBldrObj = new ej.querybuilder.QueryBuilder({
            width: '100%',
            height: '67vh',
            columns: data,
            ruleChange: RuleChanged,
            created: createdControl
        });
        qryBldrObj.appendTo('#querybuilder'");
            WriteLiteral(@");
        document.getElementById(""#querybuilder"").style.property = ""overflow-x:hidden;"";

    }
    function RuleChanged(args)
    {
        console.log(args);
        LastRule = args.rule;
    }

    function createdControl() {
        if (ej.base.Browser.isDevice) {
            qryBldrObj.summaryView = true;
        }
    }

    function AddComponent()
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
 
         ");
            WriteLiteral("   },  \n            error: function (xhr, status, error) {\n                console.log(error);\n            }\n        });\n      \n    }   \n \n</script>\n");
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
