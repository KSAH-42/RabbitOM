using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderSession : RtspHeader
    {
        private string _number   = string.Empty;

        private long   _timeout  = 0;






        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        public RtspHeaderSession()
        {
        }

        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        /// <param name="number">the number</param>
        public RtspHeaderSession( string number )
            : this( number , 0 )
        {
        }

        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        /// <param name="number">the number</param>
        /// <param name="timeout">the timeout</param>
        public RtspHeaderSession( string number , long timeout )
        {
            Number  = number;
            Timeout = timeout;
        }






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.Session;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public string Number
        {
            get => _number;
            set => _number = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the timmeout
        /// </summary>
        public long Timeout
        {
            get => _timeout;
            set => _timeout = value > 0 ? value : 0;
        }






        /// <summary>
        /// Try validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return ! string.IsNullOrWhiteSpace( _number );
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RtspHeaderWriter()
            {
                Separator = RtspSeparator.SemiColon ,
                Operator  = RtspOperator.Equality ,
            };

            writer.Write( _number );

            if ( _timeout > 0 )
            {
                writer.WriteSeparator();
                writer.WriteField( RtspHeaderFieldNames.Timeout , _timeout );
            }

            return writer.Output;
        }






        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderSession result )
        {
            result = null;

            var parser = new RtspParser( value , RtspSeparator.SemiColon );

            if ( ! parser.ParseHeaders() )
            {
                return false;
            }

            using ( var reader = new RtspHeaderReader( parser.GetParsedHeaders() ) )
            {
                reader.Operator = RtspOperator.Equality;

                if ( ! reader.Read() )
                {
                    return false;
                }

                result = new RtspHeaderSession()
                {
                    Number = reader.GetElement()
                };

                while ( reader.Read() )
                {
                    if ( ! reader.SplitElementAsField() )
                    {
                        continue;
                    }

                    if ( reader.IsTimeoutElementType )
                    {
                        result.Timeout = reader.GetElementValueAsLong();
                    }
                }

                return true;
            }
        }
    }
}
