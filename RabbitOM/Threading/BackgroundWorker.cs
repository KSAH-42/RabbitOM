using System;
using System.Threading;

namespace RabbitOM.Threading
{
    public sealed class BackgroundWorker
    {
        private readonly object _lock;

        private readonly string _name;

        private readonly ManualResetEventSlim _startHandle;

        private readonly ManualResetEventSlim _stopHandle;

        private Thread _thread;




        public BackgroundWorker( string name )
        {
            _name       = name ?? string.Empty;
            _lock       = new object();
            _startHandle= new ManualResetEventSlim( false );
            _stopHandle = new ManualResetEventSlim( false );
        }





        public string Name
        {
            get => _name;
        }

        public bool IsStarted
        {
            get => _startHandle.IsSet;
        }

        public bool IsStopping
        {
            get => _startHandle.IsSet && _stopHandle.IsSet;
        }

        public WaitHandle ExitHandle
        {
            get => _stopHandle.WaitHandle;
        }





        public bool Start( Action action )
        {
            if ( action == null )
            {
                throw new ArgumentNullException( nameof( action ) );
            }

            lock ( _lock )
            {
                if ( _thread != null )
                {
                    return false;
                }

                try
                {
                    var thread = new Thread( Processing )
                    {
                        Name         = _name ,
                        IsBackground = true  ,  
                    };

                    thread.Start( action );

                    _thread = thread;
                    
                    _startHandle.TrySet();

                    return true;
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }
            }

            return false;
        }

        public void Stop()
        {
            Stop( Timeout.InfiniteTimeSpan );
        }

        public bool Stop( TimeSpan timeout )
        {
            lock ( _lock )
            {
                if ( _thread?.ManagedThreadId == Thread.CurrentThread.ManagedThreadId )
                {
                    throw new InvalidOperationException();
                }

                try
                {
                    _stopHandle.Set();

                    if ( _thread == null || _thread.Join( timeout ) )
                    {
                        _stopHandle.TryReset();
                        _startHandle.TryReset();

                        _thread = null;

                        return true;
                    }
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }
            }

            return false;
        }

        public bool CanContinue()
        {
            return CanContinue( TimeSpan.Zero );
        }

        public bool CanContinue( TimeSpan timeout )
        {
            return _startHandle.IsSet && _stopHandle.TryWait( timeout ) == false;
        }

        private void Processing( object parameter )
        {
            Action routine = parameter as Action;

            _startHandle.TrySet();

            try
            {
                routine?.Invoke();
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }
        }





        private void OnError( Exception ex )
        {
            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
