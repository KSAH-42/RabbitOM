using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public interface IReadOnlyNameValueCollection : IReadOnlyCollection<KeyValuePair<string ,string[]>>
    {
        string[] this[string key] { get; }
        string this[string key, int index ] { get; }


        string[] AllKeys { get; }


        bool ContainsKey( string key );
        bool TryGetValue( string key , out string result );
        bool TryGetValueAt( string key , int index , out string result );
        bool TryGetValues( string key , out string[] result );
    }
}
