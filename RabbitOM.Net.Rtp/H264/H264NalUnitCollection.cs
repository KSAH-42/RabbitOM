/*
 EXPERIMENTATION of the next implementation of the rtp layer

              O P T I M I Z A T I O N S

 For next implementation, optimize can to reduce copy 
 by using array segment to remove Buffer.Copy or similar methods

*/

using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Rtsp.Tests
{
    public sealed class H264NalUnitCollection : Queue<H264NalUnit>
    {
        public bool IsEmpty { get => Count == 0; }

        public bool Any() => Count > 0;
    }
}