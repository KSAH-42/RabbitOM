using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent an action queue
    /// </summary>
    internal sealed class RTSPActionQueue : RTSPBaseQueue<Action>
    {
        private readonly object              _lock              = null;

        private readonly RTSPEventWaitHandle _handle            = null;

        private readonly Queue<Action>       _collection        = null;

        private readonly Scope               _scope             = null;







        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPActionQueue()
        {
            _lock            = new object();
            _collection      = new Queue<Action>();
            _handle          = new RTSPEventWaitHandle();
            _scope           = new Scope( this );
        }







        /// <summary>
        /// Gets the sync root
        /// </summary>
        public override object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets the number of elements
        /// </summary>
        public override int Count
        {
            get
            {
                lock ( _lock )
                {
                    return _collection.Count;
                }
            }
        }

        /// <summary>
        /// Check if the queue is empty
        /// </summary>
        public override bool IsEmpty
        {
            get
            {
                lock ( _lock )
                {
                    return _collection.Count <= 0;
                }
            }
        }

        /// <summary>
        /// Gets the handle
        /// </summary>
        protected override RTSPEventWaitHandle Handle
        {
            get => _handle;
        }








        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>returns an enumerator</returns>
        protected override IEnumerator<Action> BaseGetEnumerator()
        {
            lock ( _lock )
            {
                return _collection.ToList().GetEnumerator();
            }
        }

        /// <summary>
        /// Check if the queue contains some elements
        /// </summary>
        /// <returns>returns true for a success, otherwise false.</returns>
        public override bool Any()
        {
            lock ( _lock )
            {
                return _collection.Count > 0;
            }
        }

        /// <summary>
        /// Post an element
        /// </summary>
        /// <param name="action">the action</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public override bool Enqueue( Action action )
        {
            if ( action == null )
			{
                return false;
			}
        
            lock ( _lock )
            {
                using (_scope )
                {
                    _collection.Enqueue( action );

                    return true;
                }
            }
        }

        /// <summary>
        /// Dequeue an action
        /// </summary>
        /// <returns>must returns an instance</returns>
        public override Action Dequeue()
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    return _collection.Dequeue();
                }
            }
        }

        /// <summary>
        /// Dequeue an action
        /// </summary>
        /// <param name="result">the action</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public override bool TryDequeue( out Action result )
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    result = _collection.Count > 0 ? _collection.Dequeue() : null;

                    return result != null;
                }
            }
        }

        /// <summary>
        /// Clear the queue
        /// </summary>
        public override void Clear()
        {
            lock ( _lock )
            {
                using ( _scope )
                {
                    _collection.Clear();
                }
            }
        }
    }
}
