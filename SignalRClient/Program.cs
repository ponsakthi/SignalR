using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;

namespace SignalRClient
{
    class Program
    {
        private static IHubProxy HubProxy;
        static void Main(string[] args)
        {
            Console.WriteLine("Establishing Connection");
            const string Server = "http://localhost:52527/signalr";
            HubConnection Connection = new HubConnection(Server);
            Connection.Closed += Connection_Closed;
            HubProxy = Connection.CreateHubProxy("ChatHub");
            //Handle incoming event from server: use Invoke to write to console from SignalR's thread
            HubProxy.On<string, string>("broadcastMessage", (name, message) =>
            {
                Console.WriteLine(name + " : " + message);
              
            }
                );
            Console.WriteLine("Starting Connection");
            Connection.Start();
            Connection.StateChanged += Connection_StateChanged;
           
            while (true)
            {
                
            }
        }

        static void Connection_StateChanged(StateChange obj)
        {
            if (obj.NewState == ConnectionState.Connected)
            {
                HubProxy.Invoke("Echo", new[] { "PrefixFromClient:" });
            }
        }

        

        private static void Connection_Closed()
        {
            Console.WriteLine("Connection Closed");
        }
    }
}
