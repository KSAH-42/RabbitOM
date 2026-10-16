using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RabbitOM.Node.Application
{
    public sealed class ApplicationSettings
    {
        [Option(IsRequired = true, Help = "represent the rtsp uri" )]
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



        // refactoring as O(n) see previous comit of the parse method
        // TODO: add the support of IsRequired
        // RabbiOM.Node https://www.youtube.com/watch?v=ENdpvW1otMQ&t=7196s --script spying@curie.xml --disable-logging

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

            var properties = new Dictionary<string,PropertyInfo>();

            foreach ( var property in typeof( ApplicationSettings ).GetRuntimeProperties() )
            {
                properties[ property.Name ] = property;
            }

            var options = new List<OptionAttribute>();
            var indexes = new Dictionary<int,PropertyInfo>();

            // O(n+m) => O(n)
            foreach ( var prop in properties.Values )
            {
                indexes[ options.Count ] = prop;
                options.AddRange( prop.GetCustomAttributes<OptionAttribute>() );
            }

            var selectedProperties = new Dictionary<string,Tuple<PropertyInfo,OptionAttribute>>();
            var items = input.ToHashSet();
            var index = -1;

            for ( var i = 0; i < options.Count; ++ i )
            {
                var option = options[ i ];

                if ( indexes.ContainsKey( i ) )
                {
                    index = i;
                }

                if ( indexes.TryGetValue( index , out var property ) && items.Contains( option.Name ) )
                {
                    selectedProperties[ option.Name ] = new Tuple<PropertyInfo, OptionAttribute>( property , option );
                }
            }

            index = -1;
            while ( ++ index < input.Length )
            {
                if ( selectedProperties.TryGetValue( input[ index ] , out var selectedProperty ) )
                {
                    var (property,option) = selectedProperty;

                    if ( option.Value != null )
                    {
                        property.SetValue( result , option.Value );
                    }
                    else
                    {
                        var value = input.ElementAtOrDefault( ++ index ) ?? string.Empty;

                        if ( value.StartsWith( "-" ) )
                        {
                            throw new InvalidOperationException( $"waiting a value without starting with a dash: {value}" );
                        }

                        property.SetValue( result , PropertyValueConverter.Convert( selectedProperty.Item1.PropertyType , value ) );
                    }
                }
            }

            return result;
        }













        //public static ApplicationSettings Parse( string[] input )
        //{
        //    if ( input == null )
        //    {
        //        throw new ArgumentNullException( nameof( input ) , "no input is provided" );
        //    }

        //    if ( input.Length < 1 )
        //    {
        //        throw new ArgumentException( "no input is provided" , nameof( input ) );
        //    }

        //    var result = new ApplicationSettings()
        //    {
        //        Uri = input[0],
        //    };


        //    // TODO: refactor as O(n) and move the code to a different class

        //    for ( var i = 1 ; i < input.Length ; ++ i )
        //    {
        //        var property = typeof( ApplicationSettings )
        //            .GetRuntimeProperties()
        //            .Where( p => p.GetCustomAttributes<OptionAttribute>().Any( o => o.Name == input[i] ) )
        //            .FirstOrDefault()
        //            ;

        //        if ( property != null )
        //        {
        //            var option = property.GetCustomAttributes<OptionAttribute>().First( o => o.Name == input[i] );

        //            if ( option.Value != null )
        //            {
        //                property.SetValue( result , option.Value );
        //            }
        //            else
        //            {
        //                var value = input.ElementAtOrDefault( ++ i ) ?? string.Empty;

        //                if ( property.PropertyType == typeof( string ) )
        //                {
        //                    property.SetValue( result , value );
        //                }
        //                else if ( property.PropertyType == typeof( bool ) )
        //                {
        //                    property.SetValue( result , bool.Parse( value ) );
        //                }
        //                else if ( property.PropertyType == typeof( int ) )
        //                {
        //                    property.SetValue( result , int.Parse( value ) );
        //                }
        //                else if ( property.PropertyType == typeof( TimeSpan ) )
        //                {
        //                    property.SetValue( result , TimeSpan.Parse( value ) );
        //                }
        //            }
        //        }
        //    }

        //    return result;
        //}
    }
}