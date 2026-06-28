using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed partial class RtspHeaderRegistry
    {
        public struct Enumerator : IEnumerator<KeyValuePair<string,string>>
        {
            private readonly IEnumerator<KeyValuePair<string,RtspHeaderRegistryBucket>> _enumerator;
            private IEnumerator<object> _valuesEnumerator;
            private KeyValuePair<string,string> _current;




            internal Enumerator( RtspHeaderRegistry registry )
            {
                _enumerator = registry._headers.GetEnumerator();
                _valuesEnumerator = null;
                _current = default;
            }




            public object Current
            {
                get => _current;
            }

            KeyValuePair<string,string> IEnumerator<KeyValuePair<string,string>>.Current
            {
                get => _current ;
            }




            public bool MoveNext()
            {
                while ( true )
                {
                    if ( _valuesEnumerator != null && _valuesEnumerator.MoveNext() )
                    {
                        break;
                    }

                    if ( ! _enumerator.MoveNext() )
                    {
                        return false;
                    }

                    _valuesEnumerator = _enumerator.Current.Value.Values.GetEnumerator();
                }

                _current = new KeyValuePair<string,string>( _enumerator.Current.Key , _valuesEnumerator.Current?.ToString() ?? string.Empty );

                return true;
            }

            public void Reset()
            {
                _enumerator.Reset();
                _valuesEnumerator = null;
                _current = default;
            }

            public void Dispose()
            {
                _enumerator.Dispose();
                _valuesEnumerator?.Dispose();
                _valuesEnumerator = null;
                _current = default;
            }
        }
    }
}
