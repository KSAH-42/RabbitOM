using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtp.Framing
{
    public sealed class DefaultRTPFrameBuilder : RTPFrameBuilder
    {
        private readonly object _lock = new object();
        
        private readonly Queue<RtpPacket> _packets = new Queue<RtpPacket>();
        
        private readonly Queue<int> _sizes = new Queue<int>();
        
        private RtpPacket _lastPacket;
       
        private int _frameSize;







        public override object SyncRoot
        {
            get => _lock;
        }

        public RtpPacket LastPacket
        {
            get 
            {
                lock ( _lock )
                {
                    return _lastPacket;
                }
            }
        }





        // TODO: Change the signature, replace the byte array by a packet class and move the parse statement into the sink class.

        public override bool TryAddPacket( byte[] buffer )
        {
            if ( ! RtpPacket.TryParse( buffer , out RtpPacket packet ) )
            {
                return false;
            }

            lock ( _lock )
            {
                _packets.Enqueue( packet );
                _lastPacket = packet;

                OnPacketAdded( packet );
            
                return true;
            }
        }

        public override bool CanBuildFrame()
        {
            lock ( _lock )
            {
                return _sizes.Count > 0;
            }
        }

        public override RTPFrame BuildFrame()
        {
            lock ( _lock )
            {
                var packets = new RtpPacket[ _sizes.Dequeue() ];

                int index = 0;

                while ( index < packets.Length )
                {
                    packets[ index ++ ] = _packets.Dequeue();
                }

                return new RTPFrame( packets );
            }
        }

        public override void Clear()
        {
            lock ( _lock )
            {
                _packets.Clear();
                _sizes.Clear();
                _lastPacket = null;
            }
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                Clear();
            }
        }









        private void OnPacketAdded( RtpPacket packet )
        {
            _frameSize++;

            if ( packet.Marker )
            {
                _sizes.Enqueue( _frameSize );
                _frameSize = 0;
            }
        }
    }
}