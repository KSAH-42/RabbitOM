using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class SupportedFormats
    {
        private static Lazy<IReadOnlyCollection<string>> s_values = new Lazy<IReadOnlyCollection<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
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

        public static IReadOnlyCollection<string> Values { get => s_values.Value; }    
    }
}
