using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ResponsesRtspHeaderCollection : RtspHeaderCollection
    {
        private static readonly RtspHeaderServiceSettings s_settings = new RtspHeaderServiceSettings( new List<RtspHeaderParser>()
        {
            RtspHeaderParser.NewParser<AllowRtspHeaderValue>( RtspHeaderNames.Allow , AllowRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<BandwidthRtspHeaderValue>( RtspHeaderNames.Bandwidth , BandwidthRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<CacheControlRtspHeaderValue>( RtspHeaderNames.CacheControl , CacheControlRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<ConferenceRtspHeaderValue>( RtspHeaderNames.Conference , ConferenceRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<ConnectionRtspHeaderValue>( RtspHeaderNames.Connection , ConnectionRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<UriRtspHeaderValue>( RtspHeaderNames.ContentBase , UriRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<UriRtspHeaderValue>( RtspHeaderNames.ContentLocation , UriRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<ContentTypeRtspHeaderValue>( RtspHeaderNames.ContentType , ContentTypeRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<ContentRangeRtspHeaderValue>( RtspHeaderNames.ContentRange , ContentRangeRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<DateTimeRtspHeaderValue>( RtspHeaderNames.Date , DateTimeRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<DateTimeRtspHeaderValue>( RtspHeaderNames.Expires , DateTimeRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<DateTimeRtspHeaderValue>( RtspHeaderNames.LastModified , DateTimeRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<UriRtspHeaderValue>( RtspHeaderNames.Location , UriRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<MediaDurationRtspHeaderValue>( RtspHeaderNames.MediaDuration , MediaDurationRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<PublicRtspHeaderValue>( RtspHeaderNames.Public , PublicRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<RequireRtspHeaderValue>( RtspHeaderNames.Require , RequireRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<DateTimeRtspHeaderValue>( RtspHeaderNames.RetryAfter , DateTimeRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<RtpInfoRtspHeaderValue>( RtspHeaderNames.RtpInfo , RtpInfoRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<TransportRtspHeaderValue>( RtspHeaderNames.Transport , TransportRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<ViaRtspHeaderValue>( RtspHeaderNames.Via , ViaRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<WarningRtspHeaderValue>( RtspHeaderNames.Warning , WarningRtspHeaderValue.TryParse ),
            RtspHeaderParser.NewParser<WWWAuthenticateRtspHeaderValue>( RtspHeaderNames.WWWAuthenticate , WWWAuthenticateRtspHeaderValue.TryParse ),
        } , 
        new [] 
        { 
            RtspHeaderNames.CSeq , 
            RtspHeaderNames.ContentLength , 
            RtspHeaderNames.Authorization 
        });

        public ResponsesRtspHeaderCollection() : base( new RtspHeaderService( s_settings ) )
        {
        }

        public AllowRtspHeaderValue Allow
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Allow ) as AllowRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Allow , value );
        }

        public BandwidthRtspHeaderValue Bandwidth
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Bandwidth ) as BandwidthRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Bandwidth , value );
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
        
        public UriRtspHeaderValue ContentBase
        {
            get => Service.GetHeaderValue( RtspHeaderNames.ContentBase ) as UriRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.ContentBase , value );
        }

        public UriRtspHeaderValue ContentLocation
        {
            get => Service.GetHeaderValue( RtspHeaderNames.ContentLocation ) as UriRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.ContentLocation , value );
        }

        public ContentTypeRtspHeaderValue ContentType
        {
            get => Service.GetHeaderValue( RtspHeaderNames.ContentType ) as ContentTypeRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.ContentType , value );
        }

        public ContentRangeRtspHeaderValue ContentRange
        {
            get => Service.GetHeaderValue( RtspHeaderNames.ContentRange ) as ContentRangeRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.ContentRange , value );
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

        public DateTimeRtspHeaderValue LastModified
        {
            get => Service.GetHeaderValue( RtspHeaderNames.LastModified ) as DateTimeRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.LastModified , value );
        }
        
        public UriRtspHeaderValue Location
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Location ) as UriRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Location , value );
        }
        
        public MediaDurationRtspHeaderValue MediaDuration
        {
            get => Service.GetHeaderValue( RtspHeaderNames.MediaDuration ) as MediaDurationRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.MediaDuration , value );
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

        public DateTimeRtspHeaderValue RetryAfter
        {
            get => Service.GetHeaderValue( RtspHeaderNames.RetryAfter ) as DateTimeRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.RetryAfter , value );
        }

        public RtpInfoRtspHeaderValue RtpInfo
        {
            get => Service.GetHeaderValue( RtspHeaderNames.RtpInfo ) as RtpInfoRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.RtpInfo , value );
        }
        
        public SessionRtspHeaderValue Session
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Session ) as SessionRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Session , value );
        }
        
        public TransportRtspHeaderValue Transport
        {
            get => Service.GetHeaderValue( RtspHeaderNames.Transport ) as TransportRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.Transport , value );
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

        public WWWAuthenticateRtspHeaderValue WWWAuthenticate
        {
            get => Service.GetHeaderValue( RtspHeaderNames.WWWAuthenticate ) as WWWAuthenticateRtspHeaderValue;
            set => Service.SetHeaderValue( RtspHeaderNames.WWWAuthenticate , value );
        }
    }
}
