using System;
using System.Text;

namespace RabbitOM.Net.Rtp
{
    /// <summary>
    /// Represent a class used to format and parse data
    /// </summary>
    internal static class RtpPacketFormatter
    {
        /// <summary>
        /// Format to string the packet
        /// </summary>
        /// <param name="packet">the packet</param>
        /// <returns>returns a string</returns>
        public static string Format( RtpPacket packet )
        {
            if ( packet == null )
            {
                return string.Empty;
            }

            var builder = new StringBuilder();

            builder.AppendFormat( "Marker: {0}" , packet.Marker );
            builder.AppendFormat( "SequenceNumber: {0}" , packet.SequenceNumber );
            builder.AppendFormat( "PayloadType: {0}" , packet.PayloadType );
            builder.AppendFormat( "DataSize: {0}" , packet.Data.Count);

            return builder.ToString();
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="buffer">the value</param>
        /// <param name="result">the packet result</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse( byte[] buffer , out RtpPacket result )
        {
            result = default;

            if ( buffer == null || buffer.Length < RtpPacket.DefaultHeaderSize )
            {
                return false;
            }

            int offset = 0;

            uint version = (uint) buffer[ offset ] >> 6;

            if ( version != RtpPacket.DefaultVersion )
            {
                return false;
            }

            result = new RtpPacket();
            result.Version = version;

            result.HasPadding = ( ( buffer[ offset ] >> 5 ) & 0x1 ) != 0;
            result.HasExtension = ( ( buffer[ offset ] >> 4 ) & 0x1 ) != 0;
            result.NumberOfCSRC = buffer[ offset++ ] & 0xF;

            result.Marker = ( ( buffer[ offset ] >> 7 ) & 0x1 ) != 0;
            result.PayloadType = buffer[ offset++ ] & 0x7F;

            result.SequenceNumber = (ushort) RtpDataConverter.ConvertToInt16( buffer , offset );
            offset += 2;

            result.Timestamp = RtpDataConverter.ConvertToUInt( buffer , offset );
            offset += 4;

            result.SSRC = RtpDataConverter.ConvertToUInt( buffer , offset );
            offset += 4 + 4 * result.NumberOfCSRC;

            if ( result.HasExtension )
            {
                result.ExtensionHeaderId = RtpDataConverter.ConvertToInt16( buffer , offset );

                offset += 4;
                offset += RtpDataConverter.ConvertToInt16( buffer , offset ) * 4;
            }

            int payloadSize = buffer.Length - offset;

            if ( result.HasPadding )
            {
                payloadSize -= buffer[ buffer.Length - 1 ];
            }

            result.Data = new ArraySegment<byte>( buffer , offset , payloadSize );

            return true;
        }
    }
}
