using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class RtspHeaderRegistrySettings
    {
        public RtspHeaderRegistrySettings( IEnumerable<RtspHeaderParser> parsers , IEnumerable<string> forbiddenHeaders )
        {
            RegisteredParsers = new Dictionary<string, RtspHeaderParser>( parsers != null ? parsers.ToDictionary( parser => parser.Name ) : throw new ArgumentNullException( nameof( parsers ) ) , StringComparer.OrdinalIgnoreCase );

            ForbiddenHeaders = new HashSet<string>( forbiddenHeaders ?? throw new ArgumentNullException( nameof( forbiddenHeaders ) ) , StringComparer.OrdinalIgnoreCase );
        }

        public IReadOnlyDictionary<string,RtspHeaderParser> RegisteredParsers { get; }

        public IReadOnlyCollection<string> ForbiddenHeaders { get; }
    }
}
