using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderAllow : RTSPHeader
    {
        private readonly RTSPMethodList _methods = null;



        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPHeaderAllow()
            : this( new RTSPMethodList() )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="methods">the method list</param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPHeaderAllow( RTSPMethodList methods )
        {
            _methods = methods ?? throw new ArgumentNullException( nameof( methods ) );
        }



        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.Allow;
        }

        /// <summary>
        /// Gets the methods
        /// </summary>
        public RTSPMethodList Methods
        {
            get => _methods;
        }

        /// <summary>
        /// Validate
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
            var writer = new RTSPHeaderWriter( RTSPSeparator.Comma );

            foreach ( var method in _methods )
            {
                if ( writer.IsAppended )
                {
                    writer.WriteSeparator();
                    writer.WriteSpace();
                }

                writer.Write( RTSPMethodConverter.Convert( method ) );
            }

            return writer.Output;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RTSPHeaderAllow result )
        {
            result = null;

            var parser = new RTSPParser( value , RTSPSeparator.Comma );

            if ( !parser.ParseHeaders() )
            {
                return false;
            }

            using ( var reader = new RTSPHeaderReader( parser.GetParsedHeaders() ) )
            {
                result = new RTSPHeaderAllow();

                while ( reader.Read() )
                {
                    result.Methods.TryAdd( RTSPMethodConverter.Convert( reader.GetElement() ) );
                }

                return true;
            }
        }
    }
}
