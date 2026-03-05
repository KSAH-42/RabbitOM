using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Parsers;

    public sealed class RtpInfo 
    { 
        private RtpInfo()
        {
        }

        public RtpInfo( string url , long? rtpTime , long? sequence , string ssrc )
        {
            if ( string.IsNullOrWhiteSpace( url ) )
            {
                throw new ArgumentException( nameof( url ) );
            }
           
            Url = url.Trim();
            SSRC = ssrc?.Trim() ?? string.Empty;
            Sequence = sequence;
            RtpTime = rtpTime;
        }







        public string Url { get; private set; } = string.Empty;
        
        public string SSRC { get; private set; } = string.Empty;
        
        public long? RtpTime { get; private set; }
        
        public long? Sequence { get; private set; }
        

        




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
        
        
        

        
        
        public static bool TryParse( string input , out RtpInfo result )
        {
            result = null;

            if ( StringRtspHeaderParser.TryParse( input , ";" , out var tokens ) )
            {
                var header = new RtpInfo();

                var comparer = StringComparer.OrdinalIgnoreCase;

                foreach ( var token in tokens )
                {
                    if ( RtspHeaderProperty.TryParse( token , "=" , out var parameter ) )
                    {
                        if ( comparer.Equals( "url" , parameter.Name ) )
                        {
                            header.Url = parameter.Value;
                        }
                        else if ( comparer.Equals( "ssrc" , parameter.Name ) )
                        {
                            header.SSRC = parameter.Value;
                        }
                        else if ( comparer.Equals( "seq" , parameter.Name ) )
                        {
                            if ( long.TryParse( parameter.Value , out var value ) )
                            {
                                header.Sequence = value;
                            }
                        }
                        else if ( comparer.Equals( "rtptime" , parameter.Name ) )
                        {
                            if ( long.TryParse( parameter.Value , out var value ) )
                            {
                                header.RtpTime = value;
                            }
                        }
                    }
                }

                if ( ! string.IsNullOrWhiteSpace( header.Url )  || header.Sequence.HasValue || header.RtpTime.HasValue || ! string.IsNullOrWhiteSpace( header.SSRC ) )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
