// TODO: add unit test to ensure that CSeq and ContentLength headers are NOT exposed by this class

/*
 * CSeq , Content-Length are not present and it will be to dto send by the channel
 */
using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Extensions;

    public sealed class HeaderCollectionResponse : HeaderCollection
    {
        public uint? Bandwidth
        {
            get => GetValue( HeaderNames.Bandwidth ).ToNullableUInt();
            set => SetValue( HeaderNames.Bandwidth , value );
        }
        public Uri ContentBase
        {
            get => GetValue( HeaderNames.ContentBase ).ToUri();
            set => SetValue( HeaderNames.ContentBase , value );
        }
        
        public DateTime? Date
        {
            get => GetValue( HeaderNames.Date ).ToNullableDateTime();
            set => SetValue( HeaderNames.Date , value );
        }
        
        public DateTime? Expires
        {
            get => GetValue( HeaderNames.Expires ).ToNullableDateTime();
            set => SetValue( HeaderNames.Expires , value );
        }

        public DateTime? LastModified
        {
            get => GetValue( HeaderNames.LastModified ).ToNullableDateTime();
            set => SetValue( HeaderNames.LastModified , value );
        }
        
        public Uri Location
        {
            get => GetValue( HeaderNames.Location ).ToUri();
            set => SetValue( HeaderNames.Location , value );
        }
        
        public double? MediaDuration
        {
            get => GetValue( HeaderNames.MediaDuration ).ToNullableDouble();
            set => SetValue( HeaderNames.MediaDuration , value );
        }
        
        public AuthenticateHeaderValue Authenticate
        {
            get => GetValueObject( HeaderNames.Authenticate ) as AuthenticateHeaderValue;
            set => SetValueObject( HeaderNames.Authenticate , value );
        }

        public CacheControlHeaderValue CacheControl
        {
            get => GetValueObject( HeaderNames.CacheControl ) as CacheControlHeaderValue;
            set => SetValueObject( HeaderNames.CacheControl , value );
        }
        
        public ConferenceHeaderValue Conference
        {
            get => GetValueObject( HeaderNames.Conference ) as ConferenceHeaderValue;
            set => SetValueObject( HeaderNames.Conference , value );
        }
        
        public ContentRangeHeaderValue ContentRange
        {
            get => GetValueObject( HeaderNames.ContentRange ) as ContentRangeHeaderValue;
            set => SetValueObject( HeaderNames.ContentRange , value );
        }
        
        
        public RtpInfoHeaderValue RtpInfo
        {
            get => GetValueObject( HeaderNames.RtpInfo ) as RtpInfoHeaderValue;
            set => SetValueObject( HeaderNames.RtpInfo , value );
        }
        
        public SessionHeaderValue Session
        {
            get => GetValueObject( HeaderNames.Session ) as SessionHeaderValue;
            set => SetValueObject( HeaderNames.Session , value );
        }
        
        public TransportHeaderValue Transport
        {
            get => GetValueObject( HeaderNames.Transport ) as TransportHeaderValue;
            set => SetValueObject( HeaderNames.Transport , value );
        }
                
        public HeaderValueCollection<MethodHeaderValue> Allow
        {
            get
            {
                var headerValue = GetValueObject( HeaderNames.Allow ) as HeaderValueCollection<MethodHeaderValue>;

                if ( headerValue == null )
                {
                    headerValue = new HeaderValueCollection<MethodHeaderValue>();
                    AddOrUpdateObject( HeaderNames.Allow , headerValue );
                }

                return headerValue;
            }
        }

        public HeaderValueCollection<string> Connection
        {
            get
            {
                var headerValue = GetValueObject( HeaderNames.Connection ) as HeaderValueCollection<string>;

                if ( headerValue == null )
                {
                    headerValue = new HeaderValueCollection<string>();
                    AddOrUpdateObject( HeaderNames.Connection , headerValue );
                }

                return headerValue;
            }
        }
        
        public HeaderValueCollection<MethodHeaderValue> Public
        {
            get
            {
                var headerValue = GetValueObject( HeaderNames.Public ) as HeaderValueCollection<MethodHeaderValue>;

                if ( headerValue == null )
                {
                    headerValue = new HeaderValueCollection<MethodHeaderValue>();
                    AddOrUpdateObject( HeaderNames.Public , headerValue );
                }

                return headerValue;
            }
        }
        
        public HeaderValueCollection<ViaHeaderValue> Via
        {
            get
            {
                var headerValue = GetValueObject( HeaderNames.Via ) as HeaderValueCollection<ViaHeaderValue>;

                if ( headerValue == null )
                {
                    headerValue = new HeaderValueCollection<ViaHeaderValue>();
                    AddOrUpdateObject( HeaderNames.Via , headerValue );
                }

                return headerValue;
            }
        }

        public HeaderValueCollection<WarningHeaderValue> Warning
        {
            get
            {
                var headerValue = GetValueObject( HeaderNames.Warning ) as HeaderValueCollection<WarningHeaderValue>;

                if ( headerValue == null )
                {
                    headerValue = new HeaderValueCollection<WarningHeaderValue>();
                    AddOrUpdateObject( HeaderNames.Warning , headerValue );
                }

                return headerValue;
            }
        }
    }
}
