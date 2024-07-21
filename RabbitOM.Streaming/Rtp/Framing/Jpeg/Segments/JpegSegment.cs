﻿using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg.Segments
{
    public abstract class JpegSegment
    {
        protected byte[] _buffer = null;

        
        

        public byte[] GetBuffer()
        {
            if ( _buffer == null )
            {
                _buffer = CreateBuffer();
            }

            return _buffer;
        }


        public virtual void ClearBuffer()
        {
            _buffer = null;
        }

        protected abstract byte[] CreateBuffer();
    }
}
