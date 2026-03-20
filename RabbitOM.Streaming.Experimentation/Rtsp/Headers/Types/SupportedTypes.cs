using System;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public static class SupportedTypes
    {
        private static readonly Lazy<IReadOnlyCollection<string>> s_encodings = new Lazy<IReadOnlyCollection<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
        {
            "zip",
            "tar",
            "gzip",
            "identity" ,
            "deflate" ,
            "br",
            "*",
        });
        
        private static readonly Lazy<IReadOnlyCollection<string>> s_mimes = new Lazy<IReadOnlyCollection<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
        {
            "application/sdp",
            "application/text" ,
            "application/xml" ,
            "application/json" ,
            "application/parameters" ,
            "application/binary" ,
            "text" ,
            "text/sdp" ,
            "text/xml" ,
            "text/json" ,
            "text/plain" ,
            "text/parameters" ,
            "sdp" ,
            "xml" ,
            "json" ,
            "binary" ,
        });
        
        private static readonly Lazy<IReadOnlyCollection<string>> s_languages = new Lazy<IReadOnlyCollection<string>>( () =>
        {
            var languages = CultureInfo.GetCultures( CultureTypes.AllCultures ).Select( culture => culture.Name );

            return new HashSet<string>( languages , StringComparer.OrdinalIgnoreCase );
        });

        private static readonly Lazy<IReadOnlyCollection<string>> s_transmissions = new Lazy<IReadOnlyCollection<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
        {
            "unicast",
            "multicast",
        });

        private static readonly Lazy<IReadOnlyCollection<string>> s_transports = new Lazy<IReadOnlyCollection<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
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





        public static IReadOnlyCollection<string> Encodings { get => s_encodings.Value; }

        public static IReadOnlyCollection<string> Mimes { get => s_mimes.Value; }

        public static IReadOnlyCollection<string> Languages { get => s_languages.Value; }

        public static IReadOnlyCollection<string> Transmissions { get => s_transmissions.Value; }

        public static IReadOnlyCollection<string> Transports { get => s_transports.Value; }
    }
}
