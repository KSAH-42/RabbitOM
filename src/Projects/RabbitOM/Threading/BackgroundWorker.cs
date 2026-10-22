/*
 It's a wrapper about thread, Win32 programming a starting a thread required to call the CreateThread method,
and passing SID data struct for OS permission, a pointer to thread function, etc... this will an opaque pointer
call handle. behind this a data structure, it highly important to CloseHandle to avoid leak.

But wait few minutes, most of programmer use bool to alert the leave it's compute. It's is true a poor approch.
According to Microsoft Press, and to Jefrey Ritcher who mainly write the a book for Win32 programming, discuss about the best to win32 thread (not pthread).
A thread is unit a execution flow where the access of the cpu is managed by something like a round robbin algorithm using a quantum time where the quantum time is calculating about the OS properties and the type of CPU.
And the quantum time is never fixed, it's quantum it may be possible that time variation occurs. It's look like a field times. and you may understand why synchronization objects are more usefull instead of using bool.
The recommended way to stop win32 thread is to use win32 synchronization technics with CreateEvent, SetEvent, ResetEvent, WaitForSingleObject...
take a look on cancelationtokensource , there an event synchronization used.
The other reason, we don't use task here, because task scheduler rely in thread pool, and the size of the thread can be changed by a third party libray, it may be possible to reduce the size of thread pool.
I repeat, avoid using boolean to stop a thread, it's too dangerous for mission critical applications.

Use synchronization objects instead.
*/

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
            return _startHandle.IsSet && ! _stopHandle.TryWait( timeout );
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
