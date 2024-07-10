using System;
using System.Linq;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RtspHeaderRange : RtspHeader
    {
        private readonly RtspStringPair _npt;

        private readonly RtspStringPair _clock;

        private readonly RtspStringPair _time;


        
        
        /// <summary>
        /// Intialize an new instance of header range class
        /// </summary>
        /// <param name="ntp">the ntp</param>
        /// <param name="clock">the clock</param>
        /// <param name="time">the time</param>
        public RtspHeaderRange( RtspStringPair ntp , RtspStringPair clock , RtspStringPair time )
        {
            _npt   = ntp   ?? RtspStringPair.Empty;
            _clock = clock ?? RtspStringPair.Empty;
            _time  = time  ?? RtspStringPair.Empty;
        }


       
        
        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.Range;
        }

        /// <summary>
        /// Gets the ntp value
        /// </summary>
        public RtspStringPair Npt
        {
            get => _npt;
        }

        /// <summary>
        /// Gets the clock value
        /// </summary>
        public RtspStringPair Clock
        {
            get => _clock;
        }

        /// <summary>
        /// Gets the time value
        /// </summary>
        public RtspStringPair Time
        {
            get => _time;
        }


        
        
        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return ! RtspStringPair.IsNullOrEmpty( _npt )
                || ! RtspStringPair.IsNullOrEmpty( _clock )
                || ! RtspStringPair.IsNullOrEmpty( _time )
                ;
        }       
        
        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RtspHeaderWriter( RtspSeparator.SemiColon , RtspOperator.Equality );

            if ( ! RtspStringPair.IsNullOrEmpty( _npt ) )
            {
                writer.WriteField( RtspHeaderFieldNames.Npt , _npt.ToString() );
            }

            if ( ! RtspStringPair.IsNullOrEmpty( _clock ) )
            {
                if ( writer.IsAppended )
                {
                    writer.WriteSeparator();
                }

                writer.WriteField( RtspHeaderFieldNames.Clock , _clock.ToString() );
            }

            if ( ! RtspStringPair.IsNullOrEmpty( _time ) )
            {
                if ( writer.IsAppended )
                {
                    writer.WriteSeparator();
                }

                writer.WriteField( RtspHeaderFieldNames.Time , _time.ToString() );
            }

            return writer.Output;
        }




        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderRange result )
        {
            result = null;

            var parser = new RtspParser( value , RtspSeparator.SemiColon );

            if ( ! parser.ParseHeaders() )
            {
                return false;
            }

            var pairs = new RtspStringPair[3];

            using ( var reader = new RtspHeaderReader( parser.GetParsedHeaders() ) )
            {
                reader.Operator = RtspOperator.Equality;

                while ( reader.Read() )
                {
                    if ( ! reader.SplitElementAsField() )
                    {
                        continue;
                    }

                    if ( reader.IsNptTimeElementType )
                    {
                        RtspStringPair.TryParse( reader.GetElementValue() , out pairs[0] );
                    }

                    if ( reader.IsClockElementType )
                    {
                        RtspStringPair.TryParse( reader.GetElementValue() , out pairs[1] ); 
                    }

                    if ( reader.IsTimeElementType )
                    {
                        RtspStringPair.TryParse( reader.GetElementValue() , out pairs[2] );
                    }
                }

                if ( pairs.All( RtspStringPair.IsNullOrEmpty ) )
                {
                    return false;
                }

                result = new RtspHeaderRange( pairs[0] , pairs[1] , pairs[2] );

                return true;
            }
        }
    }
}
