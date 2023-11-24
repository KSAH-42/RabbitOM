using RabbitOM.Net.Rtsp;
using RabbitOM.Net.Rtsp.Clients;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Net.Rtsp.Tests.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
			var arguments = new Dictionary<string, string>();
            
            #region MENU

            arguments["uri"] = args.ElementAtOrDefault(0);
            arguments["-u"]  = args.ElementAtOrDefault(args.IndexAfter(x => x == "-u"));
            arguments["-p"]  = args.ElementAtOrDefault(args.IndexAfter(x => x == "-p"));

            if ( string.IsNullOrEmpty(arguments["uri"]) || args.Contains( "/?") ) 
            {
                string processName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe";

                Console.WriteLine("Receiving packet from a rtsp source ");
                Console.WriteLine("Usage: ");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{processName} rtsp://127.0.0.1/toy.mp4");
                Console.WriteLine($"{processName} rtsp://127.0.0.1/toy.mp4  -u admin -p camera123");
                Console.WriteLine($"{processName} rtsp://127.0.0.1:554/toy.mp4");
                Console.WriteLine($"{processName} rtsp://127.0.0.1:554/toy.mp4 -u admin -p camera123");
                Console.WriteLine();
                Console.WriteLine("-u: the username");
                Console.WriteLine("-p: the password");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            #endregion

            Run(arguments["uri"], arguments["-u"], arguments["-p"]);
        }

        // ----> don't forget to use <inheritdoc/> for comments

        // TODO: About namespace: keep it flatten, don't use headers namespace but move the remoting namespace into the clients namespaces

        // TODO: Force header class to be immutable: used constructor or method factory and pass setters to private
        
        // TODO: Remove some locks on the messaging layer

        // TODO: On the connection class, remove the error handler and replace it by the differents event handler, in this case, avoid unification using strategy pattern
        // TODO: A dispose method on the client object
        // TODO: Code review on the SDP layer
        // TODO: Write an efficient memory buffer for replacing the RTSPMemoryStream class
        // TODO: Remove the client class
        // TODO: Goal: write individual client class with it respective configuration
        // TODO: Write a client receiver class for tcp streaming
        // TODO: Write a client receiver class for udp streaming
        // TODO: Write a client receiver class for multicast streaming
        // TODO: Refactor the configuration class make it fully immutable/readonly and remove the lock
        // TODO: Then inject the configuration class on each receiver constructor
        // TODO: Secure password using SecureString 
        // TODO: Duplicated code: unify scope classes 
        // TODO: Reduce memory allocations
        //         => use a small memory pool ?
        // TODO: Introduce complete frame objects 
        // TODO: Pass all class to internals which are not supposed to be used by the developper who want to used the assembly

        // TODO: Use some the lastest feature of C# 9.0 like record type for configuration classes
        //       and remove factory methods => it must be done at the end of the final release.

        // If you want to get more features, used the RTSPConnection class instead to get more control of the protocol messaging layer
        // Make sure that the ports are not blocked
        // Use the vendor configuration tool to activate the rtsp protocol especially the port
        // AND create a user account for the rtsp connection
        // You can try to find a online security camera F R O M  a manufacturer, but ...
        // I strongly recommend to BUY a camera, it is better, and don't waste your time to find a security camera online from any manufacturers
        // Otherwise you can use HappyRtspServer software but it does not reflect an ip security camera


        static void Run(string uri, string userName, string password)
        {
            var client = new RTSPClient();

            #region Events

            client.CommunicationStarted += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Communication started - " + DateTime.Now);
            };

            client.CommunicationStopped += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Communication stopped - " + DateTime.Now);
            };

            client.Connected += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Client connected - " + client.Configuration.Uri);
            };

            client.Disconnected += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Client disconnected - " + DateTime.Now);
            };

            client.Error += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Client Error: " + e.Code);
            };

            client.PacketReceived += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("DataReceived {0}", e.Packet.Data.Length);
            };

            #endregion

            // Please note, that rtsp uri is not the same from a camera to another

            client.Configuration.Uri = uri; 
            client.Configuration.UserName = userName;
            client.Configuration.Password = password;
            client.Configuration.KeepAliveType = RTSPKeepAliveType.Options; // <--- you must read the protocol documentation of the vendor to be sure.
            client.Configuration.ReceiveTimeout = TimeSpan.FromSeconds(3); // <-- increase the timeout if the camera is located far away 
            client.Configuration.SendTimeout = TimeSpan.FromSeconds(5);
			
            // For multicast settings, please make sure
            // that the camera or the video source support multicast
            // For instance, the happy RTSP server does not support multicast
            // AND make sure that your are used a switch not a hub, very is difference between them
            // And activate igmp snooping on the switch

            // client.Options.DeliveryMode = RTSPDeliveryMode.Multicast;
            // client.Options.MulticastAddress = "229.0.0.1";
            // client.Options.MulticastPort = 55000;

            client.StartCommunication();

            Console.WriteLine("Press any keys to close the application");
            Console.ReadKey();

            client.StopCommunication(TimeSpan.FromSeconds(3));
        }
    }
}