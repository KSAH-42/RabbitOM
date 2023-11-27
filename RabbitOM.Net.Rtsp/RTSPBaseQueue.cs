using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the base generic queue
    /// </summary>
    /// <typeparam name="TElement">the element type</typeparam>
    internal abstract partial class RTSPBaseQueue<TElement> 
        : IEnumerable 
        , IEnumerable<TElement>
        , IReadOnlyCollection<TElement>
        
        where TElement : class

    {
        /// <summary>
        /// Gets the sync root
        /// </summary>
        public abstract object SyncRoot
        {
            get;
        }

        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public abstract int Count
        {
            get;
        }

        /// <summary>
        /// Check if the queue is empty
        /// </summary>
        public abstract bool IsEmpty
        {
            get;
        }

        /// <summary>
        /// Gets the handle
        /// </summary>
        protected abstract RTSPEventWaitHandle Handle
        {
            get;
        }







        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait(RTSPBaseQueue<TElement> queue)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            return queue.Handle.Wait();
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait(RTSPBaseQueue<TElement> queue, int timeout)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            return queue.Handle.Wait(timeout);
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait(RTSPBaseQueue<TElement> queue, EventWaitHandle cancellationHandle)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            return queue.Handle.Wait(cancellationHandle);
        }

        /// <summary>
        /// Wait until an element has been push to the queue
        /// </summary>
        /// <param name="queue">the queue</param>
        /// <param name="timeout">the timeout</param>
        /// <param name="cancellationHandle">the cancellation handle</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool Wait(RTSPBaseQueue<TElement> queue, int timeout, EventWaitHandle cancellationHandle)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            if (cancellationHandle == null)
            {
                throw new ArgumentNullException(nameof(cancellationHandle));
            }

            return queue.Handle.Wait(timeout, cancellationHandle);
        }








        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return BaseGetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an enumerator</returns>
        public IEnumerator<TElement> GetEnumerator()
        {
            return BaseGetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an enumerator</returns>
        protected abstract IEnumerator<TElement> BaseGetEnumerator();

        /// <summary>
        /// Check if the queue contains some elements
        /// </summary>
        /// <returns>returns true for a success, otherwise false.</returns>
        public abstract bool Any();

        /// <summary>
        /// Post an element
        /// </summary>
        /// <param name="action">the action</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public abstract bool Enqueue(TElement action );

        /// <summary>
        /// Dequeue an action
        /// </summary>
        /// <returns>must returns an instance</returns>
        public abstract TElement Dequeue();

        /// <summary>
        /// Dequeue an action
        /// </summary>
        /// <param name="result">the action</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public abstract bool TryDequeue( out TElement result );

        /// <summary>
        /// Clear the queue
        /// </summary>
        public abstract void Clear();        
    }
}
