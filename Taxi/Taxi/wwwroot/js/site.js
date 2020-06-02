// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
class Message {
    constructor(username, text, when) {
        this.userName = username;
        this.text = text;
        this.when = when;
    }
}

const username = userName;
const textInput = document.getElementById('messageText');
const chat = document.getElementById('chat');
const messagesQueue = [];


function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage(event) {
    let text = textInput.value || "";
    textInput.value = "";
    if (text.trim() === "") return;
    
    let message = new Message(username, text);
    sendMessageToHub(message);
}

function addMessageToChat(message) {
    const isCurrentUserMessage = message.userName === username;
    const offset = isCurrentUserMessage ? "col-md-6 offset-md-6" : "";
    const contcolor = isCurrentUserMessage ? "bg-primary" : "bg-light";
    const textAlign = isCurrentUserMessage ? "text-right text-white" : "text-left";
    const timePosition = isCurrentUserMessage ? "time-right text-light" : "time-left";
    const containerClass = isCurrentUserMessage ? "container darker" : "container";
    const row = document.createElement('div');
    row.insertAdjacentHTML('beforeend',
        `<div class="${offset}">
                            <div class="${containerClass} ${contcolor}">
                                <p class="sender ${textAlign}">${message.userName}</p>
                                <p class="${textAlign}">${message.text}</p>
                                <span class="${timePosition}">${message.when}</span>
                            </div>
                        </div>`);
    
    chat.appendChild(row);
}