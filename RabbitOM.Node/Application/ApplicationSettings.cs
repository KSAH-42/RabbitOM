using System;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Node.Application
{
    public sealed class ApplicationSettings
    {
        [Option( IsRequired = true , Help = "represent the rtsp uri" )]
        public string Uri { get; private set; }

        [Option(Name = "-s", Help = "represent the file name of the script")]
        public string Script { get; private set; }

        [Option(Name = "-l" , Value = false , Help = "disable logging" )]
        [Option(Name = "-L" , Value = true , Help = "enable logging" )]
        [Option(Name = "--disable-logging" , Value = false , Help = "disable logging" )]
        [Option(Name = "--enable-logging" , Value = true , Help = "enable logging" )]
        public bool EnableLogging { get; private set; } = true;

        [Option(Name = "-t" , Help = "receive timeout" )]
        [Option(Name = "--timeout-receive" , Help = "receive timeout" )]
        public TimeSpan ReceiveTimeout { get; private set; } = TimeSpan.FromSeconds( 3 );

        [Option(Name = "-T" , Help = "send timeout" )]
        [Option(Name = "--timeout-send" , Help = "send timeout" )]
        public TimeSpan SendTimeout { get; private set; } = TimeSpan.FromSeconds( 3 );




        public static ApplicationSettings Parse( string[] input )
        {
            if ( input == null )
            {
                throw new ArgumentNullException( nameof( input ) , "no input is provided" );
            }

            if ( input.Length < 1 )
            {
                throw new ArgumentException( "no input is provided" , nameof( input ) );
            }

            var result = new ApplicationSettings()
            {
                Uri = input[0],
            };

            // TODO: refactor and optimize the code, and add the support of IsRequired

            for ( var i = 1 ; i < input.Length ; ++ i )
            {
                var property = typeof( ApplicationSettings )
                    .GetRuntimeProperties()
                    .Where( p => p.GetCustomAttributes<OptionAttribute>().Any( o => o.Name == input[i] ) )
                    .FirstOrDefault()
                    ;

                if ( property != null )
                {
                    var option = property.GetCustomAttributes<OptionAttribute>().First( o => o.Name == input[i] );

                    if ( option.Value != null )
                    {
                        property.SetValue( result , option.Value );
                    }
                    else
                    {
                        var value = input.ElementAtOrDefault( ++ i ) ?? string.Empty;

                        if ( property.PropertyType == typeof( string ) )
                        {
                            property.SetValue( result , value );
                        }
                        else if ( property.PropertyType == typeof( bool ) )
                        {
                            property.SetValue( result , bool.Parse( value ) );
                        }
                        else if ( property.PropertyType == typeof( int ) )
                        {
                            property.SetValue( result , int.Parse( value ) );
                        }
                        else if ( property.PropertyType == typeof( TimeSpan ) )
                        {
                            property.SetValue( result , TimeSpan.Parse( value ) );
                        }
                    }
                }
            }

            return result;
        }
    }
}