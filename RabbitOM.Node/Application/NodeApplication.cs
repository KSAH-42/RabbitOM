using System;

namespace RabbitOM.Node.Application
{
    using RabbitOM.Net.Rtsp;
    using RabbitOM.Net.Rtsp.Clients;
	using RabbitOM.Node.Logging;
	using RabbitOM.Node.Scripting;
    
	public sealed class NodeApplication : IApplication
	{
        private readonly ILogger _logger;

        private readonly IScriptHost _scriptHost;

        private readonly ApplicationSettings _settings;





		public NodeApplication( ILogger logger , IScriptHost scriptHost , ApplicationSettings settings )
		{
            _logger = logger ?? throw new ArgumentNullException( nameof( logger ) );

            _scriptHost = scriptHost ?? throw new ArgumentNullException( nameof( scriptHost ) );

            _settings = settings ?? throw new ArgumentNullException( nameof( settings ) );
		}





		public void Run()
        {
            using ( var client = new RtspClient() )
            using ( var scope = new NodeScriptHostLauncher( _scriptHost ) )
			{
				client.CommunicationStarted += ( sender , e ) =>
                {
                    _logger.Info( "Communication started" );

					_scriptHost.PostMessage( new Message( MessageTypes.CommunicationStarted , sender ) );
				};

                client.CommunicationStopped += ( sender , e ) =>
                {
                    _logger.Info( "Communication stopped" );

					_scriptHost.PostMessage( new Message( MessageTypes.CommunicationStopped , sender ) );
				};

                client.Connected += ( sender , e ) =>
                {
                    _logger.Info( "Client connected" );

					_scriptHost.PostMessage( new Message( MessageTypes.Connected , sender ) );
				};

                client.Disconnected += ( sender , e ) =>
                {
                    _logger.Info( "Client disconnected" );

					_scriptHost.PostMessage( new Message( MessageTypes.Disconnected , sender ) );
				};

                client.Error += ( sender , e ) =>
                {
                    _logger.Error( (sender as RtspClient).Configuration.Uri + " " + e.Code );

					_scriptHost.PostMessage( new Message( MessageTypes.Error , sender ) { Payload = e.Message } );
				};

                client.PacketReceived += ( sender , e ) =>
                {
                    _logger.Info( "DataReceived {0}" , e.Packet.Data.Length );

					_scriptHost.PostMessage( new Message( MessageTypes.DataReceived , sender ) { Payload = e.Packet } );
				};

                _logger.IsEnabled = _settings.EnableLogging;

                var uri = RtspUri.Parse( _settings.Uri );

                client.Configuration.Uri = uri.ToString( true );
                client.Configuration.UserName = uri.UserName;
                client.Configuration.Password = uri.Password;
                client.Configuration.ReceiveTimeout = _settings.ReceiveTimeout;
                client.Configuration.SendTimeout = _settings.SendTimeout;
                client.Configuration.KeepAliveType = RtspKeepAliveType.Options;
                client.Configuration.MediaFormat = RtspMediaFormat.Video;
                client.Configuration.DeliveryMode = RtspDeliveryMode.Tcp;

                client.StartCommunication();

                Console.CancelKeyPress += ( sender , e ) => Console.ForegroundColor = ConsoleColor.White;

                _logger.Info( "Press any keys to close the application" );
                Console.ReadKey();

                client.StopCommunication( TimeSpan.FromSeconds( 3 ) );
            }
        }
	}
}
