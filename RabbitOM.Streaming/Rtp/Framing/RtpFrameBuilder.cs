using System;

namespace RabbitOM.Streaming.Rtp.Framing
{

    // Normally the builder pattern must expose a build method
    // The problem is that the frame must be build at a special time
    // That's why a event handler has been introduce
    // Try pattern can be used, to solve the problem 
    // Like bool TryBuild( byte[] buffer , out RtpFrame result );
    
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
            FrameReceived?.TryInvoke( this , e );
        }
    }
}