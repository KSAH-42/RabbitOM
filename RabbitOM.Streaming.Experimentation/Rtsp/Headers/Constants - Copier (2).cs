using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public static class SupportedEncodings
    {
        private static Lazy<IReadOnlyCollection<string>> s_values = new Lazy<IReadOnlyCollection<string>>( () => new HashSet<string>( StringComparer.OrdinalIgnoreCase)
        {
            "zip",
            "tar",
            "gzip",
            "identity" ,
            "deflate" ,
            "br",
            "*",
        });

        public static IReadOnlyCollection<string> Values { get => s_values.Value; }
    }
}
