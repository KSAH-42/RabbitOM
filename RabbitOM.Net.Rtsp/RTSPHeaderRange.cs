using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderRange : RTSPHeader
    {
        private readonly RTSPStringPair _npt   = new RTSPStringPair();

        private readonly RTSPStringPair _clock = new RTSPStringPair();

        private readonly RTSPStringPair _time  = new RTSPStringPair();



        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.Range;
        }

        /// <summary>
        /// Gets the ntp value
        /// </summary>
        public RTSPStringPair Npt
        {
            get => _npt;
        }

        /// <summary>
        /// Gets the clock value
        /// </summary>
        public RTSPStringPair Clock
        {
            get => _clock;
        }

        /// <summary>
        /// Gets the time value
        /// </summary>
        public RTSPStringPair Time
        {
            get => _time;
        }



        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _npt.TryValidate() || _clock.TryValidate() || _time.TryValidate();
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RTSPHeaderWriter( RTSPSeparator.SemiColon , RTSPOperator.Equality );

            if ( _npt.TryValidate() )
            {
                writer.WriteField( RTSPHeaderFieldNames.Npt , _npt.ToString() );
            }

            if ( _clock.TryValidate() )
            {
                if ( writer.IsAppended )
                {
                    writer.WriteSeparator();
                }

                writer.WriteField( RTSPHeaderFieldNames.Clock , _clock.ToString() );
            }

            if ( _time.TryValidate() )
            {
                if ( writer.IsAppended )
                {
                    writer.WriteSeparator();
                }

                writer.WriteField( RTSPHeaderFieldNames.Time , _time.ToString() );
            }

            return writer.Output;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RTSPHeaderRange result )
        {
            result = new RTSPHeaderRange();

            var parser = new RTSPParser( value , RTSPSeparator.SemiColon );

            if ( !parser.ParseHeaders() )
            {
                return false;
            }

            using ( var reader = new RTSPHeaderReader( parser.GetParsedHeaders() ) )
            {
                reader.Operator = RTSPOperator.Equality;

                result = new RTSPHeaderRange();

                while ( reader.Read() )
                {
                    if ( ! reader.SplitElementAsField() )
                    {
                        continue;
                    }

                    if ( reader.IsNptTimeElementType )
                    {
                        RTSPStringPair.TryDecode( result.Npt , reader.GetElementValue() );
                    }

                    if ( reader.IsClockElementType )
                    {
                        RTSPStringPair.TryDecode( result.Clock , reader.GetElementValue() ); 
                    }

                    if ( reader.IsTimeElementType )
                    {
                        RTSPStringPair.TryDecode( result.Time, reader.GetElementValue() );
                    }
                }

                return true;
            }
        }
    }
}
