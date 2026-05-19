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
            RtspHeaderNames.ContentLength,
            RtspHeaderNames.CSeq,
            RtspHeaderNames.Public,
            RtspHeaderNames.RtpInfo,
            RtspHeaderNames.Server,
            RtspHeaderNames.WWWAuthenticate,
        });
        
        public RequestsRtspHeaderCollection() : base( new RtspHeaderRegistry( s_settings ) )
        {
        }
        
        public AcceptRtspHeaderValue Accept
        {
            get => Registry.GetValue( RtspHeaderNames.Accept ) as AcceptRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Accept , value );
        }

        public AcceptEncodingRtspHeaderValue AcceptEncoding
        {
            get => Registry.GetValue( RtspHeaderNames.AcceptEncoding ) as AcceptEncodingRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.AcceptEncoding , value );
        }
        
        public AcceptLanguageRtspHeaderValue AcceptLanguage
        {
            get => Registry.GetValue( RtspHeaderNames.AcceptLanguage ) as AcceptLanguageRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.AcceptLanguage , value );
        }

        public AuthorizationRtspHeaderValue Authorization 
        {
            get => Registry.GetValue( RtspHeaderNames.Authorization ) as AuthorizationRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Authorization , value );
        }        

        public BandwidthRtspHeaderValue Bandwidth
        {
            get => Registry.GetValue( RtspHeaderNames.Bandwidth ) as BandwidthRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Bandwidth , value );
        }
        
        public BlockSizeRtspHeaderValue BlockSize
        {
            get => Registry.GetValue( RtspHeaderNames.BlockSize ) as BlockSizeRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.BlockSize , value );
        }
        
        public CacheControlRtspHeaderValue CacheControl
        {
            get => Registry.GetValue( RtspHeaderNames.CacheControl ) as CacheControlRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.CacheControl , value );
        }
        
        public ConferenceRtspHeaderValue Conference
        {
            get => Registry.GetValue( RtspHeaderNames.Conference ) as ConferenceRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Conference , value );
        }
        
        public ConnectionRtspHeaderValue Connection
        {
            get => Registry.GetValue( RtspHeaderNames.Connection ) as ConnectionRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Connection , value );
        }

        public ContentTypeRtspHeaderValue ContentType
        {
            get => Registry.GetValue( RtspHeaderNames.ContentType ) as ContentTypeRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.ContentType , value );
        }

        public DateTimeRtspHeaderValue Date
        {
            get => Registry.GetValue( RtspHeaderNames.Date ) as DateTimeRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Date , value );
        }
        
        public DateTimeRtspHeaderValue Expires
        {
            get => Registry.GetValue( RtspHeaderNames.Expires ) as DateTimeRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Expires , value );
        }
           
        public UriRtspHeaderValue From
        {
            get => Registry.GetValue( RtspHeaderNames.From ) as UriRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.From , value );
        }
        
        public IfMatchRtspHeaderValue IfMatch
        {
            get => Registry.GetValue( RtspHeaderNames.IfMatch ) as IfMatchRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.IfMatch , value );
        }
        
        public DateTimeRtspHeaderValue IfModifiedSince
        {
            get => Registry.GetValue( RtspHeaderNames.IfModifiedSince ) as DateTimeRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.IfModifiedSince , value );
        }  
                
        public MaxForwardsRtspHeaderValue MaxForwards
        {
            get => Registry.GetValue( RtspHeaderNames.MaxForwards ) as MaxForwardsRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.MaxForwards , value );
        }
        
        public RequireRtspHeaderValue Require
        {
            get => Registry.GetValue( RtspHeaderNames.Require ) as RequireRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Require , value );
        }

        public UriRtspHeaderValue Referer
        {
            get => Registry.GetValue( RtspHeaderNames.Referer ) as UriRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Referer , value );
        }
        
        public ScaleRtspHeaderValue Scale
        {
            get => Registry.GetValue( RtspHeaderNames.Scale ) as ScaleRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Scale , value );
        }
        
        public SessionRtspHeaderValue Session
        {
            get => Registry.GetValue( RtspHeaderNames.Session ) as SessionRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Session , value );
        }
        
        public SpeedRtspHeaderValue Speed
        {
            get => Registry.GetValue( RtspHeaderNames.Speed ) as SpeedRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Speed , value );
        }

        public TimeStampRtspHeaderValue TimeStamp
        {
            get => Registry.GetValue( RtspHeaderNames.TimeStamp ) as TimeStampRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.TimeStamp , value );
        }

        public UriRtspHeaderValue To
        {
            get => Registry.GetValue( RtspHeaderNames.To ) as UriRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.To , value );
        }
        
        public TransportRtspHeaderValue Transport
        {
            get => Registry.GetValue( RtspHeaderNames.Transport ) as TransportRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Transport , value );
        }
        
        public UserAgentRtspHeaderValue UserAgent
        {
            get => Registry.GetValue( RtspHeaderNames.UserAgent ) as UserAgentRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.UserAgent , value );
        }
        
        public ViaRtspHeaderValue Via
        {
            get => Registry.GetValue( RtspHeaderNames.Via ) as ViaRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Via , value );
        }

        public WarningRtspHeaderValue Warning
        {
            get => Registry.GetValue( RtspHeaderNames.Warning ) as WarningRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Warning , value );
        }
    }
}
