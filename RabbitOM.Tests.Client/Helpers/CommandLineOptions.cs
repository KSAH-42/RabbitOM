using System;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Tests.Client.Helpers
{
    using RabbitOM.Streaming.Net.Rtsp;

    public sealed class CommandLineOptions
    {        
        private CommandLineOptions() { }



        public string Uri { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }



        public bool TryValidate()
        { 
            if ( string.IsNullOrWhiteSpace( Uri ) )
            {
                return false;
            }

            return ! string.IsNullOrWhiteSpace( Password ) ? 
                   ! string.IsNullOrWhiteSpace( UserName ) : true;
        }

        

        public static bool TryParse( string[] args , out CommandLineOptions result )
        {
            result = default;

            if ( RtspUri.TryParse( args?.FirstOrDefault() , out RtspUri uri ) )
            {
                result = new CommandLineOptions() { Uri = uri.ToString( true ) , UserName = uri.UserName , Password = uri.Password };
            }

            return result != null;
        }

        public static void ShowHelp()
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
    }
}