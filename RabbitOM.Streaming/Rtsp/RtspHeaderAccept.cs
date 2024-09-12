using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderAccept : RtspHeader
    {
        private readonly RtspStringCollection _mimes = new RtspStringCollection();






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.Accept;
        }

        /// <summary>
        /// Gets / Sets the mime types
        /// </summary>
        public RtspStringCollection Mimes
        {
            get => _mimes;
        }






        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _mimes.Any();
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RtspHeaderWriter()
            {
                Separator = RtspSeparator.Comma
            };

            foreach ( var mime in _mimes )
            {
                if ( string.IsNullOrWhiteSpace( mime ) )
                {
                    continue;
                }

                if ( writer.IsAppended )
                {
                    writer.WriteSeparator();
                    writer.WriteSpace();
                }

                writer.Write( mime );
            }

            return writer.Output;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderAccept result )
        {
            result = null;

            var parser = new RtspParser( value , RtspSeparator.Comma );

            if ( !parser.ParseHeaders() )
            {
                return false;
            }

            using ( var reader = new RtspHeaderReader( parser.GetParsedHeaders() ) )
            {
                result = new RtspHeaderAccept();

                while ( reader.Read() )
                {
                    result.Mimes.TryAdd( reader.GetElement() );
                }

                return true;
            }
        }
    }
}
