using System;
using System.Linq;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class StringRtspHeader
    {
        public static readonly StringRtspHeader Empty = new StringRtspHeader( string.Empty , null );





        private StringRtspHeader( string name , double? quality )
        {
            Name = name;
            Quality = quality;
        }





        public string Name
        {
            get;
        }

        public double? Quality
        {
            get;
        }




        public static StringRtspHeader NewString( string value )
        {
            return NewString( value , null );
        }

        public static StringRtspHeader NewString( string value , double? quality )
        {
            return new StringRtspHeader( RtspValueNormalizer.Normalize( value ) , quality );
        }





        public static bool IsNullOrEmpty( StringRtspHeader value )
        {
            return value == null || string.IsNullOrWhiteSpace( value.Name ) || object.ReferenceEquals( value , StringRtspHeader.Empty );
        }

        




        public static bool TryParse( string input , out StringRtspHeader result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( RtspValueNormalizer.Normalize( input , "\'" , "\"" ) , ';' , out var tokens ) )
            {
                double? quality = null;

                foreach ( var token in tokens.Skip( 1 ) )
                {
                    if ( StringParameterRtspHeaderParser.TryParse( token , '=' , out var parameter ) )
                    {
                        if ( ! string.Equals( "q" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            continue;
                        }
                        
                        if ( ! double.TryParse( parameter.Value , NumberStyles.Any , CultureInfo.InvariantCulture , out var parameterValue ) )
                        {
                            continue;
                        }

                        quality = parameterValue;
                        break;
                    }
                }

                result = NewString( tokens.FirstOrDefault() , quality );
            }

            return result != null;
        }





        public override string ToString()
        {
            if ( string.IsNullOrWhiteSpace( Name ) )
            {
                return string.Empty;
            }

            if ( ! Quality.HasValue )
            {
                return Name;
            }

            return string.Format( CultureInfo.InvariantCulture, "{0}; q={1:F1}" , Name , Quality );
        }
    }
}
