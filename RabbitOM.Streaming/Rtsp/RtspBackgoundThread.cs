using System;
using System.Threading;

namespace RabbitOM.Streaming.Rtsp
{
    internal sealed class RtspBackgoundThread
    {
        private readonly object _lock = new object();
        
        private readonly string _name;
        
        private readonly EventWaitHandle _exitHandle;
        
        private readonly State _state;
        
        private Thread _thread;







        public RtspBackgoundThread( string name )
        {
            _name       = name ?? string.Empty;
            _exitHandle = new EventWaitHandle( false , EventResetMode.ManualReset );
            _state      = new State();
        }







        public string Name 
        { 
            get => _name; 
        }

        public EventWaitHandle ExitHandle 
        { 
            get => _exitHandle; 
        }
        
        public bool Status
        { 
            get => _state.GetStatus(); 
        }







        public bool Start( ThreadStart threadStart )
        {
            if ( threadStart == null )
            {
                return false;
            }

            lock ( _lock )
            {
                if ( _thread != null || !_exitHandle.Reset() )
                {
                    return false;
                }

                _thread = new Thread( threadStart );
                _thread.IsBackground = true;
                _thread.Name = _name;
                _thread.Start();

                _state.SetStatus( true );

                return true;
            }
        }

        public bool Stop()
        {
            return Stop( Timeout.InfiniteTimeSpan );
        }

        public bool Stop( TimeSpan timeout )
        {
            lock ( _lock )
            {
                using ( Scope.NewScope( this ) )
                {
                    return _thread?.Join( timeout ) ?? false;
                }
            }
        }

        public void Abort()
        {
            lock ( _lock )
            {
                using ( Scope.NewScope( this ) )
                {
                    _thread?.Abort();
                }
            }
        }







        struct Scope : IDisposable
        {
            private readonly RtspBackgoundThread _instance;

            public Scope( RtspBackgoundThread instance )
            {
                _instance = instance;
                _instance._exitHandle.Set();
            }

            public void Dispose()
            {
                _instance._exitHandle.Reset();
                _instance._thread = null;
                _instance._state.SetStatus( false );
            }

            public static Scope NewScope( RtspBackgoundThread instance )
            {
                if ( instance._thread?.ManagedThreadId == Thread.CurrentThread.ManagedThreadId )
                {
                    throw new InvalidOperationException();
                }

                return new Scope( instance );
            }
        }







        sealed class State
        {
            private long _status;

            public bool GetStatus()
            {
                return Interlocked.Read( ref _status ) != 0;
            }

            public void SetStatus( bool status )
            {
                Interlocked.Exchange( ref _status , status ? 1 : 0 );
            }
        }
    }
}
