using System;

namespace RabbitOM
{
    public struct Disposer : IDisposable
    {
        private readonly Action _action;

        public Disposer( Action action )
        {
            _action = action ?? throw new ArgumentNullException( nameof( action ) );
        }

        public void Dispose()
        {
            System.Diagnostics.Debug.Assert( _action != null , "it seems that the default ctor has been calls, that not allowed" );

            _action.TryInvoke();
        }
    }
}
