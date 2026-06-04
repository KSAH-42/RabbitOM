using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed partial class RtspHeaderCollection
    {
        public struct Enumerator : IEnumerator<KeyValuePair<string , string>>
        {
            private IEnumerator<KeyValuePair<string,List<string>>> _enumerator;
            private IEnumerator<string> _valueEnumerator;
            private KeyValuePair<string , string> _value;




            internal Enumerator( RtspHeaderCollection headers )
            {
                _enumerator = headers._collection.GetEnumerator();
                _valueEnumerator = null;
                _value = default;
            }





            object IEnumerator.Current => _value;

            public KeyValuePair<string , string> Current => _value;





            public bool MoveNext()
            {
                if ( _valueEnumerator == null || ! _valueEnumerator.MoveNext() )
                {
                    _valueEnumerator = null;

                    while( _enumerator.MoveNext() )
                    {
                        var valueEnumerator = _enumerator.Current.Value.GetEnumerator();

                        if ( valueEnumerator.MoveNext() )
                        {
                            _valueEnumerator = valueEnumerator;
                            break;
                        }
                    }
                }

                if ( _valueEnumerator == null )
                {
                    return false;
                }

                _value = new KeyValuePair<string, string>( _enumerator.Current.Key , _valueEnumerator.Current );

                return true;
            }

            public void Reset()
            {
                _valueEnumerator = null;
                _enumerator.Dispose();
                _value = default;
            }

            public void Dispose()
            {
                _valueEnumerator?.Dispose();
                _enumerator.Dispose();
            }
        }
    }
}
