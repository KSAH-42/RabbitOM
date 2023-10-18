using System;

namespace RabbitOM.Net.Rtsp.Tests.ConsoleApp
{
    using RabbitOM.Net.Rtsp;
    using RabbitOM.Net.Rtsp.Clients;

    class Program
    {
        // TODO: Remove the client class
        // TODO: Goal, write individual client class with it respective configuration
        // TODO: Write a client receiver class for tcp streaming
        // TODO: Write a client receiver class for udp streaming
        // TODO: Write a client receiver class for multicast streaming
        // TODO: Refactor the configuration class make it fully immutable/readonly and remove the lock
        // TODO: Then inject the configuration class on each receiver constructor
        // TODO: Secure password using SecureString 
        // TODO: Find something about the duplicated code on scope object used by queue => inject a ICollection<T> or something like this ?
        // TODO: Reduce memory allocations
        // TODO: Introduce complete frame objects 
        // TODO: At the end, remove un-necessary try catch
        // TODO: Some classes need to be removed 
        //       -> RTSPFile used for debugging

        // If you want to get more features, used the connection class instead to control the protocol messaging layer
        // using ( var connection = new RabbitOM.Net.Rtsp.Remoting.RTSPConnection() ) {}
        // Make sure that the ports are not blocked
        // Use the vendor configuration tool to activate the rtsp protocol
        // AND create a user account for the rtsp connection
        // You can try to find a online security camera F R O M  a manufacturer, but ...
        // I strongly recommend to BUY a camera, and don't waste your time to find a security camera online from any manufacturers
        // Otherwise you can use HappyRtspServer software

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

                // You known it: for reducing the CPU and memory consumption 
                // comment the code below :

                Console.WriteLine("DataReceived {0} ", e.Packet.Data.Length);
            };

            #endregion

            client.Configuration.Uri = Constants.LocalServer;
            client.Configuration.UserName = Constants.UserName;
            client.Configuration.Password = Constants.Password;
            client.Configuration.KeepAliveType = RTSPKeepAliveType.Options; // <--- you must read the protocol documentation of the vendor to be sure.
            client.Configuration.ReceiveTimeout = TimeSpan.FromSeconds(3); // <-- increase the timeout if the camera is located far away 
            client.Configuration.SendTimeout = TimeSpan.FromSeconds(5);

            client.StartCommunication();

            Console.BufferHeight = 100;

            Console.WriteLine("Press any keys to close the application");

            Console.ReadKey();

            client.StopCommunication(TimeSpan.FromSeconds(3));
        }
    }
}
