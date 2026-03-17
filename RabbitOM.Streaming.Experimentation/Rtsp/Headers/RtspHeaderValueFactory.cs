using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class RtspHeaderValueFactory
    {
        public static RtspHeaderValueCollection<StringWithQuality> AcceptRtspHeaderValueCollection()
        {
            return new StringWithQualityRtspHeaderValueList( element => SupportedTypes.Formats.Contains( element.Value ) );
        }

        public static RtspHeaderValueCollection<StringWithQuality> AcceptEncodingRtspHeaderValueCollection()
        {
            return new StringWithQualityRtspHeaderValueList( element => SupportedTypes.Encodings.Contains( element.Value ) );
        }

        public static RtspHeaderValueCollection<StringWithQuality> AcceptLanguageRtspHeaderValueCollection()
        {
            return new StringWithQualityRtspHeaderValueList( element => SupportedTypes.Languages.Contains( element.Value ) );
        }

        public static RtspHeaderValueCollection<RtspMethod> AllowRtspHeaderValueCollection()
        {
            return new RtspMethodHeaderValueHashSet( );
        }

        public static RtspHeaderValueCollection<string> ConnectionRtspHeaderValueCollection()
        {
            return new StringRtspHeaderValueHashSet( RtspHeaderValueValidator.TryValidateDirective );
        }

        public static RtspHeaderValueCollection<string> IfMatchRtspHeaderValueCollection()
        {
            return new StringRtspHeaderValueHashSet();
        }

        public static RtspHeaderValueCollection<RtspMethod> PublicRtspHeaderValueCollection()
        {
            return new RtspMethodHeaderValueHashSet( );
        }        

        public static RtspHeaderValueCollection<RtpInfo> NewRtpInfoRtspHeaderValueCollection()
        {
            return new RtpInfoHeaderValueHashSet();
        }

        public static RtspHeaderValueCollection<ProxyInfo> ViaRtspHeaderValueCollection()
        {
            return new ProxyInfoRtspHeaderValueHashSet();
        }
    }
}
