using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: move field on the RtspResponseHeaders
    // TODO: fuse collections in order to remove internal collection class by introducing static method , including lambda expression and so on

    public class RtspRequestHeaderCollection : RtspHeaderCollection
    {
        public RtspHeaderValueCollection<StringWithQuality> Accept { get; } = RtspHeaderValueFactory.AcceptRtspHeaderValueCollection();
        
        public RtspHeaderValueCollection<StringWithQuality> AcceptEncoding { get; } = RtspHeaderValueFactory.AcceptEncodingRtspHeaderValueCollection();
        
        public RtspHeaderValueCollection<StringWithQuality> AcceptLanguage { get; } = RtspHeaderValueFactory.AcceptLanguageRtspHeaderValueCollection();
        
        public RtspHeaderValueCollection<RtspMethod> Allow { get; } = RtspHeaderValueFactory.AllowRtspHeaderValueCollection();
        
        public AuthorizationRtspHeaderValue Authorization { get; set; }
        
        public UIntRtspHeaderValue Bandwidth { get; set; }
        
        public UShortRtspHeaderValue BlockSize { get; set; }
        
        public CacheControlRtspHeaderValue CacheControl { get; set; }
        
        public ConferenceRtspHeaderValue Conference { get; set; }
        
        public RtspHeaderValueCollection<string> Connection { get; } = RtspHeaderValueFactory.ConnectionRtspHeaderValueCollection();
        
        public ContentBaseRtspHeaderValue ContentBase { get; set; }
        
        public ContentRangeRtspHeaderValue ContentRange { get; set; }
        
        public UIntRtspHeaderValue CSeq { get; } = new UIntRtspHeaderValue();
        
        public DateTimeRtspHeaderValue Date { get; set; }
        
        public DateTimeRtspHeaderValue Expires { get; set; }
        
        public RtspHeaderValueCollection<string> IfMatch { get; } = RtspHeaderValueFactory.IfMatchRtspHeaderValueCollection();
        
        public DateTimeRtspHeaderValue IfModifiedSince { get; set; }
        
        public DateTimeRtspHeaderValue LastModified { get; set; }
        
        public UriRtspHeaderValue Location { get; set; }
        
        public UIntRtspHeaderValue MaxForwards { get; set; }
        
        public DoubleRtspHeaderValue MediaDuration { get; set; }
        
        public RtspHeaderValueCollection<RtspMethod> Public { get; } = RtspHeaderValueFactory.PublicRtspHeaderValueCollection();
        
        public UriRtspHeaderValue Referer { get; set; }
        
        public RtspHeaderValueCollection<RtpInfo> RtpInfo { get; } = RtspHeaderValueFactory.NewRtpInfoRtspHeaderValueCollection();
        
        public FloatRtspHeaderValue Scale { get; set; }
        
        public SessionRtspHeaderValue Session { get; set; }
        
        public DoubleRtspHeaderValue Speed { get; set; }
        
        public TransportRtspHeaderValue Transport { get; set; }
        
        public UserAgentRtspHeaderValue UserAgent { get; set; }
        
        public RtspHeaderValueCollection<ProxyInfo> Via { get; } = RtspHeaderValueFactory.ViaRtspHeaderValueCollection();
        
        public WWWAuthenticateRtspHeaderValue WWWAuthenticate { get; set; }
    }
}
