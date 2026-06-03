using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Transports.Channels
{
    public sealed partial class RtspHeaderCollection
    {
        public struct Enumerator : IEnumerator<KeyValuePair<string , string>>
        {
            public Enumerator( RtspHeaderCollection collection )
            {
            }

            object IEnumerator.Current => Current;

            public KeyValuePair<string , string> Current => throw new NotImplementedException();

            public bool MoveNext()
            {
                throw new NotImplementedException();
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
    }
}
