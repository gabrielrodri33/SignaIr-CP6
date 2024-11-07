"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;

// Configurando eventos para escutar notificações de entrada e saída de usuário
connection.on("UserJoined", (user) => {
    const msg = `${user} entrou no chat`;
    const messageDiv = document.createElement("div");
    messageDiv.textContent = msg;
    document.getElementById("messagesList").appendChild(messageDiv);
});

connection.on("UserLeft", (user) => {
    const msg = `${user} saiu do chat`;
    const messageDiv = document.createElement("div");
    messageDiv.textContent = msg;
    document.getElementById("messagesList").appendChild(messageDiv);
});

connection.on("ReceiveMessage", function (user, message) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${user} diz: ${message}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;

    const user = document.getElementById("userInput").value;
    if (user) {
        connection.invoke("UserJoined", user).catch(function (err) {
            console.error(err.toString());
        });
    }
    document.getElementById("userInput").focus();
}).catch(function (err) {
    console.error("Erro ao conectar: ", err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    if (user && !document.getElementById("userInput").hasAttribute('data-joined')) {
        connection.invoke("UserJoined", user).catch(function (err) {
            return console.error(err.toString());
        });
        document.getElementById("userInput").setAttribute('data-joined', 'true');
    }

    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
