using RabbitOM.Streaming.Experimentation.Rtsp.Headers;
using System;
using System.Linq;
using System.Text;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels.Readers
{
    public sealed class RtspRequestResponseMessageReader
    {
        private readonly IStream _stream;
        
        public RtspRequestResponseMessageReader( IStream stream )
        {
            _stream = stream ?? throw new ArgumentNullException( nameof( stream ) );
        }


        public string ProtocolName { get; set; } = "RTSP";

        public string Version { get; set; } = "1.0";


        public RtspMessage ReadMessage()
        {
            var startLine = _stream.ReadLine();

            // TODO use regualar expression to parse the start line 
            // it should be the best approach

            throw new NotImplementedException();
        }
    }
}
