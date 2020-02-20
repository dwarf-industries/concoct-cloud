 




var WorkItemUserOption = new ejs.buttons.Switch({ checked: workItemRule === 1 ? true :false });
WorkItemUserOption.appendTo('#WorkItemUserOption');
var ChatUserChannels = new ejs.buttons.Switch({ checked: chatRule === 1 ? true: false });
ChatUserChannels.appendTo('#ChatUserChannels');
var EditUserRightsUser = new ejs.buttons.Switch({ checked: editUserRights === 1 ? true : false });
EditUserRightsUser.appendTo('#EditUserRightsUser');
var IterationUserOptions = new ejs.buttons.Switch({ checked: iterationRule ===  1 ? true : false });
IterationUserOptions.appendTo('#IterationUserOptions');
var ScheduleUserManagement = new ejs.buttons.Switch({ checked: scheduleItem === 1 ? true : false });
ScheduleUserManagement.appendTo('#ScheduleUserManagement');
var ViewWorkUserItems = new ejs.buttons.Switch({ checked: viewWorkItem === 1 ? true : false });
ViewWorkUserItems.appendTo('#ViewWorkUserItems');
$(document).ready(function() {
    $("#ProjSettings").hide();

    var bindingData = [];

    // projects.forEach(function(element) {
    //     var current = {
    //         "Name": element.ProjectName,
    //         "Id": element.Id
    //     }
    //     bindingData.push(current);
    // });
    // console.log(bindingData);

 
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

    var SaveChanges = new ej.buttons.Button({ cssClass: 'e-outline', isPrimary: true });
    SaveChanges.appendTo('#SaveChanges');
});

 

function SaveChanges() {

    var accountRights =
    {
        "WorkItemOption": WorkItemUserOption.properties.checked === true ? 1: 0,
        "ChatChannels": ChatUserChannels.properties.checked === true? 1:0,
        "EditUserRights":EditUserRightsUser.properties.checked === true? 1:0,
        "IterationOptions":IterationUserOptions.properties.checked === true? 1: 0,
        "ViewWorkItems": ViewWorkUserItems.properties.checked === true ? 1 : 0, 
        "ScheduleManagement": ScheduleUserManagement.properties.checked === true? 1 :0,
        "Name":"asd",
        "AccountId": 0
    }
    var dto = {
        "Id": UserId,
        "Email": $("#Email").val(),
        "Password": $("#Password").val(),
        "FirstName": $("#FirstName").val(),
        "LastName": $("#LastName").val(),
        "Rights": accountRights,
        "ProjectId": projectId
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