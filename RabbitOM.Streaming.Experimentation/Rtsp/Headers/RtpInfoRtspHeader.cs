using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Formatting;

    public sealed class RtpInfoRtspHeader
    {
        public static readonly string TypeName = "RTP-Info";





        public string Url { get; private set; } = string.Empty;
        
        public string SSRC { get; private set; } = string.Empty;

        public long? Sequence { get; set; }
        
        public long? RtpTime { get; set; }
        





        public static bool TryParse( string input , out RtpInfoRtspHeader result )
        {
            result = null;

            input = RtspValueNormalizer.Normalize( input );

            if ( StringRtspHeaderParser.TryParse( input , ';' , out var tokens ) )
            {
                result = new RtpInfoRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( StringParameterRtspHeaderParser.TryParse( token , '=' , out var parameter ) )
                    {
                        if ( string.Equals( "url" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            result.SetUrl( parameter.Value );
                        }
                        else if ( string.Equals( "seq" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            result.SetSequence( parameter.Value );
                        }
                        else if ( string.Equals( "sequence" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            result.SetSequence( parameter.Value );
                        }
                        else if ( string.Equals( "rtptime" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            result.SetRtpTime( parameter.Value );
                        }
                        else if ( string.Equals( "ssrc" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            result.SetSSRC( parameter.Value );
                        }
                    }
                }
            }

            return result != null;
        }






        public void SetUrl( string value )
        {
            Url = RtspValueNormalizer.Normalize( value );
        }

        public void SetSSRC( string value )
        {
            SSRC = RtspValueNormalizer.Normalize( value );
        }
        
        public void SetSequence( string value )
        {
            Sequence = long.TryParse( RtspValueNormalizer.Normalize( value ) , out var result ) 
                ? new long?( result ) 
                : null
                ;
        }

        public void SetRtpTime( string value )
        {
            RtpTime = long.TryParse( RtspValueNormalizer.Normalize( value ) , out var result ) 
                ? new long?( result ) 
                : null
                ;
        }
        
        public override string ToString()
        {
            var builder = new StringBuilder();

            if ( ! string.IsNullOrWhiteSpace( Url ) )
            {
                builder.AppendFormat( Url.Contains( " " ) ? "url=\"{0}\";" : "url={0};" , Url );
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
                builder.AppendFormat( SSRC.Contains( " " ) ? "ssrc=\"{0}\";" : "ssrc={0};" , SSRC );
            }

            return builder.ToString().Trim( ' ' , ';' );
        }
    }
}
