using System;

namespace RabbitOM.Streaming.Rtp
{
    public sealed class DefaultRTPSink : RTPSink
    {
        /// TODO: use the DIP and replace with an interface
        
        private readonly DefaultRTPFrameBuilder _builder = new DefaultRTPFrameBuilder();

        public override void Write( byte[] data )
        {
            if ( ! _builder.TryAddPacket( data ) )
            {
                return;
            }

            OnPacketReceived( new RTPPacketReceivedEventArgs( _builder.LastPacket ) );

            RTPFrame frame = null;

            lock ( _builder.SyncRoot )
            {
                if ( _builder.CanBuildFrame() )
                {
                    frame = _builder.BuildFrame();
                }
            }
            
            if ( frame != null )
            {
                OnFrameReceived( new RTPFrameReceivedEventArgs( frame ) );
            }
        }
        
        public override void Reset()
        {
            _builder.Clear();
        }

        protected override void Dispose( bool disposing)
        {
            if ( disposing )
            {
                _builder.Dispose();
            }
        }
    }
}