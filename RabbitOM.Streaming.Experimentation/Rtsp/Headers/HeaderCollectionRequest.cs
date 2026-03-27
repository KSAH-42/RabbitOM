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
        public HeaderValueCollection<StringWithQualityHeaderValue> Accept
        {
            get => GetValueObject( HeaderNames.Accept , () => new HeaderValueCollection<StringWithQualityHeaderValue>() );
        }
        
        public HeaderValueCollection<StringWithQualityHeaderValue> AcceptEncoding
        {
            get => GetValueObject( HeaderNames.AcceptEncoding , () => new HeaderValueCollection<StringWithQualityHeaderValue>() );
        }
        
        public HeaderValueCollection<StringWithQualityHeaderValue> AcceptLanguage
        {
            get => GetValueObject( HeaderNames.AcceptLanguage , () => new HeaderValueCollection<StringWithQualityHeaderValue>() );
        }

        public AuthorizationHeaderValue Authorization
        {
            get => GetValueObject( HeaderNames.Authorization ) as AuthorizationHeaderValue;
            set => SetValueObject( HeaderNames.Authorization , value );
        }        

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
        
        public HeaderValueCollection<string> Connection
        {
            get => GetValueObject( HeaderNames.Connection , () => new HeaderValueCollection<string>( (value) => ! string.IsNullOrWhiteSpace( value ) ) );
        }

        public DateTime? Date
        {
            get => GetValue( HeaderNames.Date ).ToNullableDateTime();
            set => SetValue( HeaderNames.Date , value?.ToUniversalDateString() );
        }
        
        public DateTime? Expires
        {
            get => GetValue( HeaderNames.Expires ).ToNullableDateTime();
            set => SetValue( HeaderNames.Expires , value?.ToUniversalDateString() );
        }
           
        public Uri From
        {
            get => GetValue( HeaderNames.From ).ToUri();
            set => SetValue( HeaderNames.From , value );
        }
        
        public IfMatchHeaderValue IfMatch
        {
            get => GetValueObject( HeaderNames.IfMatch ) as IfMatchHeaderValue;
            set => SetValueObject( HeaderNames.IfMatch , value );
        }
        
        public DateTime? IfModifiedSince
        {
            get => GetValue( HeaderNames.IfModifiedSince ).ToNullableDateTime();
            set => SetValue( HeaderNames.IfModifiedSince , value?.ToUniversalDateString() );
        }
             
                
        public uint? MaxForwards
        {
            get => GetValue( HeaderNames.MaxForwards ).ToNullableUInt();
            set => SetValue( HeaderNames.MaxForwards , value );
        }
        
        public HeaderValueCollection<MethodHeaderValue> Public
        {
            get => GetValueObject( HeaderNames.Public , () => new HeaderValueCollection<MethodHeaderValue>() );
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
        
        public SessionHeaderValue Session
        {
            get => GetValueObject( HeaderNames.Session ) as SessionHeaderValue;
            set => SetValueObject( HeaderNames.Session , value );
        }
        
        public double? Speed
        {
            get => GetValue( HeaderNames.Speed ).ToNullableFloat();
            set => SetValue( HeaderNames.Speed , value );
        }

        public Uri To
        {
            get => GetValue( HeaderNames.To ).ToUri();
            set => SetValue( HeaderNames.To , value );
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
        
        public HeaderValueCollection<ViaHeaderValue> Via
        {
            get => GetValueObject( HeaderNames.Via , () => new HeaderValueCollection<ViaHeaderValue>() );
        }

        public HeaderValueCollection<WarningHeaderValue> Warning
        {
            get => GetValueObject( HeaderNames.Warning , () => new HeaderValueCollection<WarningHeaderValue>() );
        }
    }
}
