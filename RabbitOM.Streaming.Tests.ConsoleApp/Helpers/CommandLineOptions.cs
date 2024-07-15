using System;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Streaming.Tests.ConsoleApp
{
    using RabbitOM.Streaming.Rtsp;

    public sealed class CommandLineOptions
    {        
        public static readonly CommandLineOptions Empty = new CommandLineOptions();





        private readonly string _uri = string.Empty;

        private readonly string _userName = string.Empty;

        private readonly string _password = string.Empty;






		private CommandLineOptions()
		{
		}

        public CommandLineOptions( string uri , string userName , string password )
        {
            _uri      = uri      ?? string.Empty;
            _userName = userName ?? string.Empty;
            _password = password ?? string.Empty;
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






        public bool TryValidate()
        {
            if ( string.IsNullOrWhiteSpace( _uri ) )
            {
                return false;
            }

            if ( ! string.IsNullOrWhiteSpace( _password ) )
            {
                return ! string.IsNullOrWhiteSpace( _userName );
            }

            return true;
        }

        public void ShowHelp()
        {
            string processName = Assembly.GetExecutingAssembly().GetName().Name + ".exe";

            Console.WriteLine( $"Receiving packets from a Rtsp source" );
            Console.WriteLine();
            Console.WriteLine( "Usage: " );
            Console.WriteLine();
            Console.WriteLine( $"{processName} rtsp://127.0.0.1/toy.mp4" );
            Console.WriteLine( $"{processName} rtsp://admin:camera123@127.0.0.1/toy.mp4" );
            Console.WriteLine( $"{processName} rtsp://127.0.0.1:554/toy.mp4" );
            Console.WriteLine( $"{processName} rtsp://admin:camera123@127.0.0.1:554/toy.mp4" );
            Console.WriteLine();
        }






        public static CommandLineOptions Parse( string[] args )
        {
            if ( args == null )
            {
                return CommandLineOptions.Empty;
            }

            if ( ! RtspUri.TryParse( args.FirstOrDefault() , out RtspUri uri ) )
            {
                return CommandLineOptions.Empty;
            }

            return new CommandLineOptions( uri.ToString( true ) , uri.UserName , uri.Password );
        }
    }
}