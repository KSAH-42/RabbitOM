using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class RtspHeaderServiceSettings
    {
        private readonly Dictionary<string,RtspHeaderParser> _parsers;

        private readonly HashSet<string> _forbiddenHeaders;




        public RtspHeaderServiceSettings( IEnumerable<RtspHeaderParser> parsers , IEnumerable<string> forbiddenHeaders )
        {
            if ( parsers == null )
            {
                throw new ArgumentNullException( nameof( parsers ) );
            }

            if ( forbiddenHeaders == null )
            {
                throw new ArgumentNullException( nameof( forbiddenHeaders ) );
            }

            _parsers = new Dictionary<string, RtspHeaderParser>( parsers.ToDictionary( parser => parser.Name ) , StringComparer.OrdinalIgnoreCase );

            _forbiddenHeaders = new HashSet<string>( forbiddenHeaders , StringComparer.OrdinalIgnoreCase );

            // TODO: throw exception if forbidden headers are also in the parse collection
        }






        public IReadOnlyDictionary<string,RtspHeaderParser> Parsers
        {
            get => _parsers;
        }

        public IReadOnlyCollection<string> ForbbidenHeaders
        {
            get => _forbiddenHeaders;
        }
    }
}
