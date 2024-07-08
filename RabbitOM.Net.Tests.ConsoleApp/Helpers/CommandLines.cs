using System;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Net.Tests.ConsoleApp
{
    using RabbitOM.Net.Rtsp;

    public sealed class CommandLines
    {
        private readonly string[] _args;
        
        private readonly bool _sinkOption ;
        
        private readonly string _uriOption = string.Empty;

        private readonly string _uri = string.Empty;

        private readonly string _userName = string.Empty;

        private readonly string _password = string.Empty;








        public CommandLines( string[] args )
        {
            _args = args ?? throw new ArgumentNullException( nameof( args ) );

            _uriOption = args.FirstOrDefault();

            _sinkOption = args.Any( x => x == "-sink" );

            if ( RTSPUri.TryParse( _uriOption , out RTSPUri uri ) )
            {
                _uri = uri.ToString( true );
                _userName = uri.UserName;
                _password = uri.Password;
            }
        }






        public string UriOption
        {
            get => _uriOption;
        }

        public bool SinkOption
        {
            get => _sinkOption;
        }

        public string Uri
        {
            get => _uri;
        }

        public string UserName
        {
            get => _userName;
        }
        public string Password
        {
            get => _password;
        }






        public bool CanShowHelp()
        {
			return ! _args.Any() || string.IsNullOrWhiteSpace( _uri );
        }

        public void ShowHelp()
        {
            string processName = Assembly.GetExecutingAssembly().GetName().Name + ".exe";

            Console.WriteLine( $"Receiving packets from a rtsp source" );
            Console.WriteLine();
            Console.WriteLine( "Usage: " );
            Console.WriteLine();
            Console.WriteLine( $"{processName} rtsp://127.0.0.1/toy.mp4" );
            Console.WriteLine( $"{processName} rtsp://admin:camera123@127.0.0.1/toy.mp4" );
            Console.WriteLine( $"{processName} rtsp://127.0.0.1:554/toy.mp4" );
            Console.WriteLine( $"{processName} rtsp://admin:camera123@127.0.0.1:554/toy.mp4" );
            Console.WriteLine();
        }
    }
}