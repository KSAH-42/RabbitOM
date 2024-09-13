using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderAllow : RtspHeader
    {
        private readonly RtspMethodList _methods = null;






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.Allow;
        }

        /// <summary>
        /// Gets the methods
        /// </summary>
        public RtspMethodList Methods
        {
            get => _methods;
        }






        /// <summary>
        /// Try validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _methods.Any();
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RtspHeaderWriter( RtspSeparator.Comma );

            foreach ( var method in _methods )
            {
                if ( writer.IsAppended )
                {
                    writer.WriteSeparator();
                    writer.WriteSpace();
                }

                writer.Write(RtspDataConverter.ConvertToString( method ) );
            }

            return writer.Output;
        }






        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderAllow result )
        {
            result = null;

            var parser = new RtspParser( value , RtspSeparator.Comma );

            if ( ! parser.ParseHeaders() )
            {
                return false;
            }

            using ( var reader = new RtspHeaderReader( parser.GetParsedHeaders() ) )
            {
                result = new RtspHeaderAllow();

                while ( reader.Read() )
                {
                    result.Methods.TryAdd( RtspDataConverter.ConvertToEnumMethod( reader.GetElement() ) );
                }

                return true;
            }
        }
    }
}
