using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: on getter, implement and use the method ElementOrDefaultAt( string name ) and the method ElementOrDefaultAt( string name , int index )
    // TODO: on setter do not throw exception on null, prefer removing from the internal collection
    public class RtspRequestHeaderCollection : RtspHeaderCollection
    {
        public AcceptRtspHeaderValue Accept { get; set; }
        
        public AcceptEncodingRtspHeaderValue AcceptEncoding { get; set; }
        
        public AcceptLanguageRtspHeaderValue AcceptLanguage { get; set; }
        
        public AuthorizationRtspHeaderValue Authorization { get; set; }
        
        public BandwidthRtspHeaderValue Bandwidth { get; set; }
        
        public BlockSizeRtspHeaderValue BlockSize { get; set; }
        
        public CacheControlRtspHeaderValue CacheControl { get; set; }
        
        public ConferenceRtspHeaderValue Conference { get; set; }
        
        public ConnectionRtspHeaderValue Connection { get; set; }
        
        public CSeqRtspHeaderValue CSeq { get; } = new CSeqRtspHeaderValue();
        
        public DateRtspHeaderValue Date { get; set; }
        
        public ExpiresRtspHeaderValue Expires { get; set; }
        
        public IfMatchRtspHeaderValue IfMatch { get; set; }
        
        public IfModifiedSinceRtspHeaderValue IfModifiedSince { get; set; }
        
        public MaxForwardsRtspHeaderValue MaxForwards { get; set; }
        
        public PublicRtspHeaderValue Public { get; set; }
        
        public RefererRtspHeaderValue Referer { get; set; }
        
        public ScaleRtspHeaderValue Scale { get; set; }
        
        public SessionRtspHeaderValue Session { get; set; }
        
        public SpeedRtspHeaderValue Speed { get; set; }
        
        public TransportRtspHeaderValue Transport { get; set; }
        
        public UserAgentRtspHeaderValue UserAgent { get; set; }
        
        public ViaRtspHeaderValue Via { get; set; }

        public WarningRtspHeaderValue Warning { get; set; }
    }
}
