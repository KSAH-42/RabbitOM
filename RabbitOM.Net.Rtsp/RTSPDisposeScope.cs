using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a class used to perform actions when the dispose method is called or after the using scope.
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
            AddAction( action ?? throw new ArgumentNullException( nameof( action ) ) );
        }



        /// <summary>
        /// Add an action
        /// </summary>
        /// <param name="action">the action</param>
        public void AddAction( Action action )
        {
            _actions.Insert( 0 , action );
        }

        /// <summary>
        /// Remove all pending actions
        /// </summary>
        public void RemoveAllActions()
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
                action?.TryInvoke();
            }

            _actions.Clear();
        }
    }
}
