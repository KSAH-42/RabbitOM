using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    // TODO: fuse collections in order to remove internal collection class by introducing static method , including lambda expression and so on
    // TODO: review if theses headers is only present for response

    public class RtspResponseHeaderCollection : RtspHeaderCollection
    {
        public RtspHeaderValueCollection<RtspMethod> Allow { get; } = RtspHeaderValueFactory.CreateAllowHeaderValue();
        
        public CacheControlRtspHeaderValue CacheControl { get; set; }
        
        public ConferenceRtspHeaderValue Conference { get; set; }
        
        public RtspHeaderValueCollection<string> Connection { get; } = RtspHeaderValueFactory.CreateConnectionHeaderValue();
        
        public ContentBaseRtspHeaderValue ContentBase { get; set; }
        
        public ContentRangeRtspHeaderValue ContentRange { get; set; }
        
        public UIntRtspHeaderValue CSeq { get; } = RtspHeaderValueFactory.CreateCSeqHeaderValue();
        
        public DateTimeRtspHeaderValue Date { get; set; }
        
        public DateTimeRtspHeaderValue Expires { get; set; }

        public DateTimeRtspHeaderValue LastModified { get; set; }
        
        public UriRtspHeaderValue Location { get; set; }
        
        public DoubleRtspHeaderValue MediaDuration { get; set; }
        
        public RtspHeaderValueCollection<RtspMethod> Public { get; } = RtspHeaderValueFactory.CreatePublicHeaderValue();
        
        public RtspHeaderValueCollection<RtpInfo> RtpInfo { get; } = RtspHeaderValueFactory.CreateRtpInfoHeaderValue();
        
        public SessionRtspHeaderValue Session { get; set; }
        
        public TransportRtspHeaderValue Transport { get; set; }
        
        public RtspHeaderValueCollection<ProxyInfo> Via { get; } = RtspHeaderValueFactory.CreateViaHeaderValue();
        
        public WWWAuthenticateRtspHeaderValue WWWAuthenticate { get; set; }
    }
}
