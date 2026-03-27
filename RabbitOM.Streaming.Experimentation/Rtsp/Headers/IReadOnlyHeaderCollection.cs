using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public interface IReadOnlyHeaderCollection : IEnumerable , IEnumerable<KeyValuePair<string,string[]>> , IReadOnlyCollection<KeyValuePair<string,string[]>>
    {
        string this[ string name ] { get; }
        string this[ string name , int index ] { get; }


        string[] AllKeys { get; }
        bool IsEmpty { get; }



        string GetValue( string name );
        string[] GetValues( string name );
        bool ContainsKey( string name );
        bool TryGetValue( string name , out string value );
        bool TryGetValueAt( string name , int index , out string value );
        bool TryGetValues( string name , out string[] values );
    }
}
