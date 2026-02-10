using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class RetryAfterRtspHeader : RtspHeader 
    {
        public const string TypeName = "Retry-After";
        
        public DateTimeOffset? Date { get; set; }
        public TimeSpan? Delta { get; set; }

        public override bool TryValidate()
        {
            return Date.HasValue || Delta.HasValue;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }


        public static bool TryParse( string value , out RetryAfterRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
