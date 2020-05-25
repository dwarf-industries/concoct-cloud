

﻿const chatConnectionBuilder = new signalR.HubConnectionBuilder().withUrl("/ChatHub")

    .configureLogging(signalR.LogLevel.Information)
    .build();

function formatAMPM(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return strTime;
}

//-- No use time. It is a javaScript effect.
function insertChat(who, text, time) {


}

function resetChat() {
    $("chat").empty();
}


chatConnectionBuilder.on("ReciveMessage", (message) => {

    var control = "";
    var date = formatAMPM(new Date());
    //TODO change chat message handler
    var OutgoingChatHubData = JSON.parse(message);
    // {
    //     "ActiveRoom" : ActiveUser.ActiveRoom,
    //     "ProjectId" : ActiveUser.ProjectId,
    //     "Message" : text
    // };
    if(ActiveUser.ActiveRoom === OutgoingChatHubData.ActiveRoom)
    {
        control =   "<div class=\"row ChatMessage\">"+
                            "<div class=\"col-md-2\">"+
                                "<img class=\"ResponsiveChatImage\" src=\"https://www.medicinelodge.ca/wp-content/uploads/missing-avatar.jpg\" />"+
                            "</div>"+
                            "<div class=\"col-md-10\">"+
                                "<div class=\"row\">"+
                                    "<p>"+ 
                                        "<span class=\"ChatUserName\">"+OutgoingChatHubData.SenderName+"</span>,"+ date+""+
                                    "</p>"+
                                "</div>"+
                                "<div class=\"row ChatMessageContent\">"+
                                    "<p class=\"AlignText\">"+
                                    OutgoingChatHubData.Message
                                    +"</p>"+
                                "</div>"+
                            "</div>"+
                        "</div>";
        $("ChatArea").append(control).scrollTop($("ChatArea").prop('scrollHeight'));
        //setTimeout(
        //    function () {

        //    }, time);
    }
    else
        SetRoomNewMessage(OutgoingChatHubData.ActiveRoom);

});



chatConnectionBuilder.on("ReciveNotification", (data) => {
    console.log(data);
    var getData = JSON.parse(data);

    for (var cData in getData) {
        var item = getData[cData];
        console.log(item);    
        ShowNotification(item);

    }


});
 
chatConnectionBuilder.start().catch(x => console.log(x.toString()));
$("#InputChat").on("keydown", function(e) {
    if (e.which == 13) {
        SendChatRoomMessage();
    }
});

$('body > div > div > div:nth-child(2) > span').click(function() {
    $("#InputChat").trigger({ type: 'keydown', which: 13, keyCode: 13 });
})


function SendChatRoomMessage() {
    var text = $("#InputChat").val();
    if (text !== "") {
        var date = formatAMPM(new Date());


        control =   "<div class=\"row ChatMessage\">"+
                        "<div class=\"col-md-2\">"+
                            "<img class=\"ResponsiveChatImage\" src=\"https://www.medicinelodge.ca/wp-content/uploads/missing-avatar.jpg\" />"+
                        "</div>"+
                        "<div class=\"col-md-10\">"+
                            "<div class=\"row\">"+
                                "<p>"+ 
                                    "<span class=\"ChatUserName\">"+ActiveUser.Name+"</span>,"+ date+""+
                                "</p>"+
                            "</div>"+
                            "<div class=\"row ChatMessageContent\">"+
                                "<p class=\"AlignText\">"+
                                text
                                +"</p>"+
                            "</div>"+
                        "</div>"+
                    "</div>";
        
        // insertChat("me", text);
        var OutgoingChatHubData = {
            "ActiveRoom" : ActiveUser.ActiveRoom,
            "ProjectId" : ActiveUser.ProjectId,
            "Message" : text
        };
        console.log("Sending");
        $("#ChatArea").append(control).scrollTop($("ChatArea").prop('scrollHeight'));
        chatConnectionBuilder.invoke("Send", JSON.stringify(OutgoingChatHubData)).catch(x => console.log(x.toString()));
        $("#InputChat").val('');
    }
}
//-- Clear Chat
resetChat();
