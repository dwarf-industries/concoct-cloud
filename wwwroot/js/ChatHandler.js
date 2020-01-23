var me = {};
me.avatar = "https://lh6.googleusercontent.com/-lr2nyjhhjXw/AAAAAAAAAAI/AAAAAAAARmE/MdtfUmC0M4s/photo.jpg?sz=48";
var you = {};
you.avatar = "https://a11.t26.net/taringa/avatares/9/1/2/F/7/8/Demon_King1/48x48_5C5.jpg";

const chatConnectionBuilder = new signalR.HubConnectionBuilder().withUrl("/chatHub")
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


chatConnectionBuilder.on("ReciveMessage", (user, message) => {

    var control = "";
    var date = formatAMPM(new Date());
    //TODO change chat message handler

    control = '<li style="width:100%;">' +
        '<div class="msj-rta macro">' +
        '<div class="text text-r">' +
        '<p>' + message + '</p>' +
        '<p><small>' + date + '</small></p>' +
        '</div>' +
        '<div class="avatar" style="padding:0px 0px 0px 10px !important"><img class="img-circle" style="width:100%;" src="' + you.avatar + '" /></div>' +
        '</li>';
    $("chat").append(control).scrollTop($("chat").prop('scrollHeight'));
    //setTimeout(
    //    function () {

    //    }, time);
});



chatConnectionBuilder.on("ReciveNotification", (data) => {
    console.log(data);
    var getData = JSON.parse(data);

    for (var cData in getData) {
        var item = getData[cData];
        console.log(item);
        $("#NotificationsPanel").append("<a class=\"dropdown-item\" style=\"font-size:20; padding:3px;\" href=\"#\"><p>" + item.NotificationMessage + "</p></a><hr/>");
        $.notify({
            // options
            title: item.NotificationTitle,
            message: item.NotificationMessage
        }, {
            // settings
            type: 'danger'
        });
    }


});

window.setInterval(function() {
    chatConnectionBuilder.invoke("NotificationRecived").catch(x => console.log(x.toString()));
}, 5000);


chatConnectionBuilder.start().catch(x => console.log(x.toString()));

$(".mytext").on("keydown", function(e) {
    if (e.which == 13) {
        SendMessage();
    }
});

$('body > div > div > div:nth-child(2) > span').click(function() {
    $(".mytext").trigger({ type: 'keydown', which: 13, keyCode: 13 });
})


function SendMessage() {
    var text = $("#MyText").val();
    if (text !== "") {
        var date = formatAMPM(new Date());

        control = '<li style="width:100%;">' +
            '<div class="msj-rta macro">' +
            '<div class="avatar" style="padding:0px 0px 0px 10px !important"><img class="img-circle" style="width:100%;" src="' + me.avatar + '" /></div>' +

            '<div class="text text-r">' +
            '<p>' + text + '</p>' +
            '<p><small>' + date + '</small></p>' +
            '</div>' +

            '</li>';
        // insertChat("me", text);
        $("chat").append(control).scrollTop($("chat").prop('scrollHeight'));
        chatConnectionBuilder.invoke("Send", text).catch(x => console.log(x.toString()));
        $("#MyText").val('');
    }
}
//-- Clear Chat
resetChat();

//-- Print Messages
insertChat("me", "Hello Tom...", 0);
insertChat("you", "Hi, Pablo", 1500);
insertChat("me", "What would you like to talk about today?", 3500);
insertChat("you", "Tell me a joke", 7000);
insertChat("me", "Spaceman: Computer! Computer! Do we bring battery?!", 9500);
insertChat("you", "LOL", 12000);


//-- NOTE: No use time on insertChat.