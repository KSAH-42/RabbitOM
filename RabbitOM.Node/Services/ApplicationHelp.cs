using System;
using System.Reflection;

namespace RabbitOM.Node.Services
{
    public static class ApplicationHelp
	{
        public static void ShowHelp( Exception exception = null )
        {
            var processName = Assembly.GetExecutingAssembly().GetName().Name + ".exe";

            Console.WriteLine( $"Receiving packets from a Rtsp source" );
            Console.WriteLine();
            Console.WriteLine( "Usage: " );
            Console.WriteLine();
            Console.WriteLine( $"{processName} rtsp://127.0.0.1/toy.mp4" );
            Console.WriteLine( $"{processName} rtsp://admin:camera123@127.0.0.1/toy.mp4" );
            Console.WriteLine( $"{processName} rtsp://127.0.0.1:554/toy.mp4" );
            Console.WriteLine( $"{processName} rtsp://admin:camera123@127.0.0.1:554/toy.mp4" );
            Console.WriteLine();

            if ( exception != null )
            {
                Console.WriteLine( "Details:" );
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine( exception );
                Console.ResetColor();
            }
        }
	}
}
