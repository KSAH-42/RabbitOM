using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent type converter class
    /// </summary>
    public static class RTSPTransportTypeConverter
    {
        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="transportType">the transport type</param>
        /// <returns>returns a string value</returns>
        public static string Convert( RTSPTransportType transportType )
        {
            switch ( transportType )
            {
                case RTSPTransportType.RTP_AVP_TCP:
                    return RTSPHeaderFieldNames.RtpAvpTcp;

                case RTSPTransportType.RTP_AVP_UDP:
                    return RTSPHeaderFieldNames.RtpAvp;
            }

            return string.Empty;
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="transportType">the transportType type</param>
        /// <returns>returns a string value</returns>
        public static RTSPTransportType Convert( string transportType )
        {
            if ( string.IsNullOrWhiteSpace( transportType ) )
            {
                return RTSPTransportType.Unknown;
            }

            var method     = transportType.Trim();
            var ignoreCase = true;

            if ( string.Compare( RTSPHeaderFieldNames.RtpAvp , method , ignoreCase ) == 0 )
            {
                return RTSPTransportType.RTP_AVP_UDP;
            }

            if ( string.Compare( RTSPHeaderFieldNames.RtpAvpUdp , method , ignoreCase ) == 0 )
            {
                return RTSPTransportType.RTP_AVP_UDP;
            }

            if ( string.Compare( RTSPHeaderFieldNames.RtpAvpTcp , method , ignoreCase ) == 0 )
            {
                return RTSPTransportType.RTP_AVP_TCP;
            }

            return RTSPTransportType.Unknown;
        }
    }
}
