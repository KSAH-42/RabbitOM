using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderVary : RTSPHeader
    {
        private readonly RTSPStringCollection _headerNames = null;



        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPHeaderVary()
            : this( new RTSPStringCollection() )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="headerNames">the header names</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPHeaderVary( RTSPStringCollection headerNames )
        {
            _headerNames = headerNames ?? throw new ArgumentNullException( nameof( headerNames ) );
        }





        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.Vary;
        }

        /// <summary>
        /// Gets the header names
        /// </summary>
        public RTSPStringCollection HeaderNames
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
            var writer = new RTSPHeaderWriter( RTSPSeparator.Comma );

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
        public static bool TryParse( string value , out RTSPHeaderVary result )
        {
            result = null;

            var parser = new RTSPParser( value , RTSPSeparator.Comma );

            if ( !parser.ParseHeaders() )
            {
                return false;
            }

            using ( var reader = new RTSPHeaderReader( parser.GetParsedHeaders() ) )
            {
                result = new RTSPHeaderVary();

                while ( reader.Read() )
                {
                    result.HeaderNames.TryAdd( reader.GetElement() );
                }

                return true;
            }
        }
    }
}
