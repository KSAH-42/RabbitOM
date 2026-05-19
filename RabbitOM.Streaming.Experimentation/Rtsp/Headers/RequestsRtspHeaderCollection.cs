using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RequestsRtspHeaderCollection : RtspHeaderCollection
    {        
        private static readonly RtspHeaderRegistrySettings s_settings = new RtspHeaderRegistrySettings( new List<RtspHeaderParser>()
        {
            RtspHeaderParser.NewParser<AcceptRtspHeaderValue>( RtspHeaderNames.Accept , AcceptRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<AcceptEncodingRtspHeaderValue>( RtspHeaderNames.AcceptEncoding , AcceptEncodingRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<AcceptLanguageRtspHeaderValue>( RtspHeaderNames.AcceptLanguage , AcceptLanguageRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<AuthorizationRtspHeaderValue>( RtspHeaderNames.Authorization, AuthorizationRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<BandwidthRtspHeaderValue>( RtspHeaderNames.Bandwidth, BandwidthRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<BlockSizeRtspHeaderValue>( RtspHeaderNames.BlockSize, BlockSizeRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<CacheControlRtspHeaderValue>( RtspHeaderNames.CacheControl, CacheControlRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<ConferenceRtspHeaderValue>( RtspHeaderNames.Conference, ConferenceRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<ConnectionRtspHeaderValue>( RtspHeaderNames.Connection, ConnectionRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<ContentTypeRtspHeaderValue>( RtspHeaderNames.ContentType, ContentTypeRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<DateTimeRtspHeaderValue>( RtspHeaderNames.Date, DateTimeRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<DateTimeRtspHeaderValue>( RtspHeaderNames.Expires, DateTimeRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<UriRtspHeaderValue>( RtspHeaderNames.From, UriRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<IfMatchRtspHeaderValue>( RtspHeaderNames.IfMatch, IfMatchRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<DateTimeRtspHeaderValue>( RtspHeaderNames.IfModifiedSince, DateTimeRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<MaxForwardsRtspHeaderValue>( RtspHeaderNames.MaxForwards, MaxForwardsRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<PublicRtspHeaderValue>( RtspHeaderNames.Public, PublicRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<RequireRtspHeaderValue>( RtspHeaderNames.Require, RequireRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<UriRtspHeaderValue>( RtspHeaderNames.Referer, UriRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<ScaleRtspHeaderValue>( RtspHeaderNames.Scale, ScaleRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<SessionRtspHeaderValue>( RtspHeaderNames.Session, SessionRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<SpeedRtspHeaderValue>( RtspHeaderNames.Speed, SpeedRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<TimeStampRtspHeaderValue>( RtspHeaderNames.TimeStamp, TimeStampRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<UriRtspHeaderValue>( RtspHeaderNames.To, UriRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<TransportRtspHeaderValue>( RtspHeaderNames.Transport, TransportRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<UserAgentRtspHeaderValue>( RtspHeaderNames.UserAgent, UserAgentRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<ViaRtspHeaderValue>( RtspHeaderNames.Via, ViaRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<WarningRtspHeaderValue>( RtspHeaderNames.Warning, WarningRtspHeaderValue.TryParse ),
        } , 
        new [] 
        { 
            RtspHeaderNames.CSeq , 
            RtspHeaderNames.ContentLength , 
            RtspHeaderNames.WWWAuthenticate 
        });
        
        public RequestsRtspHeaderCollection() : base( new RtspHeaderRegistry( s_settings ) )
        {
        }
        
        public AcceptRtspHeaderValue Accept
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Accept ) as AcceptRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Accept , value );
        }

        public AcceptEncodingRtspHeaderValue AcceptEncoding
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.AcceptEncoding ) as AcceptEncodingRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.AcceptEncoding , value );
        }
        
        public AcceptLanguageRtspHeaderValue AcceptLanguage
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.AcceptLanguage ) as AcceptLanguageRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.AcceptLanguage , value );
        }

        public AuthorizationRtspHeaderValue Authorization 
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Authorization ) as AuthorizationRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Authorization , value );
        }        

        public BandwidthRtspHeaderValue Bandwidth
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Bandwidth ) as BandwidthRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Bandwidth , value );
        }
        
        public BlockSizeRtspHeaderValue BlockSize
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.BlockSize ) as BlockSizeRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.BlockSize , value );
        }
        
        public CacheControlRtspHeaderValue CacheControl
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.CacheControl ) as CacheControlRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.CacheControl , value );
        }
        
        public ConferenceRtspHeaderValue Conference
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Conference ) as ConferenceRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Conference , value );
        }
        
        public ConnectionRtspHeaderValue Connection
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Connection ) as ConnectionRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Connection , value );
        }

        public ContentTypeRtspHeaderValue ContentType
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.ContentType ) as ContentTypeRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.ContentType , value );
        }

        public DateTimeRtspHeaderValue Date
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Date ) as DateTimeRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Date , value );
        }
        
        public DateTimeRtspHeaderValue Expires
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Expires ) as DateTimeRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Expires , value );
        }
           
        public UriRtspHeaderValue From
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.From ) as UriRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.From , value );
        }
        
        public IfMatchRtspHeaderValue IfMatch
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.IfMatch ) as IfMatchRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.IfMatch , value );
        }
        
        public DateTimeRtspHeaderValue IfModifiedSince
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.IfModifiedSince ) as DateTimeRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.IfModifiedSince , value );
        }  
                
        public MaxForwardsRtspHeaderValue MaxForwards
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.MaxForwards ) as MaxForwardsRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.MaxForwards , value );
        }
        
        public PublicRtspHeaderValue Public
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Public ) as PublicRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Public , value );
        }
        
        public RequireRtspHeaderValue Require
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Require ) as RequireRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Require , value );
        }

        public UriRtspHeaderValue Referer
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Referer ) as UriRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Referer , value );
        }
        
        public ScaleRtspHeaderValue Scale
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Scale ) as ScaleRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Scale , value );
        }
        
        public SessionRtspHeaderValue Session
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Session ) as SessionRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Session , value );
        }
        
        public SpeedRtspHeaderValue Speed
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Speed ) as SpeedRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Speed , value );
        }

        public TimeStampRtspHeaderValue TimeStamp
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.TimeStamp ) as TimeStampRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.TimeStamp , value );
        }

        public UriRtspHeaderValue To
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.To ) as UriRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.To , value );
        }
        
        public TransportRtspHeaderValue Transport
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Transport ) as TransportRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Transport , value );
        }
        
        public UserAgentRtspHeaderValue UserAgent
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.UserAgent ) as UserAgentRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.UserAgent , value );
        }
        
        public ViaRtspHeaderValue Via
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Via ) as ViaRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Via , value );
        }

        public WarningRtspHeaderValue Warning
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Warning ) as WarningRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Warning , value );
        }
    }
}
