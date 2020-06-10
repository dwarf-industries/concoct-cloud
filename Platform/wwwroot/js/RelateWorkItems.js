var selectedChildren = [];

 var AssociationHander = {
    "WorkItemId": 0,
    "CurrWorkItemId" : 0,
    "ProjectId" : 0,
    "RelationType" :  0,
    "LinkedItems" : 0
}

var dto = {
    "id": id,
    "Phase": "!"
}
$.ajax({
        type: 'POST',
        url: '/Backlogs/GetWorkItems',
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            InitializeRelateWorkItemsGrid(response);
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
function CallItemSelect()
{
    $("#ModalAdd").hide();
    $("#ModalSelect").show();
    
}

function CancelGridGlicked()
{
    $("#ModalAdd").show();
    $("#ModalSelect").hide();
}


function InitializeRelateWorkItemsGrid(data )
{
    console.log(data);
    ej.grids.Grid.Inject(ej.grids.Filter);
    ej.grids.Grid.Inject(ej.grids.Edit, ej.grids.Toolbar);

   console.log(data);
    var grid = new ej.grids.Grid({
        dataSource: data,
        allowSorting: true,
        height: "660px",
        filterSettings: { type:'Menu' },
        allowFiltering: true,
        recordDoubleClick : WorkItemSelected,
        columns: [
            { field: 'id', headerText: 'Id', width: 125 },
            { field: 'title', headerText: 'Title', width: 125 },
            { field: 'description', headerText: 'Description', width: 180 },
            { field: 'assignedAccountNavigation.Email', headerText: 'AssignedTo', width: 110 },

        ]
    
    });
    
    grid.appendTo('#Grid');
}
function WorkItemSelected(args){
    console.log(args);
 
   AddAssocuatedItem(args.rowData.id);
}


function AddAssocuatedItem(id){
    var project = id;

    
    AssociationHander = {
        "WorkItemId": id,
        "CurrWorkItemId" : wItemId,
        "ProjectId" : project,
        "RelationType" :  $("#WorkItemRelationsGrid :selected").val(),
        "LinkedItems" : selectedChildren
    }
    console.log(dto);
     $.ajax({
        type: 'POST',
        url: '/Dashboard/ValidateSelectedItem',
        data: JSON.stringify(AssociationHander),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
            if(response.valid === true)
            {

                var wItemDTO = {
                    "WorkItemId" : response.workItemId,
                    "RelationShipId": response.relationshipId
                };
                $("#RelationShipWItemType").html(response.last.workItemType.typeName);
                $("#RelationWorkItemId").html(response.last.id);
                $("#RelationWorkItemName").html(response.last.title);
                $("#RelationDescription").html(response.last.description + response.last.repoSteps +response.last.acceptanceCriteria);
                selectedChildren.push(wItemDTO);
                $("#listview-defholder").html("");
                var res = "";
                res += "<div id=\"listview-def\" tabindex=\"1\" >";

                res += "<ul>";
                response.workItem.forEach(element => 
                {
                    if(element.workItemTypeId === 1)
                        res += "<li value=\"/Dashboard/EditWorkItem?projectId="+id+"&&workItem="+element.id+"&returnUrl="+window.location.href+" \"><i class=\"fas fa-bug\" style=\"padding:3px;\" ></i>"+element.title+"</li>";
                    else if(element.workItemTypeId === 5)
                        res += "<li value=\"/Dashboard/EditWorkItem?projectId="+id+"&&workItem="+element.id+"&returnUrl="+window.location.href+" \"><i class=\"fas fa-crown\"  style=\"padding:3px;\"></i>"+element.title+"</li>";
                    else if(element.workItemTypeId === 6)
                        res += "<li value=\"/Dashboard/EditWorkItem?projectId="+id+"&&workItem="+element.id+"&returnUrl="+window.location.href+" \"><i class=\"fas fa-exclamation-triangle\"  style=\"padding:3px;\"></i>"+element.title+"</li>";
                    else if(element.workItemTypeId === 4)
                        res += "<li value=\"/Dashboard/EditWorkItem?projectId="+id+"&&workItem="+element.id+"&returnUrl="+window.location.href+" \"><i class=\"fas fa-vial\"  style=\"padding:3px;\"></i>"+element.title+"</li>";
                    else if(element.workItemTypeId === 7)
                        res += "<li value=\"/Dashboard/EditWorkItem?projectId="+id+"&&workItem="+element.id+"&returnUrl="+window.location.href+" \"><i class=\"fas fa-user-circle\"  style=\"padding:3px;\"></i>"+element.title+"</li>";
                    else if(element.workItemTypeId === 3)
                        res += "<li value=\"/Dashboard/EditWorkItem?projectId="+id+"&&workItem="+element.id+"&returnUrl="+window.location.href+" \"><i class=\"fas fa-tasks\"  style=\"padding:3px;\"></i>"+element.title+"</li>";
                    else if(element.workItemTypeId === 2)
                        res += "<li value=\"/Dashboard/EditWorkItem?projectId="+id+"&&workItem="+element.id+"&returnUrl="+window.location.href+" \"><i class=\"fas fa-cog\"  style=\"padding:3px;\"></i>"+element.title+"</li>";
                  
                });
                res += "</ul>";
                res += "</div>";
                $("#listview-defholder").html(res);

                var listObj = new ej.lists.ListView();

                //Render initialized ListView component
                listObj.appendTo('#listview-def');
                console.log(response);
             }
            else
            {
                alert("Item with ID" + id+ " Is already linked with the current work item please chose another item!");
                for(var i = selectedChildren.length - 1; i >= 0; i--) {
                    if(selectedChildren[i].WorkItemId === id) {
                        selectedChildren.splice(i, 1);
                    }
                }
            }


            $("#ModalAdd").show();
            $("#ModalSelect").hide();
            $("#WorItemsGridPanel").modal('hide');
        },
        error: function (xhr, status, error) {
            console.log(error);
        }
    });
    console.log(selectedChildren);
}


function ChangeWorkItemId(id)
{
    console.log("in "+ id);
    wItemId = id;
    AssociationHander.CurrWorkItemId = id;
}
function  LoadWorkItemsGrid()
{
      $.ajax({
        type: 'GET',
        url: '/Dashboard/GetAllWorkItems?projectId=@projectId',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            console.log(response);
             var grid = new ej.grids.Grid({
                dataSource: response,
                recordDoubleClick: WorkItemGridDoubleClicked,
                columns: [
                    { field: 'id', headerText: 'ID', width: 60, textAlign: 'Right' },
                    { field: 'title', headerText: 'Title', width: 150 },
                    { field: 'itemState', headerText: 'State', width: 130, format: 'yMd', textAlign: 'Right' },
                    { field: 'itemType', width: 120, format: 'Type', textAlign: 'Right' }
                ]
            });
            grid.appendTo('#WorkItemsGrid');
        },
        error: function (xhr, status, error) {
            console.log(xhr);
        }
    });
}

function WorkItemGridDoubleClicked(args)
{

    AddAssocuatedItem(args.rowData.id);
    
}

function OpenModal()
{
    console.log("Open modal");
    $.ajax({
        type: 'GET',
        url: '/Dashboard/GetWorkItemRelations',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            response.forEach(function(element) {
                console.log(element);
                $("#WorkItemRelationsGrid").append("<option value=\""+element.id+"\">"+element.relationName+"</option>");
            });
           $('#WorItemsGridPanel').modal('show'); 
           var element = document.getElementsByClassName("modal-backdrop show");
            $(element).remove();
        },
        error: function (xhr, status, error) {
            console.log(xhr);
        }
    });
//     $('#WorItemsGridPanel').modal('show'); 

}