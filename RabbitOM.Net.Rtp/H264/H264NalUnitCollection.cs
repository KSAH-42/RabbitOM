using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Rtp.H264
{
    public sealed class H264NalUnitCollection : Queue<H264NalUnit>
    {
        public bool IsEmpty 
        { 
            get => Count == 0; 
        }

        public bool Any() 
        {
            return Count > 0;
        }
    }
}