using System;
using System.Linq;
using System.Reflection;

namespace RabbitOM.NodeShell.Services
{
    public static class ApplicationHelp
	{
        public static bool CanShowHelp( string[] args )
        {
            if ( args == null || args.Length == 0 )
            {
                return false;
            }

            return args.Any( arg => arg == "/?" || arg == "-h" || arg == "--help" );
        }

        public static void ShowHelp( Exception exception = null )
        {
            var processName = Assembly.GetExecutingAssembly().GetName().Name + ".exe";

            Console.ResetColor();
            Console.WriteLine( $"RTSP Node" );
            Console.WriteLine();
            Console.WriteLine( "Usages: " );
            Console.WriteLine();
            Console.WriteLine( $"{processName} rtsp://admin:camera123@127.0.0.1/toy.mp4" );
            Console.WriteLine( $"{processName} rtsp://admin:camera123@127.0.0.1/toy.mp4 -s workflow.yml" );
            Console.WriteLine();
            Console.WriteLine("Arguments:");
            Console.WriteLine();
            Console.WriteLine("-s\t[optional] the yaml file containing client handlers code");
            Console.WriteLine();

            if ( exception != null )
            {
                Console.WriteLine( "ERROR:" );
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine( exception );
                Console.ResetColor();
            }
        }
	}
}
