/*
 EXPERIMENTATION of the next implementation of the rtp layer

              O P T I M I Z A T I O N S

 For next implementation, optimize can to reduce copy 
 by using array segment to remove Buffer.Copy or similar methods

*/

using System;

namespace RabbitOM.Net.Rtsp.Tests
{
    public abstract class RTPFrameBuilder : IDisposable
    {
        ~RTPFrameBuilder()
            => Dispose();

        public abstract bool TryAddPacket( byte[] buffer );
        public abstract bool CanBuildFrame();
        public abstract RTPFrame BuildFrame();
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }
        protected abstract void Dispose( bool disposing );
        public abstract void Clear();
    }
}