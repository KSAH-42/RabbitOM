using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Validation;

    public sealed class RtpInfo 
    { 
        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.TrimWithUnQuoteAdapter;


        public RtpInfo( string url , ushort? rtpTime , ushort? sequence , string ssrc )
        {
            if ( ! StringValueValidator.UriValidator.TryValidate( url ) )
            {
                throw new ArgumentException( nameof( url ) );
            }

            if ( ! string.IsNullOrEmpty( ssrc ) && ! StringValueValidator.TokenValidator.TryValidate( ssrc ) )
            {
                throw new ArgumentException( nameof( url ) );
            }

            Url = ValueAdapter.Adapt( url );
            SSRC = ValueAdapter.Adapt( ssrc );
            RtpTime = rtpTime;
            Sequence = sequence;
        }


        public string Url { get; private set; } = string.Empty;

        public string SSRC { get; private set; } = string.Empty;
        
        public ushort? RtpTime { get; private set; }
        
        public ushort? Sequence { get; private set; }


        public static bool TryParse( string input , out RtpInfo result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , ";" , out string[] tokens ) )
            {
                string url = "";
                string ssrc = "";
                ushort? rtpTime = null;
                ushort? seq = null;

                foreach ( var token in tokens )
                {
                    if ( RtspHeaderParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                    {
                        if ( ValueComparer.Equals( "url" , parameter.Key ) )
                        {
                            url = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "ssrc" , parameter.Key ) )
                        {
                            ssrc = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "rtptime" , parameter.Key ) )
                        {
                            if ( ushort.TryParse( ValueAdapter.Adapt( parameter.Value ) , out var value ) )
                            {
                                rtpTime = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "seq" , parameter.Key ) )
                        {
                            if ( ushort.TryParse( ValueAdapter.Adapt( parameter.Value ) , out var value ) )
                            {
                                seq = value;
                            }
                        }
                    }
                }

                if ( ! StringValueValidator.UriValidator.TryValidate( url ) )
                {
                    return false;
                }

                if ( ! string.IsNullOrEmpty( ssrc ) && ! StringValueValidator.TokenValidator.TryValidate( ssrc ) )
                {
                    return false;
                }

                result = new RtpInfo( url , rtpTime , seq , ssrc );
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
