const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/Chat/Index")
    .build();

hubConnection.on('receiveMessage', addMessageToChat);

function sendMessageToHub(message) {
    hubConnection.invoke('Send', message);
}

hubConnection.start()
    .catch(error => {
        console.error(error.message);
    });