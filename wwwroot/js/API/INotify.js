var toasts = [];
var toastObj;


toastObj = new ej.notifications.Toast({
    position: {
        X: 'Right'
    }, target: document.body,
    close: onclose,
    beforeOpen: onBeforeOpen
});
toastObj.appendTo('#toast_type');

function ShowAlert(text) {
    toastObj.show(
        {
            title: "Error",
            content: text,
            cssClass: 'e-toast-danger',
            icon: 'e-error toast-icons'
        });

}
function ShowInfo(text) {
    toastObj.show(
        {
            title: "System Notification",
            content: text,
            cssClass: 'e-toast-info',
            icon: 'PM'
        });

}
setTimeout(function () {

}, 200);

function ShowNotification(data) {
    toastObj.show(data);
}

function onclose(e) {
    if (e.toastContainer.childElementCount === 0) {
        document.getElementById('hideTosat').style.display = 'none';

        if (e.options.id !== undefined) {
            RemoveNotification(e.options.id);
        }
    }
}

 
function onBeforeOpen() {

   // document.getElementById('hideTosat').style.display = 'inline-block';
}
 
 