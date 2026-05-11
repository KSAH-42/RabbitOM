using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public sealed class RtpInfo
    { 
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        



        private string _url = string.Empty;
        private string _ssrc = string.Empty;
        private ushort? _sequence;
        private ushort? _rtpTime;




        public string Url
        {
            get => _url;
            set => _url = EnsureValue( value );
        }

        public string SSRC
        {
            get => _ssrc;
            set => _ssrc = EnsureValue( value );
        }
        
        public ushort? Sequence
        {
            get => _sequence;
            set => _sequence = value;
        }
        
        public ushort? RtpTime
        {
            get => _rtpTime;
            set => _rtpTime = value;
        }




        private static string EnsureValue( string value )
        {
            var result = RtspHeaderValueSanitizer.UnQuotesWithTrim( value );

            RtspHeaderValueValidator.EnsureWellFormedToken( result );
            RtspHeaderValueValidator.EnsureNotNullOrWhiteSpace( result );

            return result;
        }

        public static bool TryParse( string input , out RtpInfo result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var info = new RtpInfo();

                foreach ( var token in tokens )
                {
                    if ( RtspHeaderValueParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                    {
                        if ( ValueComparer.Equals( "url" , parameter.Key ) )
                        {
                            info.Url = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "ssrc" , parameter.Key ) )
                        {
                            info.SSRC = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "seq" , parameter.Key ) )
                        {
                            if ( ushort.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value ) , out var value ) )
                            {
                                info.Sequence = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "rtptime" , parameter.Key ) )
                        {
                            if ( ushort.TryParse( RtspHeaderValueSanitizer.UnQuotesWithTrim( parameter.Value ) , out var value ) )
                            {
                                info.RtpTime = value;
                            }
                        }
                    }
                }

                if ( string.IsNullOrWhiteSpace( info.Url ) )
                {
                    return false;
                }

                result = info;
            }

            return result != null;
        }







        public override string ToString()
        {
            var builder = new StringBuilder();

            if ( ! string.IsNullOrWhiteSpace( Url ) )
            {
                builder.AppendFormat( "url={0};" , Url );
            }

            if ( Sequence.HasValue )
            {
                builder.AppendFormat( "seq={0};" , Sequence );
            }

            if ( RtpTime.HasValue )
            {
                builder.AppendFormat( "rtptime={0};" , RtpTime );
            }

            if ( ! string.IsNullOrWhiteSpace( SSRC ) )
            {
                builder.AppendFormat( "ssrc={0};" );
            }

            return builder.ToString().Trim( ' ' , ';' );
        }
    }
}
