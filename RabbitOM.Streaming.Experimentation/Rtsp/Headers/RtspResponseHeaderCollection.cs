using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: on getter, implement and use the method ElementOrDefaultAt( string name ) and the method ElementOrDefaultAt( string name , int index )
    // TODO: on setter do not throw exception on null, prefer removing from the internal collection
    public class RtspResponseHeaderCollection : RtspHeaderCollection
    {
        public AllowRtspHeaderValue Allow { get; set; }

        public CacheControlRtspHeaderValue CacheControl { get; set; }
        
        public ConferenceRtspHeaderValue Conference { get; set; }
        
        public ConnectionRtspHeaderValue Connection { get; set; }
        
        public ContentBaseRtspHeaderValue ContentBase { get; set; }
        
        public ContentRangeRtspHeaderValue ContentRange { get; set; }
        
        public CSeqRtspHeaderValue CSeq { get; } = new CSeqRtspHeaderValue();
        
        public DateRtspHeaderValue Date { get; set; }
        
        public ExpiresRtspHeaderValue Expires { get; set; }

        public LastModifiedRtspHeaderValue LastModified { get; set; }
        
        public LocationRtspHeaderValue Location { get; set; }
        
        public MediaDurationRtspHeaderValue MediaDuration { get; set; }
        
        public PublicRtspHeaderValue Public { get; set; }
        
        public RtpInfoRtspHeaderValue RtpInfo { get; set; }
        
        public SessionRtspHeaderValue Session { get; set; }
        
        public TransportRtspHeaderValue Transport { get; set; }
        
        public ViaRtspHeaderValue Via { get; set; }

        public AuthenticateRtspHeaderValue Authenticate { get; set; }
    }
}
