﻿using System;

namespace RabbitOM.Net.Rtsp.Alpha
{
    public sealed class RTSPUdpMulticastMediaReceiver : RTSPMediaTransport
    {
        public RTSPUdpMulticastMediaReceiver( RTSPMediaChannelService service )
            : base( service )
		{
		}

        public override bool IsStarted 
            => throw new NotImplementedException();
        public override bool IsReceivingPacket
            => throw new NotImplementedException();

        public override bool Start()
            => throw new NotImplementedException();
        public override void Stop()
            => throw new NotImplementedException();
        public override void Dispose()
            => throw new NotImplementedException();
    }
}
