using System;
using System.Threading;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a thread class
    /// </summary>
    public sealed class RTSPThread
    {
        private readonly string           _name        = string.Empty;

        private readonly EventWaitHandle  _eventExit   = null;

        private Thread                    _thread      = null;

        private Action                    _routine     = null;

        private Exception                 _exception   = null;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">the name</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPThread( string name )
        {
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                throw new ArgumentException( nameof( name ) );
            }

            _name = name;
            _eventExit = new EventWaitHandle( false , EventResetMode.ManualReset );
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
        public EventWaitHandle ExitHandle
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
        /// Gets / Sets a custom object to associate to the thread instance
        /// </summary>
        public object Tag
        {
            get;
            set;
        }




        /// <summary>
        /// Start a thread
        /// </summary>
        /// <param name="action">the action</param>
        /// <returns>returns true for a success, otherwise false</returns>
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

            try
            {
                if ( !_eventExit.Reset() )
                {
                    return false;
                }

                var thread = new Thread( Processing )
                {
                    Name         = _name,
                    IsBackground = true ,
                };

                _routine = action;
                _thread = thread;

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

            if (thread.ManagedThreadId == Thread.CurrentThread.ManagedThreadId )
            {
                throw new InvalidOperationException();
            }

            try
            {
                _eventExit.Set();
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
        public bool Join( TimeSpan timeout )
        {
            var thread = _thread;

            if ( thread == null)
            {
                return true;
            }

            if ( thread.ManagedThreadId == Thread.CurrentThread.ManagedThreadId)
            {
                throw new InvalidOperationException();
            }

            try
            {
                _eventExit.Set();

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
        public bool Shutdown()
        {
            var thread = _thread;

            if ( thread == null)
            {
                return true;
            }

            if ( thread.ManagedThreadId == Thread.CurrentThread.ManagedThreadId)
            {
                throw new InvalidOperationException();
            }

            try
            {
                return _eventExit.Set();
            }
            catch (Exception ex)
            {
                OnError(ex);
            }

            return false;
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

            if ( thread.ManagedThreadId == Thread.CurrentThread.ManagedThreadId )
            {             
                throw new InvalidOperationException();
            }

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

            try
            {
                return !_eventExit.WaitOne( timeout );
            }
            catch ( Exception ex )
            {
                OnError( ex );
            }

            return false;
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
        /// Occurs when an error has been detected
        /// </summary>
        /// <param name="ex">the exception</param>
        private void OnError( Exception ex )
        {
            _exception = ex;
        }
    }
}
