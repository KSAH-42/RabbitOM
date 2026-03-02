using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class Constants
    {
        private static Lazy<IReadOnlyCollection<string>> s_currentLanguages = new Lazy<IReadOnlyCollection<string>>( () =>
        {
            var languages = CultureInfo.GetCultures( CultureTypes.AllCultures ).Select( culture => culture.Name );

            return new HashSet<string>( languages , StringComparer.OrdinalIgnoreCase );
        });

        private static Lazy<IReadOnlyCollection<string>> s_transportsTypes = new Lazy<IReadOnlyCollection<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
        {
            "RTP",
            "RTP/AVP",
            "RTP/AVP/TCP",
            "RTP/AVP/UDP",
            "RTP/AVPF",
            "RTP/AVPF/TCP",
            "RTP/AVPF/UDP",
            "RTP/SAVP",
            "RTP/SAVP/TCP",
            "RTP/SAVP/UDP",
            "RTP/SAVPF",
            "RTP/SAVPF/TCP",
            "RTP/SAVPF/UDP",
            "SRTP",
            "SRTP/AVP",
            "SRTP/AVP/TCP",
            "SRTP/AVP/UDP",
            "SRTP/AVPF",
            "SRTP/AVPF/TCP",
            "SRTP/AVPF/UDP",
            "SRTP/SAVP",
            "SRTP/SAVP/TCP",
            "SRTP/SAVP/UDP",
            "SRTP/SAVPF",
            "SRTP/SAVPF/TCP",
            "SRTP/SAVPF/UDP",

        });

        private static Lazy<IReadOnlyCollection<string>> s_transmissionsTypes = new Lazy<IReadOnlyCollection<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
        {
            "unicast",
            "multicast",
        });

        private static Lazy<IReadOnlyCollection<string>> s_encodingTypes = new Lazy<IReadOnlyCollection<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
        {
            "zip",
            "tar",
            "gzip",
            "identity" ,
            "deflate" ,
            "br",
            "*",
        });

        







        public static IReadOnlyCollection<string> CurrentLanguages { get => s_currentLanguages.Value; }

        public static IReadOnlyCollection<string> TransportsTypes { get => s_transportsTypes.Value; }
    
        public static IReadOnlyCollection<string> TransmissionsTypes { get => s_transmissionsTypes.Value; }

        public static IReadOnlyCollection<string> EncodingTypes { get => s_encodingTypes.Value; }
    }
}
