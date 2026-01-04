using System;

namespace RabbitOM.Streaming
{
    /// <summary>
    /// Represent a type used to call a delegate on dispose
    /// </summary>
    public struct Disposer : IDisposable
    {
        private readonly Action _action;

        /// <summary>
        /// Initialize a new instance of the dispose bag
        /// </summary>
        /// <param name="action">action</param>
        public Disposer( Action action )
        {
            _action = action ?? throw new ArgumentNullException( nameof( action ) );
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            System.Diagnostics.Debug.Assert( _action != null , "it seems that the default ctor has been calls, that not allowed" );

            _action.TryInvoke();
        }
    }
}
