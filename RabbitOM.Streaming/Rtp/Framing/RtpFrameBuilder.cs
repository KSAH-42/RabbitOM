using System;

namespace RabbitOM.Streaming.Rtp.Framing
{
    public abstract class RtpFrameBuilder : IDisposable
    {
        public event EventHandler<RtpFrameReceivedEventArgs> FrameReceived;


        ~RtpFrameBuilder()
        {
            Dispose( false );
        }



        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        public abstract void Clear();
        public abstract void Write( byte[] buffer );








        protected virtual void Dispose( bool disposing )
        {
        }

        protected virtual void OnFrameReceived( RtpFrameReceivedEventArgs e )
        {
            if ( e == null )
            {
                return;
            }

            FrameReceived?.TryInvoke( this , e );
        }
    }
}