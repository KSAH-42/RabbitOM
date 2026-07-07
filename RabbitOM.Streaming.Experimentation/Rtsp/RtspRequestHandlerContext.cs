using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp
{
    public sealed class RtspRequestHandlerContext
    {
        public IDictionary<string,object> Parameters { get; } = new Dictionary<string, object>( StringComparer.OrdinalIgnoreCase );

        public void Abort( string message = "operation aborted" )
        {
            throw new OperationCanceledException( message );
        }
    }
}
