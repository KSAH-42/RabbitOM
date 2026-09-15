using System;

namespace RabbitOM.Node.Services
{
    using RabbitOM.Net.Rtsp;
    using RabbitOM.Net.Rtsp.Clients;
    using RabbitOM.Node.Shell;

	public sealed class ApplicationService : IApplicationService
	{
        private readonly ApplicationParameters _parameters;

        private readonly IEventDispatcher _dispatcher;





        public ApplicationService( ApplicationParameters parameters , IEventDispatcher dispatcher )
        {
            _parameters = parameters ?? throw new ArgumentNullException( nameof( parameters ) );
            _dispatcher = dispatcher ?? throw new ArgumentNullException( nameof( dispatcher ) );
        }





        public void Run()
        {
            var uri = RtspUri.Parse( _parameters.Uri );

            using ( _dispatcher )
            using ( var client = new RtspClient() )
            {
                client.CommunicationStarted += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine( "Communication started - " + DateTime.Now );

                    _dispatcher.DispatchEvent( EventNames.CommunicationStartEvent );
                };

                client.CommunicationStopped += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine( "Communication stopped - " + DateTime.Now );

                    _dispatcher.DispatchEvent( EventNames.CommunicationStopEvent );
                };

                client.Connected += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine( "Client connected - " + client.Configuration.Uri );

                    _dispatcher.DispatchEvent( EventNames.ConnectedEvent );
                };

                client.Disconnected += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine( "Client disconnected - " + DateTime.Now );

                    _dispatcher.DispatchEvent( EventNames.DisconnectedEvent );
                };

                client.Error += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine( "Client Error: " + (sender as RtspClient).Configuration.Uri + " " + e.Code );

                    _dispatcher.DispatchEvent( EventNames.ErrorEvent );
                };

                client.PacketReceived += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine( "DataReceived {0}" , e.Packet.Data.Length );
                };

                client.Configuration.Uri = uri.ToString( true );
                client.Configuration.UserName = uri.UserName;
                client.Configuration.Password = uri.Password;
                client.Configuration.ReceiveTimeout = TimeSpan.FromSeconds( 3 );
                client.Configuration.SendTimeout = TimeSpan.FromSeconds( 3 );
                client.Configuration.KeepAliveType = RtspKeepAliveType.Options;
                client.Configuration.MediaFormat = RtspMediaFormat.Video;
                client.Configuration.DeliveryMode = RtspDeliveryMode.Tcp;

                client.StartCommunication();

                Console.CancelKeyPress += ( sender , e ) => Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine( "Press any keys to close the application" );
                Console.ReadKey();

                client.StopCommunication( TimeSpan.FromSeconds( 3 ) );
            }
        }
	}
}
