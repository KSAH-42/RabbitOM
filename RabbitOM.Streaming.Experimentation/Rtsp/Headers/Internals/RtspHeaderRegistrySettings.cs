using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class RtspHeaderRegistrySettings
    {
        public RtspHeaderRegistrySettings( IEnumerable<RtspHeaderParser> parsers , IEnumerable<string> forbiddenHeaders )
        {
            if ( parsers == null )
            {
                throw new ArgumentNullException( nameof( parsers ) );
            }

            if ( forbiddenHeaders == null )
            {
                throw new ArgumentNullException( nameof( forbiddenHeaders ) );
            }

            RegisteredParsers = new Dictionary<string, RtspHeaderParser>( parsers.ToDictionary( parser => parser.Name ) , StringComparer.OrdinalIgnoreCase );

            ForbiddenHeaders = new HashSet<string>( forbiddenHeaders , StringComparer.OrdinalIgnoreCase );
        }

        public IReadOnlyDictionary<string,RtspHeaderParser> RegisteredParsers { get; }

        public IReadOnlyCollection<string> ForbiddenHeaders { get; }
    }
}
