using System;

namespace RabbitOM.Collections
{
    using RabbitOM.Threading;

    public partial class CircualConcurrentQueue<TElement>
    {
        public sealed class Scope : IDisposable
        {
            private readonly CircualConcurrentQueue<TElement> _queue;

            public Scope(CircualConcurrentQueue<TElement> queue)
            {
                _queue = queue ?? throw new ArgumentNullException(nameof(queue));
            }

            public void Dispose()
            {
                if (_queue._collection.Count > 0)
                {
                    _queue._eventHandle.TrySet();
                }
                else
                {
                    _queue._eventHandle.TryReset();
                }
            }
        }
    }
}
