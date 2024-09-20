using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Represent a class used to perform actions when the dispose method is called or after the using scope.
    /// </summary>
    public sealed class DisposeScope : IDisposable
    {
        private readonly Stack<Action> _actions = new Stack<Action>();






        //// <summary>
        /// Add an action
        /// </summary>
        /// <param name="action">the action</param>
        /// <exception cref="ArgumentNullException"/>
        public void AddAction( Action action )
        {
            _actions.Push( action ?? throw new ArgumentNullException( nameof( action ) ) );
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






        /// <summary>
        /// Create an scope
        /// </summary>
        /// <param name="action">the action</param>
        /// <returns>returns an new instance</returns>
        /// <exception cref="ArgumentNullException"/
        public static DisposeScope NewScope( Action action )
        {
            if ( action == null )
            {
                throw new ArgumentNullException( nameof( action ) );
            }

            DisposeScope scope = new DisposeScope();

            scope.AddAction( action );

            return scope;
        }
    }
}
