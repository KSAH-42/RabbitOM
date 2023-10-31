using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a type converter class
    /// </summary>
    public static class RTSPTransmissionTypeConverter
    {
        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="streamingMode">the streaming mode</param>
        /// <returns>returns a string value</returns>
        public static string Convert( RTSPTransmissionType streamingMode )
        {
            switch ( streamingMode )
            {
                case RTSPTransmissionType.Unicast:
                    return RTSPHeaderFieldNames.Unicast;

                case RTSPTransmissionType.Multicast:
                    return RTSPHeaderFieldNames.Multicast;
            }

            return string.Empty;
        }

        /// <summary>
        /// Perform a convertion
        /// </summary>
        /// <param name="streamingMode">the streaming mode</param>
        /// <returns>returns a string value</returns>
        public static RTSPTransmissionType Convert( string streamingMode )
        {
            if ( string.IsNullOrWhiteSpace( streamingMode ) )
            {
                return RTSPTransmissionType.Unknown;
            }

            var method     = streamingMode.Trim();
            var ignoreCase = true;

            if ( string.Compare( RTSPHeaderFieldNames.Unicast , method , ignoreCase ) == 0 )
            {
                return RTSPTransmissionType.Unicast;
            }

            if ( string.Compare( RTSPHeaderFieldNames.Multicast , method , ignoreCase ) == 0 )
            {
                return RTSPTransmissionType.Multicast;
            }

            return RTSPTransmissionType.Unknown;
        }
    }
}
