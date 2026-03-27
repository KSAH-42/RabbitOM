// TODO: add unit test to ensure that CSeq and ContentLength headers are NOT exposed by this class

/*
 * CSeq , Content-Length are not present and it will be to dto send by the channel
 */
using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    using RabbitOM.Streaming.Experimentation.Rtsp.Headers.Extensions;

    public sealed class HeaderCollectionRequest : HeaderCollection
    {
        public uint? Bandwidth
        {
            get => GetValue( HeaderNames.Bandwidth ).ToNullableUInt();
            set => SetValue( HeaderNames.Bandwidth , value );
        }
        
        public ushort? BlockSize
        {
            get => GetValue( HeaderNames.BlockSize ).ToNullableUShort();
            set => SetValue( HeaderNames.BlockSize , value );
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
        public DateTime? IfModifiedSince
        {
            get => GetValue( HeaderNames.IfModifiedSince ).ToNullableDateTime();
            set => SetValue( HeaderNames.IfModifiedSince , value );
        }
                
        public Uri From
        {
            get => GetValue( HeaderNames.From ).ToUri();
            set => SetValue( HeaderNames.From , value );
        }

        public Uri To
        {
            get => GetValue( HeaderNames.To ).ToUri();
            set => SetValue( HeaderNames.To , value );
        }
        
        public Uri Referer
        {
            get => GetValue( HeaderNames.Referer ).ToUri();
            set => SetValue( HeaderNames.Referer , value );
        }
        
        public float? Scale
        {
            get => GetValue( HeaderNames.Scale ).ToNullableFloat();
            set => SetValue( HeaderNames.Scale , value );
        }
                
        public uint? MaxForwards
        {
            get => GetValue( HeaderNames.MaxForwards ).ToNullableUInt();
            set => SetValue( HeaderNames.MaxForwards , value );
        }
        
        public double? Speed
        {
            get => GetValue( HeaderNames.Speed ).ToNullableFloat();
            set => SetValue( HeaderNames.Speed , value );
        }
        
        public CacheControlHeaderValue CacheControl
        {
            get => GetValueObject( HeaderNames.CacheControl ) as CacheControlHeaderValue;
            set => SetValueObject( HeaderNames.CacheControl , value );
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
        
        public UserAgentHeaderValue UserAgent
        {
            get => GetValueObject( HeaderNames.UserAgent ) as UserAgentHeaderValue;
            set => SetValueObject( HeaderNames.UserAgent , value );
        }
        
        public AuthorizationHeaderValue Authorization
        {
            get => GetValueObject( HeaderNames.Authorization ) as AuthorizationHeaderValue;
            set => SetValueObject( HeaderNames.Authorization , value );
        }
        
        public ConferenceHeaderValue Conference
        {
            get => GetValueObject( HeaderNames.Conference ) as ConferenceHeaderValue;
            set => SetValueObject( HeaderNames.Conference , value );
        }
        
        public ConnectionHeaderValue Connection
        {
            get => GetValueObject( HeaderNames.Connection ) as ConnectionHeaderValue;
            set => SetValueObject( HeaderNames.Connection , value );
        }
        
        public IfMatchHeaderValue IfMatch
        {
            get => GetValueObject( HeaderNames.IfMatch ) as IfMatchHeaderValue;
            set => SetValueObject( HeaderNames.IfMatch , value );
        }
        
        public HeaderValueCollection<StringWithQualityHeaderValue> Accept
        {
            get
            {
                var headerValue = GetValueObject( HeaderNames.Accept ) as HeaderValueCollection<StringWithQualityHeaderValue>;

                if ( headerValue == null )
                {
                    headerValue = new HeaderValueCollection<StringWithQualityHeaderValue>();
                    AddOrUpdateObject( HeaderNames.Accept , headerValue );
                }

                return headerValue;
            }
        }
        
        public HeaderValueCollection<StringWithQualityHeaderValue> AcceptEncoding
        {
            get
            {
                var headerValue = GetValueObject( HeaderNames.AcceptEncoding ) as HeaderValueCollection<StringWithQualityHeaderValue>;

                if ( headerValue == null )
                {
                    headerValue = new HeaderValueCollection<StringWithQualityHeaderValue>();
                    AddOrUpdateObject( HeaderNames.AcceptEncoding , headerValue );
                }

                return headerValue;
            }
        }
        
        public HeaderValueCollection<StringWithQualityHeaderValue> AcceptLanguage
        {
            get
            {
                var headerValue = GetValueObject( HeaderNames.AcceptLanguage ) as HeaderValueCollection<StringWithQualityHeaderValue>;

                if ( headerValue == null )
                {
                    headerValue = new HeaderValueCollection<StringWithQualityHeaderValue>();
                    AddOrUpdateObject( HeaderNames.AcceptLanguage , headerValue );
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
