using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class RtspHeaderValueFactory
    {
        public static RtspHeaderValueCollection<StringWithQuality> CreateAcceptHeaderValue()
        {
            return new StringWithQualityRtspHeaderValueList( element => SupportedTypes.Formats.Contains( element.Value ) );
        }

        public static RtspHeaderValueCollection<StringWithQuality> CreateAcceptEncodingHeaderValue()
        {
            return new StringWithQualityRtspHeaderValueList( element => SupportedTypes.Encodings.Contains( element.Value ) );
        }

        public static RtspHeaderValueCollection<StringWithQuality> CreateAcceptLanguageHeaderValue()
        {
            return new StringWithQualityRtspHeaderValueList( element => SupportedTypes.Languages.Contains( element.Value ) );
        }

        public static RtspHeaderValueCollection<RtspMethod> CreateAllowHeaderValue()
        {
            return new RtspMethodHeaderValueHashSet( );
        }

        public static RtspHeaderValueCollection<string> CreateConnectionHeaderValue()
        {
            return new StringRtspHeaderValueHashSet( RtspHeaderValueValidator.TryValidateDirective );
        }

        public static RtspHeaderValueCollection<string> CreateIfMatchHeaderValue()
        {
            return new StringRtspHeaderValueHashSet();
        }

        public static RtspHeaderValueCollection<RtspMethod> CreatePublicHeaderValue()
        {
            return new RtspMethodHeaderValueHashSet( );
        }        

        public static RtspHeaderValueCollection<RtpInfo> CreateRtpInfoHeaderValue()
        {
            return new RtpInfoHeaderValueHashSet();
        }

        public static RtspHeaderValueCollection<ProxyInfo> CreateViaHeaderValue()
        {
            return new ProxyInfoRtspHeaderValueHashSet();
        }

        public static UIntRtspHeaderValue CreateCSeqHeaderValue()
        {
            return new UIntRtspHeaderValue();
        }
    }
}
