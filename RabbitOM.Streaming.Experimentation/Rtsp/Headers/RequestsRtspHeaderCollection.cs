using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class RequestsRtspHeaderCollection : RtspHeaderCollection
    {        
        private static readonly RtspHeaderServiceSettings s_settings = RtspHeaderServiceSettingsFactory.CreateServiceSettingsForRequests();
        
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

        // TODO: question for a small rabbit or even a great: do we need to remove and let the communication layer to populate them ?
        // => if not null force them, if not let sub communication layer to populate them
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
