using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RtspRequestHeaderCollection : RtspHeaderCollection
    {
        public AcceptRtspHeaderValue Accept
        {
            get => GetValue<AcceptRtspHeaderValue>( AcceptRtspHeaderValue.TypeName );
            set => SetValue<AcceptRtspHeaderValue>( AcceptRtspHeaderValue.TypeName , value );
        }
        
        public AcceptEncodingRtspHeaderValue AcceptEncoding
        {
            get => GetValue<AcceptEncodingRtspHeaderValue>( AcceptEncodingRtspHeaderValue.TypeName );
            set => SetValue<AcceptEncodingRtspHeaderValue>( AcceptEncodingRtspHeaderValue.TypeName , value );
        }
        
        public AcceptLanguageRtspHeaderValue AcceptLanguage
        {
            get => GetValue<AcceptLanguageRtspHeaderValue>( AcceptLanguageRtspHeaderValue.TypeName );
            set => SetValue<AcceptLanguageRtspHeaderValue>( AcceptLanguageRtspHeaderValue.TypeName , value );
        }
        
        public AuthorizationRtspHeaderValue Authorization
        {
            get => GetValue<AuthorizationRtspHeaderValue>( AuthorizationRtspHeaderValue.TypeName );
            set => SetValue<AuthorizationRtspHeaderValue>( AuthorizationRtspHeaderValue.TypeName , value );
        }
        
        public BandwidthRtspHeaderValue Bandwidth
        {
            get => GetValue<BandwidthRtspHeaderValue>( BandwidthRtspHeaderValue.TypeName );
            set => SetValue<BandwidthRtspHeaderValue>( BandwidthRtspHeaderValue.TypeName , value );
        }
        
        public BlockSizeRtspHeaderValue BlockSize
        {
            get => GetValue<BlockSizeRtspHeaderValue>( BlockSizeRtspHeaderValue.TypeName );
            set => SetValue<BlockSizeRtspHeaderValue>( BlockSizeRtspHeaderValue.TypeName , value );
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
        
        public IfMatchRtspHeaderValue IfMatch
        {
            get => GetValue<IfMatchRtspHeaderValue>( IfMatchRtspHeaderValue.TypeName );
            set => SetValue<IfMatchRtspHeaderValue>( IfMatchRtspHeaderValue.TypeName , value );
        }
        
        public IfModifiedSinceRtspHeaderValue IfModifiedSince
        {
            get => GetValue<IfModifiedSinceRtspHeaderValue>( IfModifiedSinceRtspHeaderValue.TypeName );
            set => SetValue<IfModifiedSinceRtspHeaderValue>( IfModifiedSinceRtspHeaderValue.TypeName , value );
        }
        
        public MaxForwardsRtspHeaderValue MaxForwards
        {
            get => GetValue<MaxForwardsRtspHeaderValue>( MaxForwardsRtspHeaderValue.TypeName );
            set => SetValue<MaxForwardsRtspHeaderValue>( MaxForwardsRtspHeaderValue.TypeName , value );
        }
        
        public PublicRtspHeaderValue Public
        {
            get => GetValue<PublicRtspHeaderValue>( PublicRtspHeaderValue.TypeName );
            set => SetValue<PublicRtspHeaderValue>( PublicRtspHeaderValue.TypeName , value );
        }
        
        public RefererRtspHeaderValue Referer
        {
            get => GetValue<RefererRtspHeaderValue>( RefererRtspHeaderValue.TypeName );
            set => SetValue<RefererRtspHeaderValue>( RefererRtspHeaderValue.TypeName , value );
        }
        
        public ScaleRtspHeaderValue Scale
        {
            get => GetValue<ScaleRtspHeaderValue>( ScaleRtspHeaderValue.TypeName );
            set => SetValue<ScaleRtspHeaderValue>( ScaleRtspHeaderValue.TypeName , value );
        }
        
        public SessionRtspHeaderValue Session
        {
            get => GetValue<SessionRtspHeaderValue>( SessionRtspHeaderValue.TypeName );
            set => SetValue<SessionRtspHeaderValue>( SessionRtspHeaderValue.TypeName , value );
        }
        
        public SpeedRtspHeaderValue Speed
        {
            get => GetValue<SpeedRtspHeaderValue>( SpeedRtspHeaderValue.TypeName );
            set => SetValue<SpeedRtspHeaderValue>( SpeedRtspHeaderValue.TypeName , value );
        }
        
        public TransportRtspHeaderValue Transport
        {
            get => GetValue<TransportRtspHeaderValue>( TransportRtspHeaderValue.TypeName );
            set => SetValue<TransportRtspHeaderValue>( TransportRtspHeaderValue.TypeName , value );
        }
        
        public UserAgentRtspHeaderValue UserAgent
        {
            get => GetValue<UserAgentRtspHeaderValue>( UserAgentRtspHeaderValue.TypeName );
            set => SetValue<UserAgentRtspHeaderValue>( UserAgentRtspHeaderValue.TypeName , value );
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
