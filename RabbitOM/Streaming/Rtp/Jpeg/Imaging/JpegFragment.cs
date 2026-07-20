using System;

namespace RabbitOM.Streaming.Rtp.Jpeg.Imaging
{
    public sealed class JpegFragment
    {
        private const int MinimumLength = 16;






        public int Offset { get; private set; }

        public int Type { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int RestartInterval { get; private set; }

        public int MustBeZero { get; private set; }

        public int QFactor { get; private set; }

        public ArraySegment<byte> QTable { get; private set; }

        public ArraySegment<byte> Payload { get; private set; }







        public static bool TryParse( ArraySegment<byte> buffer , out JpegFragment result )
        {
            result = null;

            if ( buffer.Array == null || buffer.Count < MinimumLength )
            {
                return false;
            }

            var offset = buffer.Offset + 1;

            result = new JpegFragment()
            {
                Offset  = buffer.Array[ offset++ ]  << 16 | buffer.Array[ offset++] << 8 | buffer.Array[ offset ++ ] ,
                Type    = buffer.Array[ offset++ ] ,
                QFactor = buffer.Array[ offset++ ] ,
                Width   = buffer.Array[ offset++ ] * 8 ,
                Height  = buffer.Array[ offset++ ] * 8 ,
            };

            if ( result.Type >= 64 )
            {
                result.RestartInterval = buffer.Array[ offset++ ] << 8 | buffer.Array[ offset++ ];

                offset += 2;
            }

            if ( result.Offset == 0 && result.QFactor >= 128 )
            {
                result.MustBeZero = buffer.Array[ offset++ ];

                if ( result.MustBeZero == 0 )
                {
                    offset ++;

                    int length = buffer.Array[ offset++ ] << 8 | buffer.Array[ offset++ ];

                    if ( length >= ( buffer.Array.Length - offset ) )
                    {
                        return false;
                    }

                    result.QTable = new ArraySegment<byte>( buffer.Array , offset , length );

                    offset += length;
                }
            }

            if ( offset >= buffer.Count )
            {
                return false;
            }

            result.Payload = new ArraySegment<byte>( buffer.Array , offset , buffer.Array.Length - offset );

            return true;
        }
    }
}
