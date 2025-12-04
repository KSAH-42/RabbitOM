using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Diagnostics.Counters
{
    public abstract class RtspClientCounter : IDisposable
    {
        public event EventHandler Started;
        public event EventHandler Stopped;
        public event EventHandler Reseted;
        public event EventHandler Updated;
        




        protected RtspClientCounter( string name )
            : this ( name , null )
        {
        }
        
        protected RtspClientCounter( string name , string description )
        {
            throw new NotImplementedException("this is not finished");
        }

        ~RtspClientCounter()
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
            Started?.Invoke( this , e );
        }

        protected virtual void OnStopped( EventArgs e )
        {
            Stopped?.Invoke( this , e );
        }

        protected virtual void OnReset( EventArgs e )
        {
            Reseted?.Invoke( this , e );
        }

        protected virtual void OnUpdated( EventArgs e )
        {
            Updated?.Invoke( this , e );
        }
    }
}
