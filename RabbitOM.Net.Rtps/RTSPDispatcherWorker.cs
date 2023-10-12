using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent a dispatcher queue
    /// </summary>
    public sealed class RTSPDispatcherWorker
    {
        private readonly RTSPThread            _thread      = new RTSPThread( "Worker" );

        private readonly RTSPDispatcher        _dispathcher = new RTSPDispatcher();

        private readonly RTSPDispatcherInvoker _invoker     = new RTSPDispatcherInvoker();




        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _dispathcher.SyncRoot;
        }

        /// <summary>
        /// Check if the worker can be started
        /// </summary>
        public bool IsStarted
        {
            get => _thread.IsStarted;
        }






        /// <summary>
        /// Start the worker
        /// </summary>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Start()
        {
           return _thread.Start(() =>
           {
               while ( _thread.CanContinue() )
			   {
                   if ( _dispathcher.Wait( TimeSpan.FromSeconds( 10 ) , _thread.ExitHandle ) )
				   {
                       while (_dispathcher.Dequeue(out Action action))
                       {
                           _invoker.TryInvoke(action);
                       }
                   }        
               }
           });
        }

        /// <summary>
        /// Stop the worker
        /// </summary>
        public void Stop()
        {
            _dispathcher.Clear();
            _thread.Stop();
        }

        /// <summary>
        /// Check if the thread can continue it's job
        /// </summary>
        /// <param name="milliseconds">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanContinue(int milliseconds)
        {
            return _thread.CanContinue(milliseconds);
        }

        /// <summary>
        /// Check if the thread can continue it's job
        /// </summary>
        /// <param name="timeout">the timeout</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool CanContinue(TimeSpan timeout)
        {
            return _thread.CanContinue(timeout);
        }

        /// <summary>
        /// Post an action
        /// </summary>
        /// <param name="action">the action</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public bool Post( Action action )
        {
            return _dispathcher.Enqueue(action);
        }
    }
}
