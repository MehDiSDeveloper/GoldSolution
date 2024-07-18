var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/user").build();
connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
})
function newWindowLoadedOnClient() {
    connectionUserCount.send("newWindowLoaded");
}
function fullfilled() { //when everything is successfull
    //do something
    console.log("user hub connection successfull");
    newWindowLoadedOnClient();
}
function rejected() { //when rejected
    //log something
    console.log("user hub connection rejected");

}
connectionUserCount.start().then(fullfilled, rejected);