using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class MediaTypeWithQualityRtspHeaderValue
    {
        public MediaTypeWithQualityRtspHeaderValue( string value )
        {
            RtspHeaderValueValidator.EnsureWellFormedToken( value , x => x == '/' );
            RtspHeaderValueValidator.EnsureLettersOrDigits( value );
            RtspHeaderValueValidator.EnsureAny( value , (x,i) => x == '/' && i > 0 && i < value.Length );

            Value = value;
            Parameters = new StringParameterRtspHeaderValueCollection();
        }

        public MediaTypeWithQualityRtspHeaderValue( string value , double quality )
        {
            RtspHeaderValueValidator.EnsureWellFormedToken( value , x => x == '/' );
            RtspHeaderValueValidator.EnsureLettersOrDigits( value );
            RtspHeaderValueValidator.EnsureAny( value , (x,i) => x == '/' && i > 0 && i < value.Length );
            
            Value = value;
            Quality = quality;
            Parameters = new StringParameterRtspHeaderValueCollection();
        }

        public MediaTypeWithQualityRtspHeaderValue( string value , double? quality , StringParameterRtspHeaderValueCollection parameters )
        {
            RtspHeaderValueValidator.EnsureWellFormedToken( value );
            RtspHeaderValueValidator.EnsureLettersOrDigits( value );
            RtspHeaderValueValidator.EnsureAny( value , (x,i) => x == '/' && i > 0 && i < value.Length );
            RtspHeaderValueValidator.EnsureNotNull( parameters );

            Value = value;
            Quality = quality;
            Parameters = parameters;
        }




        public string Value { get; }

        public double? Quality { get; }
        
        public StringParameterRtspHeaderValueCollection Parameters { get; }



        
        public static bool TryParse( string input , out MediaTypeWithQualityRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var name = RtspHeaderValueSanitizer.UnQuotesWithTrim( tokens.FirstOrDefault( token => ! token.Contains( "=" ) ) );

                var boolIsValid =  RtspHeaderValueValidator.TryEnsureWellFormedToken( name , x => x == '/' ) 
                                && RtspHeaderValueValidator.TryEnsureLettersOrDigits( name )
                                && RtspHeaderValueValidator.TryEnsureAny( name , (x,i) => x == '/' && i > 0 && i < name.Length );

                if ( boolIsValid )
                {
                    var parameters = new StringParameterRtspHeaderValueCollection();
                    
                    double? quality = null;

                    foreach ( var token in tokens.Where( token => token.Contains( "=" ) ) )
                    {
                        if ( RtspHeaderValueParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                        {
                            if ( StringComparer.OrdinalIgnoreCase.Equals( "q" , parameter.Key ) && quality.HasValue == false )
                            {
                                if ( double.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value ).Replace( "," , "." ) , NumberStyles.Float , CultureInfo.InvariantCulture , out var qualityValue ) )
                                {
                                    quality = qualityValue;
                                }
                            }
                            else
                            {
                               parameters.TryAdd( parameter.Key , RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value ) );
                            }
                        }
                    }

                    result = new MediaTypeWithQualityRtspHeaderValue( name , quality , parameters );
                }
            }

            return result != null;
        }
        



        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat( "{0}; " , Value );

            if ( Quality.HasValue )
            {
                builder.AppendFormat( "q={0}; " , Quality.GetValueOrDefault().ToString("0.0##", NumberFormatInfo.InvariantInfo) );
            }

            foreach ( var parameter in Parameters )
            {
                builder.AppendFormat( "{0}={1}; " , parameter.Key , parameter.Value );
            }

            return builder.ToString().Trim( ' ' , ';' );
        }
    }
}
