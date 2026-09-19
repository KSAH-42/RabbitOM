using System;

namespace RabbitOM.Node.Services
{
    using RabbitOM.Net.Rtsp;
    using RabbitOM.Net.Rtsp.Clients;
	using RabbitOM.Node.Scripting;

	public sealed class ApplicationService : IApplicationService
	{
        private readonly IScriptRunner _scriptRunner;
        private readonly RtspUri _uri;



		public ApplicationService(IScriptRunner scriptRunner , string uri )
		{
			_scriptRunner = scriptRunner ?? throw new ArgumentNullException( nameof( scriptRunner ) );
			_uri = RtspUri.Parse( uri );
		}



		 public void Run()
        {
            using ( var client = new RtspClient() )
            using ( _scriptRunner )
			{
				_scriptRunner.Start();

				client.CommunicationStarted += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine( "Communication started - " + DateTime.Now );

					_scriptRunner.PostEvent( sender , e );
				};

                client.CommunicationStopped += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine( "Communication stopped - " + DateTime.Now );

					_scriptRunner.PostEvent( sender , e );
				};

                client.Connected += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine( "Client connected - " + client.Configuration.Uri );

					_scriptRunner.PostEvent( sender , e );
				};

                client.Disconnected += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine( "Client disconnected - " + DateTime.Now );

					_scriptRunner.PostEvent( sender , e );
				};

                client.Error += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine( "Client Error: " + (sender as RtspClient).Configuration.Uri + " " + e.Code );

					_scriptRunner.PostEvent( sender , e );
				};

                client.PacketReceived += ( sender , e ) =>
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine( "DataReceived {0}" , e.Packet.Data.Length );

					_scriptRunner.PostEvent( sender , e );
				};

                client.Configuration.Uri = _uri.ToString( true );
                client.Configuration.UserName = _uri.UserName;
                client.Configuration.Password = _uri.Password;
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
