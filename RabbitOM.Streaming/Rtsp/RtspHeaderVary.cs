using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderVary : RtspHeader
    {
        private readonly RtspStringCollection _headerNames = null;



        /// <summary>
        /// Constructor
        /// </summary>
        public RtspHeaderVary()
            : this( new RtspStringCollection() )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="headerNames">the header names</param>
        /// <exception cref="ArgumentNullException"/>
        public RtspHeaderVary( RtspStringCollection headerNames )
        {
            _headerNames = headerNames ?? throw new ArgumentNullException( nameof( headerNames ) );
        }





        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.Vary;
        }

        /// <summary>
        /// Gets the header names
        /// </summary>
        public RtspStringCollection HeaderNames
        {
            get => _headerNames;
        }



        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _headerNames.Any();
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RtspHeaderWriter( RtspSeparator.Comma );

            foreach ( var header in _headerNames )
            {
                if ( string.IsNullOrWhiteSpace( header ) )
                {
                    continue;
                }

                if ( writer.IsAppended )
                {
                    writer.WriteSeparator();
                    writer.WriteSpace();
                }

                writer.Write( header );
            }

            return writer.Output;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderVary result )
        {
            result = null;

            var parser = new RtspParser( value , RtspSeparator.Comma );

            if ( !parser.ParseHeaders() )
            {
                return false;
            }

            using ( var reader = new RtspHeaderReader( parser.GetParsedHeaders() ) )
            {
                result = new RtspHeaderVary();

                while ( reader.Read() )
                {
                    result.HeaderNames.TryAdd( reader.GetElement() );
                }

                return true;
            }
        }
    }
}
