using System;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Net.Rtsp.Tests.ConsoleApp
{
    public sealed class CommandLines
    {
        private readonly string[] _args;
        
        private readonly string _uriOption;
        
        private readonly bool _sinkOption;







        public CommandLines( string[] args )
        {
            _args = args ?? throw new ArgumentNullException( nameof( args ) );

            _uriOption = RTSPUri.TryParse( args.FirstOrDefault() , out RTSPUri result ) ? args.FirstOrDefault() : string.Empty;
            
            _sinkOption = args.Any( x => x == "-sink" );
        }






        public string UriOption
        {
            get => _uriOption;
        }

        public bool SinkOption
        {
            get => _sinkOption;
        }






        public bool CanShowHelp()
        {
            return ! _args.Any() || string.IsNullOrWhiteSpace( _uriOption );
        }

        public void ShowHelp()
        {
            string processName = Assembly.GetExecutingAssembly().GetName().Name + ".exe";

            Console.WriteLine();
            Console.WriteLine( "Receiving packet from a rtsp source " );
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