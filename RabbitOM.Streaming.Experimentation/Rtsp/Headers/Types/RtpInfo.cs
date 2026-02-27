using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RtpInfo 
    { 
        private RtpInfo()
        {
        }

        public RtpInfo( string url , long? rtpTime , long? sequence , string ssrc )
        {
            if ( string.IsNullOrWhiteSpace( url ) )
            {
                throw new ArgumentNullException( nameof( url ) );
            }

            if ( ! StringRtspHeaderNormalizer.CheckValue( url ) )
            {
                throw new ArgumentException( nameof( url ) );
            }

            if ( ! string.IsNullOrWhiteSpace( url ) )
            {
                if ( ! StringRtspHeaderNormalizer.CheckValue( ssrc) )
                {
                    throw new ArgumentException( nameof( ssrc ) );
                }
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

            if ( StringRtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , ";" , out var tokens ) )
            {
                var header = new RtpInfo();

                foreach ( var token in tokens )
                {
                    if ( StringParameter.TryParse( token , "=" , out var parameter ) )
                    {
                        if ( string.Equals( "url" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.Url = parameter.Value;
                        }
                        else if ( string.Equals( "ssrc" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            header.SSRC = parameter.Value;
                        }
                        else if ( string.Equals( "seq" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
                        {
                            if ( long.TryParse( parameter.Value , out var value ) )
                            {
                                header.Sequence = value;
                            }
                        }
                        else if ( string.Equals( "rtptime" , parameter.Name , StringComparison.OrdinalIgnoreCase ) )
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
