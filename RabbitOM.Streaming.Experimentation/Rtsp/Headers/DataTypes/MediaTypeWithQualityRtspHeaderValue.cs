using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class MediaTypeWithQualityRtspHeaderValue
    {
        public MediaTypeWithQualityRtspHeaderValue( string value )
            : this( value , null , null , new StringParameterRtspHeaderValueCollection() )
        {
        }

        public MediaTypeWithQualityRtspHeaderValue( string value , double quality )
            : this( value , quality , null , new StringParameterRtspHeaderValueCollection() )
        {
        }

        public MediaTypeWithQualityRtspHeaderValue( string value , double quality , string charset )
            : this( value , quality , charset , new StringParameterRtspHeaderValueCollection() )
        {
        }

        public MediaTypeWithQualityRtspHeaderValue( string value , double? quality , string charset , StringParameterRtspHeaderValueCollection parameters )
        {
            RtspHeaderValueValidator.EnsureWellFormedTokenIfAll( value , x => x == '/' );
            RtspHeaderValueValidator.EnsureLettersOrDigits( value );
            RtspHeaderValueValidator.EnsureAny( value , (x,i) => x == '/' && i > 0 && i < value.Length );
            RtspHeaderValueValidator.EnsureNotNull( parameters );

            Value = value;
            Quality = quality;
            CharSet = string.IsNullOrEmpty( charset ) ? string.Empty : RtspHeaderValueValidator.EnsureWellFormedToken( charset );
            Parameters = parameters;
        }




        public string Value { get; }

        public double? Quality { get; }
        
        public string CharSet { get; }

        public StringParameterRtspHeaderValueCollection Parameters { get; }



        
        public static bool TryParse( string input , out MediaTypeWithQualityRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var name = RtspHeaderValueSanitizer.UnQuotesWithTrim( tokens.FirstOrDefault( token => ! token.Contains( "=" ) ) );

                var boolIsValid =  RtspHeaderValueValidator.TryEnsureWellFormedTokenIfAll( name , x => x == '/' ) 
                                && RtspHeaderValueValidator.TryEnsureLettersOrDigits( name )
                                && RtspHeaderValueValidator.TryEnsureAny( name , (x,i) => x == '/' && i > 0 && i < name.Length )
                                ;

                if ( boolIsValid )
                {
                    var parameters = new StringParameterRtspHeaderValueCollection();
                    
                    var charset = string.Empty;
                    
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
                            if ( StringComparer.OrdinalIgnoreCase.Equals( "charset" , parameter.Key )  )
                            {
                                if ( string.IsNullOrEmpty( charset ) )
                                {
                                    charset = RtspHeaderValueSanitizer.TrimWithRemoveAllQuotes( parameter.Value );
                                }
                            }
                            else
                            {
                                if ( StringParameterRtspHeaderValue.TryCreate( parameter.Key , parameter.Value , out var optionalParameter ) )
                                {
                                    parameters.TryAdd( optionalParameter );
                                }
                            }
                        }
                    }

                    result = new MediaTypeWithQualityRtspHeaderValue( name , quality , charset , parameters );
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

            if ( ! string.IsNullOrEmpty( CharSet ) )
            {
                builder.AppendFormat( "charset={0}; " , CharSet );
            }

            foreach ( var parameter in Parameters )
            {
                builder.AppendFormat( "{0}={1}; " , parameter.Name , parameter.Value );
            }

            return builder.ToString().Trim( ' ' , ';' );
        }
    }
}
