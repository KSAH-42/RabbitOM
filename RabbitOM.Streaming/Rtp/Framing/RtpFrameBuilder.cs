using System;

namespace RabbitOM.Streaming.Rtp.Framing
{
    public abstract class RtpFrameBuilder : IDisposable
    {
        ~RtpFrameBuilder()
        {
            Dispose( false );
        }

        public abstract object SyncRoot { get; }

        public abstract bool TryAddPacket( byte[] buffer );

        public abstract bool CanBuildFrame();

        public abstract RtpFrame BuildFrame();

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }
        
        protected virtual void Dispose( bool disposing )
        {
        }
        
        public abstract void Clear();
    }
}