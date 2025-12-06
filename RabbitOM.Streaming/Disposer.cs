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
        /// <exception cref="ArgumentNullException" />
        public Disposer( Action action )
        {
            _action = action ?? throw new ArgumentNullException( nameof( action ) );
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _action.TryInvoke();
        }
    }
}
