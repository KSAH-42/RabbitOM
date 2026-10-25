using System;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Node.Application
{
    public static class HelpManager
    {
        public static bool CanShowHelp( string[] args )
        {
            if ( args == null || args.Length == 0 )
            {
                return true;
            }

            return args.Any( arg => arg == "/?" || arg == "-h" || arg == "--help" );
        }

        public static void ShowHelp( Exception exception = null )
        {
            var processName = Assembly.GetExecutingAssembly().GetName().Name + ".exe";

            Console.ResetColor();
            Console.WriteLine( "Arguments:");
            Console.WriteLine();

            foreach( var property in typeof(ApplicationSettings).GetProperties() )
            {
                Console.WriteLine( $"{property.Name}" );

                foreach ( var option in property.GetCustomAttributes<OptionAttribute>() )
                {
                    var prefix = string.IsNullOrWhiteSpace( option.Name ) ? "" : string.Format( "   {0,-20}" , option.Name );

                    Console.WriteLine( "{0} {1} {2}" , prefix , option.Help , option.IsRequired ? "[required]" : "" );
                }

                Console.WriteLine();
            }

            if ( exception != null )
            {
                Console.WriteLine( "ERROR:" );
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine( exception );
            }

            Console.ResetColor();
        }
    }
}