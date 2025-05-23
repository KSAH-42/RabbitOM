using System;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Streaming.Tests.ConsoleApp.Helpers
{
    using RabbitOM.Streaming.Rtsp;

    public sealed class CommandLineOptions
    {        
        public static readonly CommandLineOptions Empty = new CommandLineOptions();



        public string Uri { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }



        public bool TryValidate()
        { 
            if ( string.IsNullOrWhiteSpace( Uri ) )
            {
                return false;
            }

            if ( ! string.IsNullOrWhiteSpace( Password ) )
            {
                return ! string.IsNullOrWhiteSpace( UserName );
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
            if ( args == null || ! RtspUri.TryParse( args.FirstOrDefault() , out RtspUri uri ) )
            {
                return CommandLineOptions.Empty;
            }

            return new CommandLineOptions() { Uri = uri.ToString( true ) , UserName = uri.UserName , Password = uri.Password };
        }
    }
}