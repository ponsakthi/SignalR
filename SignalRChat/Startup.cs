using System;
using System.Diagnostics;
using System.Reflection;
using Autofac;
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Owin;
[assembly: OwinStartup(typeof(SignalRChat.Startup))]
namespace SignalRChat
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
           // app.MapSignalR();
            var builder = new ContainerBuilder();

            // STANDARD SIGNALR SETUP:

            // Get your HubConfiguration. In OWIN, you'll create one
            // rather than using GlobalHost.
            var config = new HubConfiguration();

            // Register your SignalR hubs.
            builder.RegisterHubs(Assembly.GetExecutingAssembly());

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.Resolver = new AutofacDependencyResolver(container);

            // OWIN SIGNALR SETUP:

            // Register the Autofac middleware FIRST, then the standard SignalR middleware.
            app.UseAutofacMiddleware(container);
            app.MapSignalR("/signalr", config);
        }
    }

}