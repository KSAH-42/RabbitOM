using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.RtspToBeRemoved.Headers
{
    public interface IReadOnlyHeaderCollection : IEnumerable , IEnumerable<KeyValuePair<string,RtspHeaderValue[]>> , IReadOnlyCollection<KeyValuePair<string,RtspHeaderValue[]>>
    {
        RtspHeaderValue this[ string name ] { get; }
        RtspHeaderValue this[ string name , int index ] { get; }


        string[] AllKeys { get; }
        bool IsEmpty { get; }



        void SetValue<TValue>( string key , TValue value ) where TValue : RtspHeaderValue;
        TValue GetValue<TValue>( string name ) where TValue : RtspHeaderValue;
        TValue GetValue<TValue>( string name , Func<TValue> factory ) where TValue : RtspHeaderValue;
        RtspHeaderValue[] GetValues( string name );
        bool ContainsKey( string name );
        bool TryGetValue( string name , out RtspHeaderValue value );
        bool TryGetValueAt( string name , int index , out RtspHeaderValue value );
        bool TryGetValues( string name , out RtspHeaderValue[] values );
    }
}
