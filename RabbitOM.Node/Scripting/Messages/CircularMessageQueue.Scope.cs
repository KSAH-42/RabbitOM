using System;

namespace RabbitOM.Node.Scripting.Messages
{
    public partial class CircularMessageQueue
    {
        public sealed class Scope : IDisposable
        {
            private readonly CircularMessageQueue _queue;

            public Scope( CircularMessageQueue queue )
            {
                _queue = queue ?? throw new ArgumentNullException(nameof(queue));
            }

            public void Dispose()
            {
                if ( _queue._collection.Count > 0)
                {
                    _queue._eventHandle.Set();
                }
                else
                {
                    _queue._eventHandle.Reset();
                }
            }
        }
    }
}
