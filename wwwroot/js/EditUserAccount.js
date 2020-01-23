var CanCommit;
var CanClone;
var CanView;
var CanCreate;
var CanDelete;
var ProjectGrid;
var CProject;
$(document).ready(function() {
    $("#ProjSettings").hide();

    var bindingData = [];

    projects.forEach(function(element) {
        var current = {
            "Name": element.ProjectName,
            "Id": element.Id
        }
        bindingData.push(current);
    });
    console.log(bindingData);

    var ProjectRights = new ejs.buttons.Switch({ change: ProjectRightsChanged, checked: rights });
    ProjectRights.appendTo('#PRights');

    var email = new ej.inputs.TextBox({
        placeholder: 'Email',
        value: cEmail,
        floatLabelType: 'Auto'
    });
    email.appendTo('#Email');

    var password = new ej.inputs.TextBox({
        placeholder: 'Password',
        floatLabelType: 'Auto'
    });
    password.appendTo('#Password');

    var firstName = new ej.inputs.TextBox({
        placeholder: 'First Name',
        value: cFName,
        floatLabelType: 'Auto'
    });
    firstName.appendTo('#FirstName');

    var middleName = new ej.inputs.TextBox({
        placeholder: 'Middle Name',
        floatLabelType: 'Auto'
    });
    middleName.appendTo('#MiddleName');

    var lastName = new ej.inputs.TextBox({
        placeholder: 'Last Name',
        value: cLastName,
        floatLabelType: 'Auto'
    });
    lastName.appendTo('#LastName');

    var grid = new ej.grids.Grid({
        dataSource: bindingData,
        rowSelected: RowSelected,
        actionBegin: DeleteTest,
        editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, allowEditOnDblClick: false },
        columns: [
            { field: 'Name', headerText: 'Name', width: 120, textAlign: 'Right' },
            {
                headerText: 'Manage Records',
                width: 160,
                commands: [
                    { type: 'Delete', buttonOption: { iconCss: 'e-icons e-delete', cssClass: 'e-flat' } },
                ]
            }
        ]
    });
    grid.appendTo('#Grid');
    $.ajax({
        type: 'GET',
        url: '/Dashboard/GetProjects',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            LoadProjects(response);
        },
        error: function(xhr, status, error) {

        }
    });

});

function rippleHandler(e) {
    var rippleSpan = this.nextElementSibling.querySelector('.e-ripple-container');
    if (rippleSpan) {
        ejs.buttons.rippleMouseHandler(e, rippleSpan);
    }
}

function RowSelected(args) {
    ProjId = args.data.Id;
    var dto = {
        "ProjectId": args.data.Id,
        "UserId": UserId
    }

    $.ajax({
        type: 'POST',
        url: '/Dashboard/GetProjectUserRules',
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            InitializeRiles(response);
        },
        error: function(xhr, status, error) {
            console.log(error);
        }
    });
}

function InitializeRiles(data) {
    $("#commit").html("<label for=\"PCanCommit\" style=\"padding: 10px 70px 10px 0\"> Can Commit </label><input id=\"PCanCommit\" type=\"checkbox\" />");
    $("#clone").html("<label for=\"PCanClone\" style=\"padding: 10px 70px 10px 0\"> Can Clone </label><input id=\"PCanClone\" type=\"checkbox\" />");
    $("#view").html("<label for=\"PCanViewWork\" style=\"padding: 10px 70px 10px 0\"> Can View Work </label><input id=\"PCanViewWork\" type=\"checkbox\" />");
    $("#create").html("<label for=\"PCanCreateWork\" style=\"padding: 10px 70px 10px 0\"> Can Create Work Items </label><input id=\"PCanCreateWork\" type=\"checkbox\" />");
    $("#delete").html("<label for=\"PCanDeleteWork\" style=\"padding: 10px 70px 10px 0\"> Can Delete Work Items </label><input id=\"PCanDeleteWork\" type=\"checkbox\" />");
    var PCanCommit = new ejs.buttons.Switch({ change: PCanCommitClicked, checked: data.canCommit, });
    PCanCommit.appendTo('#PCanCommit');
    var PCanClone = new ejs.buttons.Switch({ change: PCanCloneClicked, checked: data.canClone });
    PCanClone.appendTo('#PCanClone');
    var PCanViewWork = new ejs.buttons.Switch({ change: PCanViewWorkClicked, checked: data.canView });
    PCanViewWork.appendTo('#PCanViewWork');
    var PCanCreateWork = new ejs.buttons.Switch({ change: PCanCreateWorkClicked, checked: data.canCreateWork });
    PCanCreateWork.appendTo('#PCanCreateWork');
    var PCanDeleteWork = new ejs.buttons.Switch({ change: PCanDeleteWorkClicked, checked: data.canDeleteWork });
    PCanDeleteWork.appendTo('#PCanDeleteWork');
    $("#ProjSettings").show();
}

