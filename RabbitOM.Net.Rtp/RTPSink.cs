/*
 EXPERIMENTATION of the next implementation of the rtp layer

                    IMPLEMENTATION  NOT COMPLETED

*/

using System;

namespace RabbitOM.Net.Rtp
{
    // TODO: maybe in this class we can add stats objects
    public abstract class RTPSink : IDisposable
    {
        public event EventHandler<RTPPacketReceivedEventArgs> PacketReceived;
        
        public event EventHandler<RTPFrameReceivedEventArgs> FrameReceived;


        ~RTPSink()
        {
            Dispose( false );
        }





        public abstract void Write( byte[] packet );
        
        public abstract void Reset();

        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
        }






        protected virtual void OnPacketReceived( RTPPacketReceivedEventArgs e )
        {
            PacketReceived?.Invoke( this , e );
        }

        protected virtual void OnFrameReceived( RTPFrameReceivedEventArgs e )
        {
            FrameReceived?.Invoke( this , e );
        }
    }
}