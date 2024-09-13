using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderRtpInfo : RtspHeader
    {
        private string  _url       = string.Empty;

        private long    _sequence  = 0;

        private long    _rtpTime   = 0;






        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        public RtspHeaderRtpInfo()
        {
        }

        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        /// <param name="url">the url</param>
        public RtspHeaderRtpInfo( string url )
            : this( url , 0 )
        {
        }

        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        /// <param name="url">the url</param>
        /// <param name="sequence">the sequence</param>
        public RtspHeaderRtpInfo( string url , long sequence )
            : this( url , sequence , 0 )
        {
        }

        /// <summary>
        /// Initialize a new instance of a header class
        /// </summary>
        /// <param name="url">the url</param>
        /// <param name="sequence">the sequence</param>
        /// <param name="rtpTime">the rtp time</param>
        public RtspHeaderRtpInfo( string url , long sequence , long rtpTime )
        {
            Url      = url;
            Sequence = sequence;
            RtpTime  = rtpTime;
        }






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.RtpInfo;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public string Url
        {
            get => _url;
            set => _url = RtspDataConverter.Trim( value );
        }

        /// <summary>
        /// Gets / Sets the sequence
        /// </summary>
        public long Sequence
        {
            get => _sequence;
            set => _sequence = value;
        }

        /// <summary>
        /// Gets / Sets the time
        /// </summary>
        public long RtpTime
        {
            get => _rtpTime;
            set => _rtpTime = value;
        }






        /// <summary>
        /// Try validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            if ( string.IsNullOrWhiteSpace( _url ) )
            {
                return false;
            }

            if ( _sequence < 0 )
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RtspHeaderWriter( RtspSeparator.SemiColon , RtspOperator.Equality );

            writer.WriteField( RtspHeaderFieldNames.Url , _url );
            writer.WriteSeparator();
            writer.WriteField( RtspHeaderFieldNames.Sequence , _sequence );
            writer.WriteSeparator();
            writer.WriteField( RtspHeaderFieldNames.RtpTime , _rtpTime );

            return writer.Output;
        }






        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderRtpInfo result )
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

                result = new RtspHeaderRtpInfo();

                while ( reader.Read() )
                {
                    if ( ! reader.SplitElementAsField() )
                    {
                        continue;
                    }

                    if ( reader.IsUrlElementType )
                    {
                        result.Url = reader.GetElementValue();
                    }

                    if ( reader.IsSequenceElementType )
                    {
                        result.Sequence = reader.GetElementValueAsLong();
                    }

                    if ( reader.IsRtpTimeElementType )
                    {
                        result.RtpTime = reader.GetElementValueAsLong();
                    }
                }

                return true;
            }
        }
    }
}
