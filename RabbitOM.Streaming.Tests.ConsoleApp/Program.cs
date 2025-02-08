// If you want to get more features, used the RtspConnection class instead to get more control of the protocol messaging layer
// Make sure that the ports are not blocked
// Use the vendor configuration tool to activate the Rtsp protocol especially the port
// AND create a user account for the Rtsp connection
// You can try to find a online security camera F R O M  a manufacturer, but ...
// I strongly recommend to BUY a camera, it is better, and don't waste your time to find a security camera online from any manufacturers
// Otherwise you can use HappyRtspServer software but it does not reflect an ip security camera

using System;

namespace RabbitOM.Streaming.Tests.ConsoleApp
{
    using RabbitOM.Streaming;
    using RabbitOM.Streaming.Rtsp;
    using RabbitOM.Streaming.Rtsp.Clients;
    using RabbitOM.Streaming.Tests.ConsoleApp.Helpers;
    
    class Program
    {
        static void Main( string[] args )
        {
            try
            {              
                var options = CommandLineOptions.Parse( args );
                
                if ( options.TryValidate() )
                {
                    Run( options );
                }
                else
                {
                    options.ShowHelp();
                }
            }
            finally
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        static void Run( CommandLineOptions options )
        {
            if ( options == null )
            {
                throw new ArgumentNullException( nameof( options ) );
            }
            
            using ( var client = new RtspClient() )
            {
                client.CommunicationStarted += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine( "Communication started - " + DateTime.Now );
                };

                client.CommunicationStopped += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine( "Communication stopped - " + DateTime.Now );
                };
              
                client.Connected += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine( "Client connected - " + client.Configuration.Uri );
                };

                client.Disconnected += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine( "Client disconnected - " + DateTime.Now );
                };

                client.Error += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine( "Client Error: " + (sender as RtspClient).Configuration.Uri + " " + e.Code );
                };

                client.PacketReceived += ( sender , e ) =>
                {
                    var interleavedPacket = e.Packet as RtspInterleavedPacket;

                    if ( interleavedPacket != null && interleavedPacket.Channel > 0 )
                    {
                        // In most of case, avoid this packet
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine( "Skipping some data : size {0}" , e.Packet.Data.Length );
                        return;
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine( "DataReceived {0}" , e.Packet.Data.Length );
                };

                // Please note, read the manufacturer's documentation
                // to get the right uri

                client.Configuration.Uri = options.Uri;
                client.Configuration.UserName = options.UserName;
                client.Configuration.Password = options.Password;
                client.Configuration.ReceiveTimeout = TimeSpan.FromSeconds( 3 );
                client.Configuration.SendTimeout = TimeSpan.FromSeconds( 3 );
                client.Configuration.KeepAliveType = RtspKeepAliveType.Options;
                client.Configuration.MediaFormat = RtspMediaFormat.Video;
                client.Configuration.DeliveryMode = RtspDeliveryMode.Tcp;

                // client.Configuration.DeliveryMode = RtspDeliveryMode.Multicast;
                // client.Configuration.MulticastAddress = "229.0.0.1";
                // client.Configuration.RtpPort = 55000;
                // client.Configuration.TimeToLive = 15;
                
                client.StartCommunication();    

                Console.CancelKeyPress += ( sender , e ) => Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine( "Press any keys to close the application" );
                Console.ReadKey();

                client.StopCommunication( TimeSpan.FromSeconds( 3 ) );
            }
        }
    }
}
