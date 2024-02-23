using System;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Net.Rtsp.Tests.ConsoleApp
{
    public sealed class CommandLines
    {
        private static readonly string[] DefaultHelpOptions = new string[]
        {
            "/?",
            "-?",
            "/help",
            "-help",
        };




        private readonly string[] _args;





        public CommandLines( string[] args )
        {
            _args = args ?? throw new ArgumentNullException( nameof( args ) );
        }





        public string UriOption
        {
            get => _args.FirstOrDefault() ?? string.Empty;
        }







        public bool CanShowHelp()
        {
            return ! _args.Any() || _args.Intersect( DefaultHelpOptions ).Count() > 0;
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