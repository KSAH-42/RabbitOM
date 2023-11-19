using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderRtpInfo : RTSPHeader
    {
        private string  _url       = string.Empty;

        private long    _sequence  = 0;

        private long    _rtpTime   = 0;



        /// <summary>
        /// Constructor
        /// </summary>
        public RTSPHeaderRtpInfo()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">the url</param>
        public RTSPHeaderRtpInfo( string url )
            : this( url , 0 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">the url</param>
        /// <param name="sequence">the sequence</param>
        public RTSPHeaderRtpInfo( string url , long sequence )
            : this( url , sequence , 0 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="url">the url</param>
        /// <param name="sequence">the sequence</param>
        /// <param name="rtpTime">the rtp time</param>
        public RTSPHeaderRtpInfo( string url , long sequence , long rtpTime )
        {
            Url = url;
            Sequence = sequence;
            RtpTime = rtpTime;
        }




        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RTSPHeaderNames.RtpInfo;
        }

        /// <summary>
        /// Gets / Sets the value
        /// </summary>
        public string Url
        {
            get => _url;
            set => _url = RTSPDataConverter.Trim( value );
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
        /// Validate
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
            var writer = new RTSPHeaderWriter( RTSPSeparator.SemiColon , RTSPOperator.Equality );

            writer.WriteField( RTSPHeaderFieldNames.Url , _url );
            writer.WriteSeparator();
            writer.WriteField( RTSPHeaderFieldNames.Sequence , _sequence );
            writer.WriteSeparator();
            writer.WriteField( RTSPHeaderFieldNames.RtpTime , _rtpTime );

            return writer.Output;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RTSPHeaderRtpInfo result )
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

                result = new RTSPHeaderRtpInfo();

                while ( reader.Read() )
                {
                    if ( !reader.SplitElementAsField() )
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
