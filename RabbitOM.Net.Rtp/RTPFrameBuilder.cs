/*
 EXPERIMENTATION of the next implementation of the rtp layer

                    IMPLEMENTATION  NOT COMPLETED


*/

using System;

namespace RabbitOM.Net.Rtp
{
    public abstract class RTPFrameBuilder : IDisposable
    {
        ~RTPFrameBuilder()
        {
            Dispose( false );
        }

        public abstract bool TryAddPacket( byte[] buffer );

        public abstract bool CanBuildFrame();

        public abstract RTPFrame BuildFrame();

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