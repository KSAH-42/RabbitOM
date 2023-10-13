using System;

namespace RabbitOM.Net.Rtsp.Tests.ConsoleApp
{
    using RabbitOM.Net.Rtsp;
    using RabbitOM.Net.Rtsp.Clients;

    class Program
	{
        private static readonly System.Threading.EventWaitHandle s_quitEventHandle = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset);

        // TODO: Remove the client class
        // TODO: Write a client receiver class for tcp streaming
        // TODO: Write a client receiver class for udp streaming
        // TODO: Write a client receiver class for multicast streaming
        // TODO: Refactor the configuration class make immutable
        // TODO: pass the configuration on each receiver constructor

        static void Main(string[] args)
		{

            // RabbitOM.Net.Rtsp.Remoting.RTSPConnection connection = null;

            var client = new RTSPClient();

            // Make sure that the ports not blocked
            // Use the vendor configuration tool to activate the rtsp protocol
            // AND to create a user account for the rtsp connection
            // You can try try to find a online security camera F R O M  a manufacturer, but it is very hard
            // I strongly recommend to buy a camera don't waste your time to find a security camera online from any manufacturer, you will get nothing.

            //client.Configuration.Uri = Constants.Camera_HIK;
            client.Configuration.Uri = Constants.Movie; 
            client.Configuration.UserName = Constants.User_Admin;
            client.Configuration.Password = Constants.Password_Camera;
            client.Configuration.KeepAliveType = RTSPKeepAliveType.Options; // <--- you must read the protocol documentation of the vendor to be sure.
            client.Configuration.ReceiveTimeout = TimeSpan.FromSeconds(3); // <-- increase the timeout if the camera is located far away 
            client.Configuration.SendTimeout = TimeSpan.FromSeconds(5);
			
            client.Options.MediaFormat = RTSPMediaFormatType.Video;
            client.Options.DeliveryMode = RTSPDeliveryMode.Tcp;
            client.Options.UnicastPort = RTSPClientConfigurationOptions.DefaultPort;
            client.Options.MulticastAddress = "239.0.0.2";
            client.Options.MulticastPort = RTSPClientConfigurationOptions.DefaultPort + 1;
            client.Options.MulticastTTL = RTSPClientConfigurationOptions.DefaultTTL;
            client.Options.RetriesInterval = TimeSpan.FromSeconds(5);

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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Client connected - " + client.Configuration.Uri + " " +e.TrackInfo.ControlUri + " " + e.TrackInfo.SPS + " " + e.TrackInfo.SPS);
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
                Console.WriteLine("DataReceived {0} ", e.Packet.Data.Length);
            };
            
            // Start connection and handle auto reconnection in case of network error
            // You can unplug the network cable to test the auto reconnection
            client.StartCommunication();
			
            Console.WriteLine("Press any keys to close the application");

            Console.ReadKey();

            client.StopCommunication( TimeSpan.FromSeconds(3));
        }
	}
}
                   