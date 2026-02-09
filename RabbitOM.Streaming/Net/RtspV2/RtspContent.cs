using System;

namespace RabbitOM.Streaming.Net.RtspV2
{
    public abstract class RtspContent
    {
        public abstract void AddHeader( string name , string value );

        public abstract void RemoveHeader( string name );

        public abstract void RemoveHeaders();

        public abstract void WriteBody( string value );

        public abstract void ClearBody();
    }
}
