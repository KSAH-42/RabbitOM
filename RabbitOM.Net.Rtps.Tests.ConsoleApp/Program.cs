using System;

namespace RabbitOM.Net.Rtps.Tests.ConsoleApp
{
    using RabbitOM.Net.Rtps;
    using RabbitOM.Net.Rtps.Clients;

    class Program
	{
        // TODO: Remove the client class
        // TODO: Write a client receiver class for tcp streaming
        // TODO: Write a client receiver class for udp streaming
        // TODO: Write a client receiver class for multicast streaming
        // TODO: Refactor the configuration class make immutable
        // TODO: pass the configuration on each receiver constructor

        static void Main(string[] args)
		{
            var client = new RTSPClient();

            // I recommend to used a powershell command to be sure that RTPS port on the security camera is opened.
            // Otherwise
            // Use the vendor configuration tool to activate the rtsp protocol
            // AND to create a user account. 

            client.Configuration.Uri = Constants.Camera_HIK;
            client.Configuration.UserName = Constants.User_Admin;
            client.Configuration.Password = Constants.Password_Camera;
            client.Configuration.ReceiveTimeout = TimeSpan.FromSeconds(15);
            client.Configuration.SendTimeout = TimeSpan.FromSeconds(15);
            client.Configuration.KeepAliveType = RTSPKeepAliveType.Options;

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
                Console.WriteLine("Client Error: " + e.Code);
            };

            client.PacketReceived += (sender, e) => 
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("DataReceived {0} ", e.Packet.Data.Length);
            };

            client.StartCommunication();
			
            Console.WriteLine();

            using ( var eventHandle = new System.Threading.EventWaitHandle(false, System.Threading.EventResetMode.ManualReset ) )
            {
                Console.CancelKeyPress += (sender, e) =>
                {
                    Console.WriteLine("Closing the application");

                    e.Cancel = true;
                    eventHandle.Set();
                };

                eventHandle.WaitOne();
                Console.WriteLine("Stopping");
            }
            
            client.StopCommunication( TimeSpan.FromSeconds(2));
        }
	}
}
