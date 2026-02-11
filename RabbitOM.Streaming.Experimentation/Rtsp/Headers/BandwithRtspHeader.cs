using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent a rtsp header
    /// </summary>
    public sealed class BandwithRtspHeader : RtspHeader
    {
        /// <summary>
        /// The type name
        /// </summary>
        public const string TypeName = "Bandwith";
        




        /// <summary>
        /// Gets / Sets the bit rate
        /// </summary>
        public long BitRate { get; set; }





        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="result">the result</param>
        /// <returns>returns a string</returns>
        public static bool TryParse( string input , out BandwithRtspHeader result )
        {
            result = null;

            if ( long.TryParse( StringRtspNormalizer.Normalize( input ) , out var value ) )
            {
                result = new BandwithRtspHeader() { BitRate = value };
            }

            return result != null;
        }






        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>

        public override bool TryValidate()
        {
            return BitRate >= 0;
        }

        /// <summary>
        /// Format to string
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            return BitRate.ToString();
        }
    }
}
