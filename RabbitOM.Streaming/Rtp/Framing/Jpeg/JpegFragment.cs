using System;

namespace RabbitOM.Streaming.Rtp.Framing.Jpeg
{
	public sealed class JpegFragment
    {
        public const int MinimumLength = 16;


        public int Offset { get; set; }
        public int Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Dri { get; set; }
        public int Mbz { get; set; }
        public int QFactor { get; set; }
        public ArraySegment<byte> QTable { get; set; }
        public ArraySegment<byte> Data { get; set; }
       


        public bool TryValidate()
        {
            return Type <= 0 || Width < 2 || Height < 2 || Data.Count < 0 ? false : true;
        }
        
        public override string ToString()
        {
            return $"{Type} {QFactor} {Width} {Height} {Dri} {Mbz} {QTable.Count} {Data.Count}";
        }







        public static bool TryParse( ArraySegment<byte> buffer , out JpegFragment result )
        {
            result = null;

            if ( buffer.Array == null || buffer.Count < MinimumLength )
            {
                return false;
            }

            int offset = 1;

            result = new JpegFragment()
            {
                Offset  = buffer.Array[ offset++ ] << 16 | buffer.Array[ offset++] << 8 | buffer.Array[ offset ++ ] ,
                Type    = buffer.Array[ offset++ ] ,
                QFactor = buffer.Array[ offset++ ] ,
                Width   = buffer.Array[ offset++ ] * 8 ,
                Height  = buffer.Array[ offset++ ] * 8 ,
            };

            if ( result.Type >= 64 )
            {
                result.Dri = buffer.Array[ offset++ ] << 8 | buffer.Array[ offset++ ];

                offset += 2;
            }

            if ( result.Offset == 0 && result.QFactor >= 128 )
            {
                result.Mbz = buffer.Array[ offset++ ];

                if ( result.Mbz == 0 )
                {
                    offset ++;

                    int length = buffer.Array[ offset++ ] << 8 | buffer.Array[ offset++ ];

                    if ( length < 0 || length > ( buffer.Count - (buffer.Offset + offset) ) )
                    {
                        return false;
                    }

                    result.QTable = new ArraySegment<byte>( buffer.Array , buffer.Offset + offset , length );

                    offset += length;
                }
            }

            if ( (buffer.Offset + offset) >= buffer.Count )
            {
                return false;
            }

            result.Data = new ArraySegment<byte>( buffer.Array , buffer.Offset + offset , buffer.Count - offset );
			
            return true;
        }
    }
}
