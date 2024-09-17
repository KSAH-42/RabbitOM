﻿using System;
using System.Threading;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a thread class
    /// </summary>
    public sealed class RtspThread
    {
        private readonly object _lock;

        private readonly string _name;

        private readonly ManualResetEvent _stopHandle;

        private readonly ManualResetEvent _status;

        private Thread _thread;







        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">the name</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspThread( string name )
        {
            _name       = name ?? string.Empty;
            _lock       = new object();
            _stopHandle = new ManualResetEvent( false );
            _status     = new ManualResetEvent( false );
        }







        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name
        {
            get => _name;
        }

        /// <summary>
        /// Check if the thread has been started
        /// </summary>
        public bool IsStarted
        {
            get => _status.IsSignaled();
        }

        /// <summary>
        /// Check if the thread is actually stopping
        /// </summary>
        public bool IsStopping
        {
            get => _status.IsSignaled() && _stopHandle.IsSignaled();
        }

        /// <summary>
        /// Gets the exit handle
        /// </summary>
        public WaitHandle ExitHandle
        {
            get => _stopHandle;
        }







        /// <summary>
        /// Start a thread
        /// </summary>
        /// <param name="action">the action</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <exception cref="ArgumentNullException"/>
        public bool Start( Action action )
        {
            if ( action == null )
            {
                throw new ArgumentNullException( nameof( action ) );
            }

            if ( _status.IsSignaled() )
            {
                return false;
            }

            lock ( _lock )
            {
                if ( _thread != null || _stopHandle.IsSignaled() )
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

                    _status.TrySet();

                    return true;
                }
                catch ( Exception ex )
                {
                    OnError( ex );
                }
            }

            return false;
        }

        /// <summary>
        /// Stop the thread
        /// </summary>
        public void Stop()
        {
            Stop( Timeout.InfiniteTimeSpan );
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <exception cref="InvalidOperationException"/>
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
                        _status.TryReset();

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

        /// <summary>
        /// Check if the thread can continue it's job
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanContinue()
        {
            return CanContinue( TimeSpan.Zero );
        }

        /// <summary>
        /// Check if the thread can continue it's job
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanContinue( TimeSpan timeout )
        {
            return _status.IsSignaled() && _stopHandle.TryWait( timeout ) == false;
        }

        /// <summary>
        /// Thread function
        /// </summary>
        /// <param name="parameter">the parameter</param>
        private void Processing( object parameter )
        {
            Action routine = parameter as Action;

            _status.TrySet();

            try
            {
                routine?.Invoke();
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }
        }







        /// <summary>
        /// Occurs when an error has been detected
        /// </summary>
        /// <param name="ex">the exception</param>
        private void OnError( Exception ex )
        {
            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
