"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/msgHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (message) {
    var li = document.createElement("li");
    var textArea = document.getElementById("recentQuotes");
    var qHistory = document.getElementById("quotesHistory");
    textArea.value = message;
    qHistory.removeAttribute("hidden");
    document.getElementById("messagesList").appendChild(li);

    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `Quote of the day: ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    //var user = document.getElementById("userInput").value;
    //var message = document.getElementById("messageInput").value;
    let textArea = document.getElementById("recentQoutesDiv");
    textArea.removeAttribute("hidden");
    connection.invoke("SendMessage").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});