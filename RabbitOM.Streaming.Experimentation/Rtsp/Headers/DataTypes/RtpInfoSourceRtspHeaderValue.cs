using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.DataTypes
{
    public sealed class RtpInfoSourceRtspHeaderValue
    {
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;




        private string _url = string.Empty;
        private string _ssrc = string.Empty;




        public string Url
        {
            get => _url;
            set => _url = RtspHeaderValueValidator.EnsureWellFormedToken( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public string SSRC
        {
            get => _ssrc;
            set => _ssrc = RtspHeaderValueValidator.EnsureWellFormedTokenOrEmpty( RtspHeaderValueSanitizer.UnQuotesWithTrim( value ) );
        }

        public ushort? Sequence
        {
            get;
            set;
        }

        public ushort? RtpTime
        {
            get;
            set;
        }




        public static bool TryParse( string input , out RtpInfoSourceRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderValueParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var info = new RtpInfoSourceRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( RtspHeaderValueParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                    {
                        if ( ValueComparer.Equals( "url" , parameter.Key ) )
                        {
                            info._url = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "ssrc" , parameter.Key ) )
                        {
                            info._ssrc = parameter.Value;
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
                builder.AppendFormat( "ssrc={0};" , SSRC );
            }

            return builder.ToString().Trim( ' ' , ';' );
        }
    }
}
