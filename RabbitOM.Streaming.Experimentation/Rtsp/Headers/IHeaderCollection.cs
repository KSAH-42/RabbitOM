using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public interface IHeaderCollection : ICollection , IEnumerable , IEnumerable<KeyValuePair<string , string[]>> , IReadOnlyHeaderCollection
    {
        void Add( string name , string value );
        bool TryAdd( string name , string value );
        bool Remove( string name );
        bool RemoveAt( string name , int index );
    }
}
