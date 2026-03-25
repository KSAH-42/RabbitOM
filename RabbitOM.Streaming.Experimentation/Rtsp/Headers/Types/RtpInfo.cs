using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Normalizers;
    
    public sealed class RtpInfo : RtspHeaderValue
    { 
        private static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        private static readonly StringValueNormalizer ValueNormalizer = StringValueNormalizer.TrimWithUnQuoteNormalizer;

        public RtpInfo( string url ) : this( url , null , null , null ) { }

        public RtpInfo( string url , ushort sequence ) : this( url , sequence , null , null ) { }
        
        public RtpInfo( string url , ushort sequence , ushort rtpTime ) : this( url , sequence , rtpTime , null ) { }

        public RtpInfo( string url , ushort? sequence , ushort? rtpTime , string ssrc )
        {
            if ( ! RtspHeaderProtocolValidator.IsValidUri( url ) )
            {
                throw new ArgumentException( nameof( url ) );
            }

            if ( ! string.IsNullOrEmpty( ssrc ) && ! RtspHeaderProtocolValidator.IsValidToken( ssrc ) )
            {
                throw new ArgumentException( nameof( url ) );
            }

            Url = ValueNormalizer.Normalize( url );
            SSRC = ValueNormalizer.Normalize( ssrc );
            Sequence = sequence;
            RtpTime = rtpTime;
        }


        public string Url { get; }

        public string SSRC { get; }
        
        public ushort? Sequence { get; }
        
        public ushort? RtpTime { get; }


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
                            url = ValueNormalizer.Normalize( parameter.Value );
                        }
                        else if ( ValueComparer.Equals( "ssrc" , parameter.Key ) )
                        {
                            ssrc = ValueNormalizer.Normalize( parameter.Value );
                        }
                        else if ( ValueComparer.Equals( "seq" , parameter.Key ) )
                        {
                            if ( ushort.TryParse( ValueNormalizer.Normalize( parameter.Value ) , out var value ) )
                            {
                                seq = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "rtptime" , parameter.Key ) )
                        {
                            if ( ushort.TryParse( ValueNormalizer.Normalize( parameter.Value ) , out var value ) )
                            {
                                rtpTime = value;
                            }
                        }
                    }
                }

                if ( ! RtspHeaderProtocolValidator.IsValidUri( url ) )
                {
                    return false;
                }

                if ( ! string.IsNullOrEmpty( ssrc ) && ! RtspHeaderProtocolValidator.IsValidToken( ssrc ) )
                {
                    return false;
                }

                result = new RtpInfo( url , seq , rtpTime , ssrc );
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
