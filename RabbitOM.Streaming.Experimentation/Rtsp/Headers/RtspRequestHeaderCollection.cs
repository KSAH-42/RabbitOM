using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RtspRequestHeaderCollection : RtspHeaderCollection
    {
        private static readonly RtspHeaderParsers s_parsers = new RtspHeaderParsers();

        static RtspRequestHeaderCollection()
        {
            s_parsers.AddParser<CSeqRtspHeaderValue>( CSeqRtspHeaderValue.TypeName , CSeqRtspHeaderValue.TryParse );
            s_parsers.AddParser<AcceptRtspHeaderValue>( AcceptRtspHeaderValue.TypeName , AcceptRtspHeaderValue.TryParse );
            s_parsers.AddParser<AcceptEncodingRtspHeaderValue>( AcceptEncodingRtspHeaderValue.TypeName , AcceptEncodingRtspHeaderValue.TryParse );
            s_parsers.AddParser<AcceptLanguageRtspHeaderValue>( AcceptLanguageRtspHeaderValue.TypeName , AcceptLanguageRtspHeaderValue.TryParse );
            s_parsers.AddParser<AuthorizationRtspHeaderValue>( AuthorizationRtspHeaderValue.TypeName , AuthorizationRtspHeaderValue.TryParse );
            s_parsers.AddParser<BandwidthRtspHeaderValue>( BandwidthRtspHeaderValue.TypeName , BandwidthRtspHeaderValue.TryParse );
            s_parsers.AddParser<BlockSizeRtspHeaderValue>( BlockSizeRtspHeaderValue.TypeName , BlockSizeRtspHeaderValue.TryParse );
            s_parsers.AddParser<CacheControlRtspHeaderValue>( CacheControlRtspHeaderValue.TypeName , CacheControlRtspHeaderValue.TryParse );
            s_parsers.AddParser<ConferenceRtspHeaderValue>( ConferenceRtspHeaderValue.TypeName , ConferenceRtspHeaderValue.TryParse );
            s_parsers.AddParser<ConnectionRtspHeaderValue>( ConnectionRtspHeaderValue.TypeName , ConnectionRtspHeaderValue.TryParse );
            s_parsers.AddParser<DateRtspHeaderValue>( DateRtspHeaderValue.TypeName , DateRtspHeaderValue.TryParse );
            s_parsers.AddParser<ExpiresRtspHeaderValue>( ExpiresRtspHeaderValue.TypeName , ExpiresRtspHeaderValue.TryParse );
            s_parsers.AddParser<IfMatchRtspHeaderValue>( IfMatchRtspHeaderValue.TypeName , IfMatchRtspHeaderValue.TryParse );
            s_parsers.AddParser<IfModifiedSinceRtspHeaderValue>( IfModifiedSinceRtspHeaderValue.TypeName , IfModifiedSinceRtspHeaderValue.TryParse );
            s_parsers.AddParser<MaxForwardsRtspHeaderValue>( MaxForwardsRtspHeaderValue.TypeName , MaxForwardsRtspHeaderValue.TryParse );
            s_parsers.AddParser<PublicRtspHeaderValue>( PublicRtspHeaderValue.TypeName , PublicRtspHeaderValue.TryParse );
            s_parsers.AddParser<RefererRtspHeaderValue>( RefererRtspHeaderValue.TypeName , RefererRtspHeaderValue.TryParse );
            s_parsers.AddParser<ScaleRtspHeaderValue>( ScaleRtspHeaderValue.TypeName , ScaleRtspHeaderValue.TryParse );
            s_parsers.AddParser<SessionRtspHeaderValue>( SessionRtspHeaderValue.TypeName , SessionRtspHeaderValue.TryParse );
            s_parsers.AddParser<SpeedRtspHeaderValue>( SpeedRtspHeaderValue.TypeName , SpeedRtspHeaderValue.TryParse );
            s_parsers.AddParser<TransportRtspHeaderValue>( TransportRtspHeaderValue.TypeName , TransportRtspHeaderValue.TryParse );
            s_parsers.AddParser<UserAgentRtspHeaderValue>( UserAgentRtspHeaderValue.TypeName , UserAgentRtspHeaderValue.TryParse );
            s_parsers.AddParser<ViaRtspHeaderValue>( ViaRtspHeaderValue.TypeName , ViaRtspHeaderValue.TryParse );
            s_parsers.AddParser<WarningRtspHeaderValue>( WarningRtspHeaderValue.TypeName , WarningRtspHeaderValue.TryParse );
        }

        /// <summary>
        /// Gets / Sets the command sequence
        /// </summary>
        /// <remarks>
        ///     <para> setter is allowed in case that CSeq value can not be parsed </para>
        /// </remarks>
        
        // TODO: remove class CSeqRtspHeaderValue and nullable ushort, and not ushort directly, we don't know if the value has been set

        public CSeqRtspHeaderValue CSeq
        {
            get => GetValue<CSeqRtspHeaderValue>( CSeqRtspHeaderValue.TypeName );
            set => SetValue<CSeqRtspHeaderValue>( CSeqRtspHeaderValue.TypeName , value );
        }
        

        
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
