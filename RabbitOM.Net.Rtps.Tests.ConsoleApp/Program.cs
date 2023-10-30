using RabbitOM.Net.Rtsp;
using RabbitOM.Net.Rtsp.Clients;
using System;

namespace RabbitOM.Net.Rtsp.Tests.ConsoleApp
{
	class Program
    {
        // TODO: Force header class to be immutable: used constructor or method factory and pass setters to private


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

        static void Main(string[] args)
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
                Console.WriteLine("Client Error: " + e.Code + " " + e.Message);
            };

            client.PacketReceived += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("DataReceived {0}", e.Packet.Data.Length );
            };

            #endregion
        
            // Please note, that rtsp uri is not the same from a camera to another

            client.Configuration.Uri = Constants.LocalServer_Source_3;
            client.Configuration.UserName = Constants.UserName;
            client.Configuration.Password = Constants.Password;
            client.Configuration.KeepAliveType = RTSPKeepAliveType.Options; // <--- you must read the protocol documentation of the vendor to be sure.
            client.Configuration.ReceiveTimeout = TimeSpan.FromSeconds(3); // <-- increase the timeout if the camera is located far away 
            client.Configuration.SendTimeout = TimeSpan.FromSeconds(5);

            client.StartCommunication();
            
            Console.BufferHeight = 1000;

            Console.WriteLine("Press any keys to close the application");

            Console.ReadKey();

            client.StopCommunication(TimeSpan.FromSeconds(3));
        }
    }
}