function PCanCommitClicked(args) {
    CanCommit = args.checked;
    var dto = {
        "IncomingValue": CanCommit,
        "ProjId": ProjId,
        "UserId": UserId
    }

    $.ajax({
        type: 'POST',
        url: '/Dashboard/UpdatePACommitRule',
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {

        },
        error: function(xhr, status, error) {
            console.log(error);
        }
    });
}

function PCanCloneClicked(args) {
    CanClone = args.checked;
    var dto = {
        "IncomingValue": CanClone,
        "ProjId": ProjId,
        "UserId": UserId
    }

    $.ajax({
        type: 'POST',
        url: '/Dashboard/UpdatePACloneRule',
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {},
        error: function(xhr, status, error) {
            console.log(error);
        }
    });
}

function PCanViewWorkClicked(args) {
    CanView = args.checked;
    var dto = {
        "IncomingValue": CanView,
        "ProjId": ProjId,
        "UserId": UserId
    }

    $.ajax({
        type: 'POST',
        url: '/Dashboard/UpdatePAViewWorkRule',
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {},
        error: function(xhr, status, error) {
            console.log(error);
        }
    });
}

function PCanCreateWorkClicked(args) {
    CanCreate = args.checked;
    var dto = {
        "IncomingValue": CanCreate,
        "ProjId": ProjId,
        "UserId": UserId
    }


    $.ajax({
        type: 'POST',
        url: '/Dashboard/UpdatePACreateWorkRule',
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {},
        error: function(xhr, status, error) {
            console.log(error);
        }
    });
}

function PCanDeleteWorkClicked(args) {
    CanDelete = args.checked;
    var dto = {
        "IncomingValue": CanDelete,
        "ProjId": ProjId,
        "UserId": UserId
    }


    $.ajax({
        type: 'POST',
        url: '/Dashboard/UpdatePADeleteWorkRule',
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {},
        error: function(xhr, status, error) {
            console.log(error);
        }
    });
}

function ProjectRightsChanged(args) {

    var dto = {
        "IncomingValue": args.checked,
        "UserId": UserId
    }


    $.ajax({
        type: 'POST',
        url: '/Authenication/UserProjectRightsUpdated',
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {},
        error: function(xhr, status, error) {
            console.log(error);
        }
    });

}

function OpenProjects() {
    $.ajax({
        type: 'GET',
        url: '/Dashboard/GetProjects',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            RefreshGridData(response);
        },
        error: function(xhr, status, error) {

        }
    });

    //    $("#ProjectsModal").modal('show'); 
}

function LoadProjects(data) {
    ProjectGrid = new ej.grids.Grid({
        dataSource: data,
        rowSelected: ProjectRowSelected,
        recordDoubleClick: ProjectSelected,
        editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, allowEditOnDblClick: false },
        columns: [
            { field: 'name', headerText: 'Name', width: 120, textAlign: 'Left' }
        ]
    });
    ProjectGrid.appendTo('#ProjectsGrid');
}

function RefreshGridData(data) {

    $("#ProjectsGrid").html("");
    ProjectGrid = new ej.grids.Grid({
        dataSource: data,
        rowSelected: ProjectRowSelected,
        recordDoubleClick: ProjectSelected,
        editSettings: { allowEditing: true, allowAdding: true, allowDeleting: true, allowEditOnDblClick: false },
        columns: [
            { field: 'name', headerText: 'Name', width: 120, textAlign: 'Left' }
        ]
    });
    ProjectGrid.appendTo('#ProjectsGrid');
}

function ProjectRowSelected(args) {
    CProject = args.data.id;
}

function ProjectSelected(args) {
    CProject = args.rowData.id;
    AddProjectSelected();
}

function AddProjectSelected() {
    var dto = {
        "Id": CProject,
        "UserId": UserId
    }

    $.ajax({
        type: 'POST',
        url: '/Dashboard/AddProjectToUserAccount',
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            $("#ProjectsModal").modal('hide');
            $('.modal-backdrop').hide();

            $("body").removeClass("modal-open");

        },
        error: function(xhr, status, error) {
            console.log(error);
        }
    });
}

function SaveChanges() {

    var dto = {
        "Id": UserId,
        "Email": $("#Email").val(),
        "Password": $("#Password").val(),
        "FirstName": $("#FirstName").val(),
        "LastName": $("#LastName").val()
    }

    $.ajax({
        type: 'POST',
        url: '/Authenication/UserAccountUpdated',
        data: JSON.stringify(dto),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function(response) {
            window.location.href = "/Dashboard";
        },
        error: function(xhr, status, error) {
            console.log(error);
        }
    });
}

function DeleteTest(args) {
    console.log(args);
    if (args.requestType === 'delete') {
        $("#ProjSettings").hide();
        var dto = {
            "ProjectId": args.data[0].Id,
            "UserId": UserId
        }


        $.ajax({
            type: 'POST',
            url: '/Dashboard/RemoveUserFromProject',
            data: JSON.stringify(dto),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(response) {},
            error: function(xhr, status, error) {
                console.log(error);
            }
        });
    }
}