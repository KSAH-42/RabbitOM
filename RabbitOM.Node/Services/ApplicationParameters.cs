using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace RabbitOM.Node.Services
{
    public sealed class ApplicationParameters
    {
        public string Uri { get; set; }

        public string ScriptWorkflow { get; set; }



        public void Validate()
        {
            if ( string.IsNullOrWhiteSpace( Uri ) )
            {
                throw new ValidationException( "the uri is null or empty or white space" );
            }

            if ( ! System.Uri.TryCreate( Uri , UriKind.Absolute , out var uri ) )
            {
                throw new ValidationException( "the uri is not well formed" );
            }

            if ( uri.Scheme != "rtsp" )
            {
                throw new ValidationException( "the scheme must be rtsp" );
            }
        }


        public static bool CanParse( string[] input )
        {
            return input?.Length >= 1;
        }

        public static ApplicationParameters Parse( string[] input )
        {
            if ( input == null )
            {
                throw new ArgumentNullException( nameof( input ) , "no input is provided" );
            }

            if ( input.Length < 1 )
            {
                throw new ArgumentException( "no input is provided" , nameof( input ) );
            }

            var result = new ApplicationParameters()
            {
                Uri = input[0],
            };

            for ( var i = 1 ; i < input.Length ; ++ i )
            {
                if ( input[ i ] == "-s" || input[ i ] == "--script" )
                {
                    if ( ! string.IsNullOrEmpty( result.ScriptWorkflow ) )
                    {
                        throw new ParseException( "duplicated parameter: " + input[i] );
                    }

                    result.ScriptWorkflow = input.ElementAtOrDefault( ++ i );
                }
            }

            return result;
        }
    }
}