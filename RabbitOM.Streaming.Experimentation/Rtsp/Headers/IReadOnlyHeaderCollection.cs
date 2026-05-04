using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public interface IReadOnlyHeaderCollection : IEnumerable , IEnumerable<KeyValuePair<string,string>> , IReadOnlyCollection<KeyValuePair<string,string>>
    {
        string this[ string name ] { get; }

        string this[ string name , int index ] { get; }




        IEnumerable<string> AllKeys { get; }




        bool ContainsKey( string name );

        string GetValue( string name );

        string GetValueAt( string name , int index );

        IEnumerable<string> GetValues( string name );

        bool TryGetValue( string name , out string value );

        bool TryGetValueAt( string name , int index , out string value );

        bool TryGetValues( string name , out IEnumerable<string> values );
    }
}
