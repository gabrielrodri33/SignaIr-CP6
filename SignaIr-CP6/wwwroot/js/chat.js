"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

document.getElementById("sendButton").disabled = true;

// Receber uma nova mensagem
connection.on("ReceiveMessage", (user, message) => {
    const msg = document.createElement("div");
    msg.textContent = `${user}: ${message}`;
    document.getElementById("messagesList").appendChild(msg);
});

// Receber o histórico de mensagens
connection.on("ReceiveMessageHistory", (messages) => {
    document.getElementById("messagesList").innerHTML = '';
    messages.forEach(message => {
        const msg = document.createElement("div");
        msg.textContent = `${message.User}: ${message.Message}`;
        document.getElementById("messagesList").appendChild(msg);
    });
});

document.getElementById("sendButton").addEventListener("click", event => {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;

    if (user && message) {
        connection.invoke("SendMessage", user, message).catch(err => console.error(err.toString()));
    } else {
        console.error("Usuário ou mensagem estão vazios");
    }

    event.preventDefault();
});

connection.start()
    .then(() => {
        console.log("Conectado ao chat");
    })
    .catch(err => console.error("Erro ao conectar: ", err.toString()));

document.getElementById("sendButton").addEventListener("click", event => {
    const user = document.getElementById("userInput").value;
    const message = document.getElementById("messageInput").value;

    if (user && !userJoined) {
        connection.invoke("UserJoined", user)
            .then(() => {
                userJoined = true;
            })
            .catch(err => console.error(err.toString()));
    }

    if (user && message) {
        connection.invoke("SendMessage", user, message)
            .catch(err => console.error(err.toString()));
    } else {
        console.error("Usuário ou mensagem estão vazios");
    }

    event.preventDefault();
});