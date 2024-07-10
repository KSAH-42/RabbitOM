using System;
using System.Threading;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a thread class
    /// </summary>
    public sealed class RtspThread
    {
        private readonly string              _name        = string.Empty;

        private readonly RtspEventWaitHandle _eventExit   = null;

        private Thread                       _thread      = null;

        private Action                       _routine     = null;

        private Exception                    _exception   = null;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">the name</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspThread( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            _name = name;
            _eventExit = new RtspEventWaitHandle();
        }





        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name
        {
            get => _name;
        }

        /// <summary>
        /// Gets the internal exception
        /// </summary>
        public Exception Exception
        {
            get => _exception;
        }

        /// <summary>
        /// Gets the exit handle
        /// </summary>
        public RtspEventWaitHandle ExitHandle
        {
            get => _eventExit;
        }

        /// <summary>
        /// Check if the thread has been started
        /// </summary>
        public bool IsStarted
        {
            get => _thread != null;
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

            if ( _thread != null )
            {
                return false;
            }

            if ( !_eventExit.Reset() )
            {
                return false;
            }

            try
            {
                var thread = new Thread( Processing )
                {
                    Name         = _name,
                    IsBackground = true ,
                };

                _routine = action;
                _thread  = thread;

                _thread.Start();

                return true;
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
        }

        /// <summary>
        /// Stop the thread
        /// </summary>
        public void Stop()
        {
            var thread = _thread;

            if (thread == null )
            {
                return;
            }

            EnsureCallingThread( thread );

            _eventExit.Set();

            try
            {
                thread.Join();
            }
            catch ( Exception ex )
            {
                OnError( ex );

                thread.Abort();
            }
            finally
            {
                _thread = null;
            }
        }

        /// <summary>
        /// Join
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <exception cref="InvalidOperationException"/>
        public bool Join( TimeSpan timeout )
        {
            var thread = _thread;

            if ( thread == null)
            {
                return true;
            }

            EnsureCallingThread( thread );

            _eventExit.Set();

            try
            {
                return thread.Join( timeout );
            }
            catch (Exception ex)
            {
                OnError(ex);
            }

            return false;
        }

        /// <summary>
        /// Alert the thread to leave/stop it's execution
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        /// <exception cref="InvalidOperationException"/>
        public bool Shutdown()
        {
            var thread = _thread;

            if ( thread == null)
            {
                return true;
            }

            EnsureCallingThread( thread );

            return _eventExit.Set();
        }

        /// <summary>
        /// Abort the thread
        /// </summary>
        public void Abort()
        {
            var thread = _thread;

            if ( thread == null )
            {
                return;
            }

            EnsureCallingThread( thread );

            _eventExit.Set();

            try
            {
                thread.Abort();
            }
            catch ( Exception ex )
            {
                _exception = ex;
            }
            finally
            {
                _thread = null;
            }
        }

        /// <summary>
        /// Check if the thread can continue it's job
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanContinue()
        {
            return CanContinue( 0 );
        }

        /// <summary>
        /// Check if the thread can continue it's job
        /// </summary>
        /// <param name="milliseconds">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanContinue( int milliseconds )
        {
            return CanContinue( TimeSpan.FromMilliseconds( milliseconds > 0 ? milliseconds : 0 ) );
        }

        /// <summary>
        /// Check if the thread can continue it's job
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanContinue( TimeSpan timeout )
        {
            if ( _thread == null )
            {
                return false;
            }

            return ! _eventExit.Wait( timeout );
        }

        /// <summary>
        /// Thread function
        /// </summary>
        private void Processing()
        {
            try
            {
                _routine?.Invoke();
            }
            catch ( Exception ex )
            {
                _exception = ex;
            }
        }





        /// <summary>
        /// Ensure that we are not make a call the specific thread.
        /// For instance, this method ensure that we are not invoking the stop method inside the thread, like a thread that want to stop it self using the public method of this class.
        /// </summary>
        /// <param name="thread">the thread</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="InvalidOperationException"/>
        private static void EnsureCallingThread( Thread thread )
        {
            if (thread == null)
            {
                throw new ArgumentNullException( nameof( thread ) );
            }

            if (thread.ManagedThreadId == Thread.CurrentThread.ManagedThreadId)
            {
                throw new InvalidOperationException();
            }
        }






        /// <summary>
        /// Occurs when an error has been detected
        /// </summary>
        /// <param name="ex">the exception</param>
        private void OnError( Exception ex )
        {
            _exception = ex;
        }
    }
}
