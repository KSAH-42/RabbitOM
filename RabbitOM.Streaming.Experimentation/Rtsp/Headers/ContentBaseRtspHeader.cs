using System;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    /// <summary>
    /// Represent an rtsp header
    /// </summary>
    public sealed class ContentBaseRtspHeader : RtspHeader 
    {
        /// <summary>
        /// The type name
        /// </summary>
        public const string TypeName = "Content-Base";
        



        private string _uri = string.Empty;
        



        /// <summary>
        /// Gets / Sets the uri
        /// </summary>
        public string Uri
        {
            get => _uri;
            set => _uri = StringRtspNormalizer.Normalize( value );
        }




        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="result">the result</param>
        /// <returns>returns true for a success, otherwiser false</returns>
        public static bool TryParse( string input , out ContentBaseRtspHeader result )
        {
            result = null;

            var uri = StringRtspNormalizer.Normalize( input );

            if ( StringRtspValidator.TryValidateUri( uri ) )
            {
                result = new ContentBaseRtspHeader() { Uri = uri };
            }

            return result != null;
        }





        /// <summary>
        /// Try to validate
        /// </summary>
        /// <returns>returns true for a successs, otherwise false</returns>
        public override bool TryValidate()
        {
            return StringRtspValidator.TryValidateUri( _uri );
        }

        /// <summary>
        /// Try to format
        /// </summary>
        /// <returns>returns a string</returns>
        public override string ToString()
        {
            return _uri;
        }
    }
}
