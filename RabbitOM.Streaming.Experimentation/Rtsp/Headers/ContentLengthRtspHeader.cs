using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent a rtsp header
    /// </summary>
    public sealed class ContentLengthRtspHeader : RtspHeader 
    {
        public const string TypeName = "Content-Length";
        




        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public long Value { get; set; }





        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="result">the result</param>
        /// <returns>returns a string</returns>
        public static bool TryParse( string input , out ContentLengthRtspHeader result )
        {
            result = null;

            if ( long.TryParse( StringRtspNormalizer.Normalize( input ) , out var value ) )
            {
                result = new ContentLengthRtspHeader() { Value = value };
            }

            return result != null;
        }






        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>

        public override bool TryValidate()
        {
            return Value > 0;
        }

        /// <summary>
        /// Format to string
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
