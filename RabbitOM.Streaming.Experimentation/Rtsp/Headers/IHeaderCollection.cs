using System;
using System.Collections;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public interface IHeaderCollection : ICollection
    {
        void Add( string name , string value );

        bool TryAdd( string name , string value );

        bool Remove( string name );

        bool RemoveAt( string name , int index );

        void Clear();
    }
}
