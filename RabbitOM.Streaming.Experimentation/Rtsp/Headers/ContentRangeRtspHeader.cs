using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class ContentRangeRtspHeader : RtspHeader 
    {
        public const string TypeName = "Content-Range";
        
        
        public long? From { get; set; }
        public bool HasLength { get; set; }
        public bool HasRange { get; set; }
        public long? Length { get; set; }
        public long? To { get; set; }
        public string Unit { get; set; }

        
        public override bool TryValidate() 
            => throw new NotImplementedException();


        public override string ToString()
        {
            throw new NotImplementedException();
        }
        public static ContentRangeRtspHeader Parse( string value )
        {
            throw new NotImplementedException();
        }

        public static bool TryParse( string value , out ContentRangeRtspHeader result )
        {
            throw new NotImplementedException();
        }
    }
}
