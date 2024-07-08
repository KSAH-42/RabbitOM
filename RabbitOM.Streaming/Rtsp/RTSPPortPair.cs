using System;
using System.Linq;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a port pair value
    /// </summary>
    public struct RTSPPortPair
    {
        /// <summary>
        /// Represent the zero value
        /// </summary>
        public readonly static RTSPPortPair    Zero    = new RTSPPortPair( 0 , 0 );





        /// <summary>
        /// The RTP port
        /// </summary>
        private readonly int                   _rtp;

        /// <summary>
        /// The RTCP port
        /// </summary>
        private readonly int                   _rtcp;





        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="rtp">the rtp port</param>
        /// <param name="rtcp">the rtcp port</param>
        public RTSPPortPair( int rtp , int rtcp )
        {
            _rtp  = rtp;
            _rtcp = rtcp;
        }





        /// <summary>
        /// Gets the rtp port
        /// </summary>
        public int Rtp
        {
            get => _rtp;
        }

        /// <summary>
        /// Gets the rtcp port
        /// </summary>
        public int Rtcp
        {
            get => _rtcp;
        }





        /// <summary>
        /// Create a new port an set automatically the rtcp port with incrementation of one from the rtp port value
        /// </summary>
        /// <param name="rtp">the rtp port</param>
        /// <returns>return an instance</returns>
        public static RTSPPortPair NewPortPair(int rtp)
        {
            return new RTSPPortPair(rtp, rtp + 1);
        }


        /// <summary>
        /// Check of the port is invalid
        /// </summary>
        /// <param name="port">the port</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool IsNotNull(RTSPPortPair port)
        {
            return port.Rtp > 0 || port.Rtcp > 0;
        }
        
        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the input text</param>
        /// <param name="result">the outputed value</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public static bool TryParse(string value, out RTSPPortPair result)
        {
            result = RTSPPortPair.Zero;

            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            var tokens = value.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            if (   !RTSPDataConverter.CanConvertToInteger(tokens.ElementAtOrDefault(0) ?? "0")
                || !RTSPDataConverter.CanConvertToInteger(tokens.ElementAtOrDefault(1) ?? "0")
                )
            {
                return false;
            }

            result = new RTSPPortPair(
                    RTSPDataConverter.ConvertToInteger(tokens.ElementAtOrDefault(0) ?? "0"),
                    RTSPDataConverter.ConvertToInteger(tokens.ElementAtOrDefault(1) ?? "0")
                    );

            return true;
        }





        /// <summary>
        /// Format the pair port using range representation ( ie: 123-456 )
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            return $"{_rtp}-{_rtcp}";
        }
    }
}
