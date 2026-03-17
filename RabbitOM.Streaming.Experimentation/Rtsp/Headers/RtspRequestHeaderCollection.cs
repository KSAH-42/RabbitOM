using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: fuse collections in order to remove internal collection class by introducing static method , including lambda expression and so on
    // TODO: review if theses headers are really present for requesting something to the server

    public class RtspRequestHeaderCollection : RtspHeaderCollection
    {
        public RtspHeaderValueCollection<StringWithQuality> Accept { get; } = RtspHeaderValueFactory.CreateAcceptHeaderValue();
        
        public RtspHeaderValueCollection<StringWithQuality> AcceptEncoding { get; } = RtspHeaderValueFactory.CreateAcceptEncodingHeaderValue();
        
        public RtspHeaderValueCollection<StringWithQuality> AcceptLanguage { get; } = RtspHeaderValueFactory.CreateAcceptLanguageHeaderValue();
        
        public AuthorizationRtspHeaderValue Authorization { get; set; }
        
        public UIntRtspHeaderValue Bandwidth { get; set; }
        
        public UShortRtspHeaderValue BlockSize { get; set; }
        
        public CacheControlRtspHeaderValue CacheControl { get; set; }
        
        public ConferenceRtspHeaderValue Conference { get; set; }
        
        public RtspHeaderValueCollection<string> Connection { get; } = RtspHeaderValueFactory.CreateConnectionHeaderValue();
        
        public UIntRtspHeaderValue CSeq { get; } = RtspHeaderValueFactory.CreateCSeqHeaderValue();
        
        public DateTimeRtspHeaderValue Date { get; set; }
        
        public DateTimeRtspHeaderValue Expires { get; set; }

        public UriRtspHeaderValue From { get; set; }
        
        public RtspHeaderValueCollection<string> IfMatch { get; } = RtspHeaderValueFactory.CreateIfMatchHeaderValue();
        
        public DateTimeRtspHeaderValue IfModifiedSince { get; set; }
        
        public UIntRtspHeaderValue MaxForwards { get; set; }
        
        public RtspHeaderValueCollection<RtspMethod> Public { get; } = RtspHeaderValueFactory.CreatePublicHeaderValue();
        
        public UriRtspHeaderValue Referer { get; set; }
        
        public FloatRtspHeaderValue Scale { get; set; }
        
        public SessionRtspHeaderValue Session { get; set; }
        
        public DoubleRtspHeaderValue Speed { get; set; }
        
        public TransportRtspHeaderValue Transport { get; set; }
        
        public UserAgentRtspHeaderValue UserAgent { get; set; }
        
        public RtspHeaderValueCollection<ProxyInfo> Via { get; } = RtspHeaderValueFactory.CreateViaHeaderValue();
        
        public WWWAuthenticateRtspHeaderValue WWWAuthenticate { get; set; }
    }
}
