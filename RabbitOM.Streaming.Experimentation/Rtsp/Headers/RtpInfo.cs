using System;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Adapters;
    using System.Collections.Generic;

    public sealed class RtpInfo 
    { 
        public static readonly StringComparer ValueComparer = StringComparer.OrdinalIgnoreCase;
        public static readonly StringValueAdapter ValueAdapter = StringValueAdapter.UnQuoteAdapter;



        private RtpInfo() { }

        public RtpInfo( string url , long? rtpTime , long? sequence , string ssrc )
        {
            if ( string.IsNullOrWhiteSpace( url ) )
            {
                throw new ArgumentException( nameof( url ) );
            }
           
            Url = ValueAdapter.Adapt( url );
            SSRC = ValueAdapter.Adapt( ssrc );
            Sequence = sequence;
            RtpTime = rtpTime;
        }



        public string Url { get; private set; } = string.Empty;

        public string SSRC { get; private set; } = string.Empty;
        
        public long? Sequence { get; private set; }
        
        public long? RtpTime { get; private set; }




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

            if ( RtspHeaderParser.TryParse( input , ";" , out string[] tokens ) )
            {
                var header = new RtpInfo();

                foreach ( var token in tokens )
                {
                    if ( RtspHeaderParser.TryParse( token , "=" , out KeyValuePair<string,string> parameter ) )
                    {
                        if ( ValueComparer.Equals( "url" , parameter.Key ) )
                        {
                            header.Url = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "ssrc" , parameter.Key ) )
                        {
                            header.SSRC = parameter.Value;
                        }
                        else if ( ValueComparer.Equals( "seq" , parameter.Key ) )
                        {
                            if ( long.TryParse( ValueAdapter.Adapt( parameter.Value ) , out long value ) )
                            {
                                header.Sequence = value;
                            }
                        }
                        else if ( ValueComparer.Equals( "rtptime" , parameter.Key ) )
                        {
                            if ( long.TryParse( ValueAdapter.Adapt( parameter.Value ) , out long value ) )
                            {
                                header.RtpTime = value;
                            }
                        }
                    }
                }

                if ( ! string.IsNullOrWhiteSpace( header.Url ) )
                {
                    result = header;
                }
            }

            return result != null;
        }
    }
}
