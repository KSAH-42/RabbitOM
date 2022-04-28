using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a parser class
    /// </summary>
    public sealed class RTSPDisposeScope : IDisposable
    {
        private readonly List<Action> _actions = new List<Action>();



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
            PushFront( action ?? throw new ArgumentNullException( nameof( action ) ) );
        }



        /// <summary>
        /// Add an action
        /// </summary>
        /// <param name="action">the action</param>
        public void PushBack( Action action )
        {
            if ( action == null )
            {
                return;
            }

            _actions.Add( action );
        }

        /// <summary>
        /// Add an action
        /// </summary>
        /// <param name="action">the action</param>
        public void PushFront( Action action )
        {
            if ( action == null )
            {
                return;
            }

            _actions.Insert( 0 , action );
        }

        /// <summary>
        /// Clear all pending actions
        /// </summary>
        public void Clear()
        {
            _actions.Clear();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            foreach ( var action in _actions )
            {
                try
                {
                    action.Invoke();
                }
                catch ( Exception ex )
                {
                    System.Diagnostics.Debug.WriteLine( ex );
                }
            }
        }
    }
}
