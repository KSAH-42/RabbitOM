using System;

namespace RabbitOM.Node.Application
{
    using RabbitOM.Net.Rtsp;
    using RabbitOM.Net.Rtsp.Clients;
	using RabbitOM.Node.Logging;
	using RabbitOM.Node.Scripting;
    using RabbitOM.Node.Scripting.Messages;

	public sealed class NodeApplication : IApplication
	{
        private readonly ILogger _logger;

        private readonly IScriptRunner _scriptRunner;

        private readonly ApplicationSettings _settings;





		public NodeApplication( ILogger logger , IScriptRunner scriptRunner , ApplicationSettings settings )
		{
            _logger = logger ?? throw new ArgumentNullException( nameof( logger ) );

            _scriptRunner = scriptRunner ?? throw new ArgumentNullException( nameof( scriptRunner ) );

            _settings = settings ?? throw new ArgumentNullException( nameof( settings ) );
		}





		public void Run()
        {
            using ( var client = new RtspClient() )
            using ( var scope = new NodeScriptRunnerLauncher( _scriptRunner ) )
			{
				client.CommunicationStarted += ( sender , e ) =>
                {
                    _logger.Info( "Communication started" );

					_scriptRunner.PostMessage( new CommunicationStartedMessage( sender ) );
				};

                client.CommunicationStopped += ( sender , e ) =>
                {
                    _logger.Info( "Communication stopped" );

					_scriptRunner.PostMessage( new CommunicationStoppedMessage( sender ) );
				};

                client.Connected += ( sender , e ) =>
                {
                    _logger.Info( "Client connected" );

					_scriptRunner.PostMessage( new ConnectedMessage( sender ) );
				};

                client.Disconnected += ( sender , e ) =>
                {
                    _logger.Info( "Client disconnected" );

					_scriptRunner.PostMessage( new DisconnectedMessage( sender ) );
				};

                client.Error += ( sender , e ) =>
                {
                    _logger.Error( (sender as RtspClient).Configuration.Uri + " " + e.Code );

					_scriptRunner.PostMessage( new ErrorMessage( sender , e.Message ) );
				};

                client.PacketReceived += ( sender , e ) =>
                {
                    _logger.Info( "DataReceived {0}" , e.Packet.Data.Length );

					_scriptRunner.PostMessage( new PacketReceivedMessage( sender , e.Packet.Data ) );
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
