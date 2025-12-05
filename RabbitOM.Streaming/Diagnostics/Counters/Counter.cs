using System;

namespace RabbitOM.Streaming.Diagnostics.Counters
{
    public abstract class Counter : IDisposable
    {
        public event EventHandler Started;
        public event EventHandler Stopped;
        public event EventHandler Reseted;
        public event EventHandler Updated;
        




        protected Counter( string name )
            : this ( name , null )
        {
        }
        
        protected Counter( string name , string description )
        {
            ExceptionHelper.ThrowOnRelease("this is not finished");
        }

        ~Counter()
        {
            Dispose( false );
        }
        




        public string Name { get; }
        
        public string Description { get; }
        




        public bool IsStarted()
        {
            throw new NotImplementedException();
        }
        
        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public virtual void Reset()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }
        




        protected virtual void OnStarted( EventArgs e )
        {
            Started?.TryInvoke( this , e );
        }

        protected virtual void OnStopped( EventArgs e )
        {
            Stopped?.TryInvoke( this , e );
        }

        protected virtual void OnReset( EventArgs e )
        {
            Reseted?.TryInvoke( this , e );
        }

        protected virtual void OnUpdated( EventArgs e )
        {
            Updated?.TryInvoke( this , e );
        }
    }
}
