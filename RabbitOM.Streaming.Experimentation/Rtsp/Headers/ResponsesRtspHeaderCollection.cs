using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class ResponsesRtspHeaderCollection : RtspHeaderCollection
    {
        private static readonly RtspHeaderRegistrySettings s_settings = new RtspHeaderRegistrySettings( new List<RtspHeaderParser>()
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

        public ResponsesRtspHeaderCollection() : base( new RtspHeaderRegistry( s_settings ) )
        {
        }

        public AllowRtspHeaderValue Allow
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Allow ) as AllowRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Allow , value );
        }

        public BandwidthRtspHeaderValue Bandwidth
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Bandwidth ) as BandwidthRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Bandwidth , value );
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
        
        public UriRtspHeaderValue ContentBase
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.ContentBase ) as UriRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.ContentBase , value );
        }

        public UriRtspHeaderValue ContentLocation
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.ContentLocation ) as UriRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.ContentLocation , value );
        }

        public ContentTypeRtspHeaderValue ContentType
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.ContentType ) as ContentTypeRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.ContentType , value );
        }

        public ContentRangeRtspHeaderValue ContentRange
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.ContentRange ) as ContentRangeRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.ContentRange , value );
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

        public DateTimeRtspHeaderValue LastModified
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.LastModified ) as DateTimeRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.LastModified , value );
        }
        
        public UriRtspHeaderValue Location
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Location ) as UriRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Location , value );
        }
        
        public MediaDurationRtspHeaderValue MediaDuration
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.MediaDuration ) as MediaDurationRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.MediaDuration , value );
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

        public DateTimeRtspHeaderValue RetryAfter
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.RetryAfter ) as DateTimeRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.RetryAfter , value );
        }

        public RtpInfoRtspHeaderValue RtpInfo
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.RtpInfo ) as RtpInfoRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.RtpInfo , value );
        }
        
        public SessionRtspHeaderValue Session
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Session ) as SessionRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Session , value );
        }
        
        public TransportRtspHeaderValue Transport
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.Transport ) as TransportRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.Transport , value );
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

        public WWWAuthenticateRtspHeaderValue WWWAuthenticate
        {
            get => Registry.GetHeaderValueAs( RtspHeaderNames.WWWAuthenticate ) as WWWAuthenticateRtspHeaderValue;
            set => Registry.SetHeaderValueAs( RtspHeaderNames.WWWAuthenticate , value );
        }
    }
}
