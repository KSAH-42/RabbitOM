using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp2BeRemoved.Headers
{
    public interface IHeaderCollection : ICollection , IEnumerable , IEnumerable<KeyValuePair<string , RtspHeaderValue[]>> , IReadOnlyHeaderCollection
    {
        void Add( string name , string value );
        void Add( string name , RtspHeaderValue value );
        bool Remove( string name );
        bool RemoveAt( string name , int index );
        bool TryAdd( string name , string value );
        bool TryAdd( string name , RtspHeaderValue value );
    }
}
