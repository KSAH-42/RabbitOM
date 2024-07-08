using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a class used to perform actions when the dispose method is called or after the using scope.
    /// </summary>
    internal sealed class RTSPDisposeScope : IDisposable
    {
        private readonly Stack<Action> _actions = new Stack<Action>();



        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPDisposeScope()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">the action</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPDisposeScope( Action action )
        {
            AddAction( action );
        }



        /// <summary>
        /// Add an action
        /// </summary>
        /// <param name="action">the action</param>
        /// <exception cref="ArgumentNullException"/>
        public void AddAction( Action action )
        {
            if ( action == null )
            {
                throw new ArgumentNullException( nameof( action ) );
            }

            _actions.Push( action );
        }

        /// <summary>
        /// Remove all pending actions
        /// </summary>
        public void ClearActions()
        {
            _actions.Clear();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            while ( _actions.Count > 0 )
            {
                var action = _actions.Pop();

                action.TryInvoke();
            }
        }
    }
}
