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
            RtspHeaderNames.CSeq,
            RtspHeaderNames.ContentLength,
            RtspHeaderNames.Authorization
        });

        public ResponsesRtspHeaderCollection() : base( new RtspHeaderRegistry( s_settings ) )
        {
        }

        public AllowRtspHeaderValue Allow
        {
            get => Registry.GetValue( RtspHeaderNames.Allow ) as AllowRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Allow , value );
        }

        public BandwidthRtspHeaderValue Bandwidth
        {
            get => Registry.GetValue( RtspHeaderNames.Bandwidth ) as BandwidthRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Bandwidth , value );
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
        
        public UriRtspHeaderValue ContentBase
        {
            get => Registry.GetValue( RtspHeaderNames.ContentBase ) as UriRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.ContentBase , value );
        }

        public UriRtspHeaderValue ContentLocation
        {
            get => Registry.GetValue( RtspHeaderNames.ContentLocation ) as UriRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.ContentLocation , value );
        }

        public ContentTypeRtspHeaderValue ContentType
        {
            get => Registry.GetValue( RtspHeaderNames.ContentType ) as ContentTypeRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.ContentType , value );
        }

        public ContentRangeRtspHeaderValue ContentRange
        {
            get => Registry.GetValue( RtspHeaderNames.ContentRange ) as ContentRangeRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.ContentRange , value );
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

        public DateTimeRtspHeaderValue LastModified
        {
            get => Registry.GetValue( RtspHeaderNames.LastModified ) as DateTimeRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.LastModified , value );
        }
        
        public UriRtspHeaderValue Location
        {
            get => Registry.GetValue( RtspHeaderNames.Location ) as UriRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Location , value );
        }
        
        public MediaDurationRtspHeaderValue MediaDuration
        {
            get => Registry.GetValue( RtspHeaderNames.MediaDuration ) as MediaDurationRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.MediaDuration , value );
        }
        
        public PublicRtspHeaderValue Public
        {
            get => Registry.GetValue( RtspHeaderNames.Public ) as PublicRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Public , value );
        }
        
        public RequireRtspHeaderValue Require
        {
            get => Registry.GetValue( RtspHeaderNames.Require ) as RequireRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Require , value );
        }

        public DateTimeRtspHeaderValue RetryAfter
        {
            get => Registry.GetValue( RtspHeaderNames.RetryAfter ) as DateTimeRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.RetryAfter , value );
        }

        public RtpInfoRtspHeaderValue RtpInfo
        {
            get => Registry.GetValue( RtspHeaderNames.RtpInfo ) as RtpInfoRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.RtpInfo , value );
        }
        
        public SessionRtspHeaderValue Session
        {
            get => Registry.GetValue( RtspHeaderNames.Session ) as SessionRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Session , value );
        }
        
        public TransportRtspHeaderValue Transport
        {
            get => Registry.GetValue( RtspHeaderNames.Transport ) as TransportRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.Transport , value );
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

        public WWWAuthenticateRtspHeaderValue WWWAuthenticate
        {
            get => Registry.GetValue( RtspHeaderNames.WWWAuthenticate ) as WWWAuthenticateRtspHeaderValue;
            set => Registry.SetValue( RtspHeaderNames.WWWAuthenticate , value );
        }
    }
}
