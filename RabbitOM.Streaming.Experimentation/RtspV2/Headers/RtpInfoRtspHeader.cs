using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class RtpInfoRtspHeader : RtspHeader 
    {
        public const string TypeName = "RTP-Info";



        
        public string Url { get; set; }
        public long Sequence { get; set; }
        public long Time { get; set; }


        public override bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( Url ) 
                && Uri.IsWellFormedUriString( Url , UriKind.RelativeOrAbsolute )
                && Sequence >= 0
                && Time >= 0
                ;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }




        public static bool TryParse( string value , out RtpInfoRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
