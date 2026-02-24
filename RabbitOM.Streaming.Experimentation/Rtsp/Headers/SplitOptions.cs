using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal enum SplitOptions
    {
        None = 0 ,

        RemoveEmptyEntries = 1, 

        TrimEntries = 2,  // only present in net core (see System.StringSplitOptions)
    }
}
