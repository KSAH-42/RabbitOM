/*
 EXPERIMENTATION of the next implementation of the rtp layer


                    IMPLEMENTATION  NOT COMPLETED


*/

using System;

namespace RabbitOM.Net.Rtp
{
	public sealed class RTPPacket
    {
        private RTPPacket() { }



        public byte Version { get; private set; }
        public bool Padding  { get; private set; }
        public bool HasExtension { get; private set; }
        public ushort NumberOfCSRC { get; private set; }
        public bool Marker { get; private set; }
        public byte Type { get; private set; }
        public uint SequenceNumber { get; private set; }
        public uint Timestamp { get; private set; }
        public uint SSRC { get; private set; }
        public uint ExtensionId { get; private set; }
        public byte[] Data { get; private set; }
        public byte[] ExtensionData { get; private set; }
        public int[] CSRCIdentifiers { get; private set; }




        public bool TryValidate()
        {
            return Version != 2 || Data == null || Data.Length <= 0 ? false : true;
        }






        public static bool IsH264Packet( RTPPacket packet )
        {
            return packet != null && packet.Type == 96;
        }

        public static bool TryParse( byte[] buffer , out RTPPacket result )
        {
            result = null;

            if ( buffer == null || buffer.Length <= 11 )
            {
                return false;
            }

            result = new RTPPacket();

            result.Version         = (byte) ( ( buffer[ 0 ] & 0xC0 ) >> 6 );
            result.Padding         = (byte) ( ( buffer[ 0 ] & 0x20 ) >> 5 ) == 1;
            result.HasExtension    = (byte) ( ( buffer[ 0 ] & 0x10 ) >> 4 ) == 1;
            result.NumberOfCSRC    = (ushort) ( buffer[ 0 ] & 0x0F );

            result.Marker          = (byte) ((buffer[ 1 ] & 0x80 ) ) != 0;
            result.Type     = (byte) ( buffer[ 1 ] & 0x07F );
            result.SequenceNumber += (uint) ( buffer[ 2 ]  << 8 );
            result.SequenceNumber += (uint) ( buffer[ 3 ] );
            result.Timestamp      += (uint) ( buffer[ 4 ]  << 24 );
            result.Timestamp      += (uint) ( buffer[ 5 ]  << 16 );
            result.Timestamp      += (uint) ( buffer[ 6 ]  << 8  );
            result.Timestamp      += (uint) ( buffer[ 7 ]  << 0  );
            result.SSRC           += (uint) ( buffer[ 8 ]  << 24 );
            result.SSRC           += (uint) ( buffer[ 9 ]  << 16 );
            result.SSRC           += (uint) ( buffer[ 10 ] << 8 );
            result.SSRC           += (uint) ( buffer[ 11 ] );

            result.SequenceNumber = result.SequenceNumber % ( ushort.MaxValue + 1 );

            uint startIndex = 12;

            if ( result.NumberOfCSRC > 0 )
            {
                result.CSRCIdentifiers = new int[ result.NumberOfCSRC ];

                for ( uint i = 0 ; i < result.CSRCIdentifiers.Length && ( startIndex + i ) < buffer.Length ; ++i )
                {
                    result.CSRCIdentifiers[ i ] += buffer[ startIndex + i ] << 24; startIndex++;
                    result.CSRCIdentifiers[ i ] += buffer[ startIndex + i ] << 16; startIndex++;
                    result.CSRCIdentifiers[ i ] += buffer[ startIndex + i ] << 8; startIndex++;
                    result.CSRCIdentifiers[ i ] += buffer[ startIndex + i ]; startIndex++;
                }
            }

            if ( result.HasExtension )
            {
                result.ExtensionId = ( (uint) buffer[ startIndex + 0 ] << 8 ) + (uint) ( buffer[ startIndex + 1 ] << 0 );

                uint extenstionSize = ( (uint) buffer[ startIndex + 2 ] << 8 ) + (uint) ( buffer[ startIndex + 3 ] << 0 ) * 4;

                startIndex += (uint) extenstionSize + 4;
            }

            if ( startIndex >= buffer.Length )
            {
                return false;
            }

            result.Data = new byte[ buffer.Length - startIndex ];

            Buffer.BlockCopy( buffer , (int) startIndex , result.Data , 0 , result.Data.Length );

            return true;
        }
    }
}