using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRChat
{
    //[HubName("ChatHub")]
    public class ChatHub : Hub
    {
        private string _messagePrefix = "";
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, _messagePrefix+message);
        }

        public void Echo(string message)
        {
            _messagePrefix = message;
        }
    }
}