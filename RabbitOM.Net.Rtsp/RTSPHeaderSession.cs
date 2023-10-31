using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderSession : RTSPHeader
    {
        private string _number   = string.Empty;

        private long   _timeout  = 0;



        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPHeaderSession()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="number">the number</param>
        public RTSPHeaderSession( string number )
            : this( number , 0 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="number">the number</param>
        /// <param name="timeout">the timeout</param>
        public RTSPHeaderSession( string number , long timeout )
        {
            Number = number;
            Timeout = timeout;
        }




        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.Session;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public string Number
        {
            get => _number;
            set => _number = RTSPDataFilter.Trim( value );
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
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return !string.IsNullOrWhiteSpace( _number );
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RTSPHeaderWriter()
            {
                Separator = RTSPSeparator.SemiColon ,
                Operator  = RTSPOperator.Equality ,
            };

            writer.Write( _number );

            if ( _timeout > 0 )
            {
                writer.WriteSeparator();
                writer.WriteField( RTSPHeaderFieldNames.Timeout , _timeout );
            }

            return writer.Output;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RTSPHeaderSession result )
        {
            result = null;

            var parser = new RTSPParser( value , RTSPSeparator.SemiColon );

            if ( !parser.ParseHeaders() )
            {
                return false;
            }

            using ( var reader = new RTSPHeaderReader( parser.GetParsedHeaders() ) )
            {
                reader.Operator = RTSPOperator.Equality;

                if ( !reader.Read() )
                {
                    return false;
                }

                result = new RTSPHeaderSession()
                {
                    Number = reader.GetElement()
                };

                while ( reader.Read() )
                {
                    if ( !reader.SplitElementAsField() )
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
