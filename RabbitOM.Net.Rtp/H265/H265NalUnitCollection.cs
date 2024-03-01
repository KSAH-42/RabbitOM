/*
 EXPERIMENTATION of the next implementation of the rtp layer

                    IMPLEMENTATION  NOT COMPLETED
*/

using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Rtp.H265
{
    public sealed class H265NalUnitCollection : Queue<H265NalUnit>
    {
        public bool IsEmpty
        { 
            get => Count == 0; 
        }





        public bool Any()
        {
            return Count > 0;
        }

        public bool TryAdd( H265NalUnit nalu )
        {
            if ( nalu == null )
            {
                return false;
            }

            Enqueue( nalu );

            return true;
        }
    }
}