using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderAccept : RTSPHeader
    {
        private readonly RTSPStringList _mimes = null;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">the value</param>
		public RTSPHeaderAccept( string value )
		{
            _mimes = new RTSPStringList();
            _mimes.TryAdd(value);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPHeaderAccept()
            : this( new RTSPStringList() )
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="collection">the collection</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPHeaderAccept( RTSPStringList collection )
        {
            _mimes = collection ?? throw new ArgumentNullException( nameof( collection ) );
        }




        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.Accept;
        }

        /// <summary>
        /// Gets / Sets the mime types
        /// </summary>
        public RTSPStringList Mimes
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
            var writer = new RTSPHeaderWriter()
            {
                Separator = RTSPSeparator.Comma
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
        public static bool TryParse( string value , out RTSPHeaderAccept result )
        {
            result = null;

            var parser = new RTSPParser( value , RTSPSeparator.Comma );

            if ( !parser.ParseHeaders() )
            {
                return false;
            }

            using ( var reader = new RTSPHeaderReader( parser.GetParsedHeaders() ) )
            {
                result = new RTSPHeaderAccept();

                while ( reader.Read() )
                {
                    result.Mimes.TryAdd( reader.GetElement() );
                }

                return true;
            }
        }
    }
}
