using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class RtspResponseHeaderCollection : RtspHeaderCollection
    {
        public AllowRtspHeaderValue Allow
        {
            get => GetValue<AllowRtspHeaderValue>( AllowRtspHeaderValue.TypeName );
            set => SetValue<AllowRtspHeaderValue>( AllowRtspHeaderValue.TypeName , value );
        }

        public AuthenticateRtspHeaderValue Authenticate
        {
            get => GetValue<AuthenticateRtspHeaderValue>( AuthenticateRtspHeaderValue.TypeName );
            set => SetValue<AuthenticateRtspHeaderValue>( AuthenticateRtspHeaderValue.TypeName , value );
        }

        public CacheControlRtspHeaderValue CacheControl
        {
            get => GetValue<CacheControlRtspHeaderValue>( CacheControlRtspHeaderValue.TypeName );
            set => SetValue<CacheControlRtspHeaderValue>( CacheControlRtspHeaderValue.TypeName , value );
        }
        
        public ConferenceRtspHeaderValue Conference
        {
            get => GetValue<ConferenceRtspHeaderValue>( ConferenceRtspHeaderValue.TypeName );
            set => SetValue<ConferenceRtspHeaderValue>( ConferenceRtspHeaderValue.TypeName , value );
        }
        
        public ConnectionRtspHeaderValue Connection
        {
            get => GetValue<ConnectionRtspHeaderValue>( ConnectionRtspHeaderValue.TypeName );
            set => SetValue<ConnectionRtspHeaderValue>( ConnectionRtspHeaderValue.TypeName , value );
        }
        
        public ContentBaseRtspHeaderValue ContentBase
        {
            get => GetValue<ContentBaseRtspHeaderValue>( ContentBaseRtspHeaderValue.TypeName );
            set => SetValue<ContentBaseRtspHeaderValue>( ContentBaseRtspHeaderValue.TypeName , value );
        }
        
        public ContentRangeRtspHeaderValue ContentRange
        {
            get => GetValue<ContentRangeRtspHeaderValue>( ContentRangeRtspHeaderValue.TypeName );
            set => SetValue<ContentRangeRtspHeaderValue>( ContentRangeRtspHeaderValue.TypeName , value );
        }
        
        public CSeqRtspHeaderValue CSeq
        {
            get => GetValue<CSeqRtspHeaderValue>( CSeqRtspHeaderValue.TypeName );
            set => SetValue<CSeqRtspHeaderValue>( CSeqRtspHeaderValue.TypeName , value );
        }
        
        public DateRtspHeaderValue Date
        {
            get => GetValue<DateRtspHeaderValue>( DateRtspHeaderValue.TypeName );
            set => SetValue<DateRtspHeaderValue>( DateRtspHeaderValue.TypeName , value );
        }
        
        public ExpiresRtspHeaderValue Expires
        {
            get => GetValue<ExpiresRtspHeaderValue>( ExpiresRtspHeaderValue.TypeName );
            set => SetValue<ExpiresRtspHeaderValue>( ExpiresRtspHeaderValue.TypeName , value );
        }

        public LastModifiedRtspHeaderValue LastModified
        {
            get => GetValue<LastModifiedRtspHeaderValue>( LastModifiedRtspHeaderValue.TypeName );
            set => SetValue<LastModifiedRtspHeaderValue>( LastModifiedRtspHeaderValue.TypeName , value );
        }
        
        public LocationRtspHeaderValue Location
        {
            get => GetValue<LocationRtspHeaderValue>( LocationRtspHeaderValue.TypeName );
            set => SetValue<LocationRtspHeaderValue>( LocationRtspHeaderValue.TypeName , value );
        }
        
        public MediaDurationRtspHeaderValue MediaDuration
        {
            get => GetValue<MediaDurationRtspHeaderValue>( MediaDurationRtspHeaderValue.TypeName );
            set => SetValue<MediaDurationRtspHeaderValue>( MediaDurationRtspHeaderValue.TypeName , value );
        }
        
        public PublicRtspHeaderValue Public
        {
            get => GetValue<PublicRtspHeaderValue>( PublicRtspHeaderValue.TypeName );
            set => SetValue<PublicRtspHeaderValue>( PublicRtspHeaderValue.TypeName , value );
        }
        
        public RtpInfoRtspHeaderValue RtpInfo
        {
            get => GetValue<RtpInfoRtspHeaderValue>( RtpInfoRtspHeaderValue.TypeName );
            set => SetValue<RtpInfoRtspHeaderValue>( RtpInfoRtspHeaderValue.TypeName , value );
        }
        
        public SessionRtspHeaderValue Session
        {
            get => GetValue<SessionRtspHeaderValue>( SessionRtspHeaderValue.TypeName );
            set => SetValue<SessionRtspHeaderValue>( SessionRtspHeaderValue.TypeName , value );
        }
        
        public TransportRtspHeaderValue Transport
        {
            get => GetValue<TransportRtspHeaderValue>( TransportRtspHeaderValue.TypeName );
            set => SetValue<TransportRtspHeaderValue>( TransportRtspHeaderValue.TypeName , value );
        }
        
        public ViaRtspHeaderValue Via
        {
            get => GetValue<ViaRtspHeaderValue>( ViaRtspHeaderValue.TypeName );
            set => SetValue<ViaRtspHeaderValue>( ViaRtspHeaderValue.TypeName , value );
        }

        public WarningRtspHeaderValue Warning
        {
            get => GetValue<WarningRtspHeaderValue>( WarningRtspHeaderValue.TypeName );
            set => SetValue<WarningRtspHeaderValue>( WarningRtspHeaderValue.TypeName , value );
        }
    }
}
