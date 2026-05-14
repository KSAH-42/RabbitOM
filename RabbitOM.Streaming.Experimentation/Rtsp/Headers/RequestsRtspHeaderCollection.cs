using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RequestsRtspHeaderCollection : RtspHeaderCollection
    {        
        private static readonly RtspHeaderServiceSettings s_settings = new RtspHeaderServiceSettings( new List<RtspHeaderParser>()
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
        
        public RequestsRtspHeaderCollection() : base( new RtspHeaderService( s_settings ) )
        {
        }
        
        public AcceptRtspHeaderValue Accept
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Accept ) as AcceptRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Accept , value );
        }

        public AcceptEncodingRtspHeaderValue AcceptEncoding
        {
            get => Service.GetHeaderValue( RtspHeaderNames.AcceptEncoding ) as AcceptEncodingRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.AcceptEncoding , value );
        }
        
        public AcceptLanguageRtspHeaderValue AcceptLanguage
        {
            get => Service.GetHeaderValue( RtspHeaderNames.AcceptLanguage ) as AcceptLanguageRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.AcceptLanguage , value );
        }

        public AuthorizationRtspHeaderValue Authorization 
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Authorization ) as AuthorizationRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Authorization , value );
        }        

        public BandwidthRtspHeaderValue Bandwidth
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Bandwidth ) as BandwidthRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Bandwidth , value );
        }
        
        public BlockSizeRtspHeaderValue BlockSize
        {
            get => Service.GetHeaderValue( RtspHeaderNames.BlockSize ) as BlockSizeRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.BlockSize , value );
        }
        
        public CacheControlRtspHeaderValue CacheControl
        {
            get => Service.GetHeaderValue( RtspHeaderNames.CacheControl ) as CacheControlRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.CacheControl , value );
        }
        
        public ConferenceRtspHeaderValue Conference
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Conference ) as ConferenceRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Conference , value );
        }
        
        public ConnectionRtspHeaderValue Connection
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Connection ) as ConnectionRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Connection , value );
        }

        public ContentTypeRtspHeaderValue ContentType
        {
            get => Service.GetHeaderValue( RtspHeaderNames.ContentType ) as ContentTypeRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.ContentType , value );
        }

        public DateTimeRtspHeaderValue Date
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Date ) as DateTimeRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Date , value );
        }
        
        public DateTimeRtspHeaderValue Expires
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Expires ) as DateTimeRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Expires , value );
        }
           
        public UriRtspHeaderValue From
        {
            get => Service.GetHeaderValue( RtspHeaderNames.From ) as UriRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.From , value );
        }
        
        public IfMatchRtspHeaderValue IfMatch
        {
            get => Service.GetHeaderValue( RtspHeaderNames.IfMatch ) as IfMatchRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.IfMatch , value );
        }
        
        public DateTimeRtspHeaderValue IfModifiedSince
        {
            get => Service.GetHeaderValue( RtspHeaderNames.IfModifiedSince ) as DateTimeRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.IfModifiedSince , value );
        }  
                
        public MaxForwardsRtspHeaderValue MaxForwards
        {
            get => Service.GetHeaderValue( RtspHeaderNames.MaxForwards ) as MaxForwardsRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.MaxForwards , value );
        }
        
        public PublicRtspHeaderValue Public
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Public ) as PublicRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Public , value );
        }
        
        public RequireRtspHeaderValue Require
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Require ) as RequireRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Require , value );
        }

        public UriRtspHeaderValue Referer
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Referer ) as UriRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Referer , value );
        }
        
        public ScaleRtspHeaderValue Scale
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Scale ) as ScaleRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Scale , value );
        }
        
        public SessionRtspHeaderValue Session
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Session ) as SessionRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Session , value );
        }
        
        public SpeedRtspHeaderValue Speed
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Speed ) as SpeedRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Speed , value );
        }

        public TimeStampRtspHeaderValue TimeStamp
        {
            get => Service.GetHeaderValue( RtspHeaderNames.TimeStamp ) as TimeStampRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.TimeStamp , value );
        }

        public UriRtspHeaderValue To
        {
            get => Service.GetHeaderValue( RtspHeaderNames.To ) as UriRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.To , value );
        }
        
        public TransportRtspHeaderValue Transport
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Transport ) as TransportRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Transport , value );
        }
        
        public UserAgentRtspHeaderValue UserAgent
        {
            get => Service.GetHeaderValue( RtspHeaderNames.UserAgent ) as UserAgentRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.UserAgent , value );
        }
        
        public ViaRtspHeaderValue Via
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Via ) as ViaRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Via , value );
        }

        public WarningRtspHeaderValue Warning
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Warning ) as WarningRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Warning , value );
        }
    }
}
