﻿/*
 EXPERIMENTATION of the next implementation of the rtp layer

              O P T I M I Z A T I O N S

 For next implementation, optimize can to reduce copy 
 by using array segment to remove Buffer.Copy or similar methods

*/

using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Rtsp.Tests
{
    public sealed class DefaultRTPFrameBuilder : RTPFrameBuilder
    {
        private readonly object _lock = new object();
        private readonly Queue<RTPPacket> _packets = new Queue<RTPPacket>();
        private readonly Queue<int> _sizes = new Queue<int>();
        private RTPPacket _lastPacket;
        private int _frameSize;

        public RTPPacket LastPacket
        {
            get 
            {
                lock ( _lock )
                {
                    return _lastPacket;
                }
            }
        }

        public override bool TryAddPacket( byte[] buffer )
        {
            if ( ! RTPPacket.TryParse( buffer , out RTPPacket packet ) )
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
                RTPPacket[] packets = new RTPPacket[ _sizes.Dequeue() ];

                int index = 0;

                while ( index < packets.Length )
                {
                    packets[ index ++ ] = _packets.Dequeue();
                }

                return new RTPFrame( packets );
            }
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
                Clear();
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

        private void OnPacketAdded( RTPPacket packet )
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