using System;
using System.Linq;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a message header
    /// </summary>
    public sealed class RTSPHeaderRange : RTSPHeader
    {
        private readonly RTSPStringPair _npt;

        private readonly RTSPStringPair _clock;

        private readonly RTSPStringPair _time;


        
        
        /// <summary>
        /// Intialize an new instance of header range class
        /// </summary>
        /// <param name="ntp">the ntp</param>
        /// <param name="clock">the clock</param>
        /// <param name="time">the time</param>
        public RTSPHeaderRange( RTSPStringPair ntp , RTSPStringPair clock , RTSPStringPair time )
        {
            _npt   = ntp   ?? RTSPStringPair.Empty;
            _clock = clock ?? RTSPStringPair.Empty;
            _time  = time  ?? RTSPStringPair.Empty;
        }


       
        
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
            return ! RTSPStringPair.IsNullOrEmpty( _npt )
                || ! RTSPStringPair.IsNullOrEmpty( _clock )
                || ! RTSPStringPair.IsNullOrEmpty( _time )
                ;
        }       
        
        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RTSPHeaderWriter( RTSPSeparator.SemiColon , RTSPOperator.Equality );

            if ( ! RTSPStringPair.IsNullOrEmpty( _npt ) )
            {
                writer.WriteField( RTSPHeaderFieldNames.Npt , _npt.ToString() );
            }

            if ( ! RTSPStringPair.IsNullOrEmpty( _clock ) )
            {
                if ( writer.IsAppended )
                {
                    writer.WriteSeparator();
                }

                writer.WriteField( RTSPHeaderFieldNames.Clock , _clock.ToString() );
            }

            if ( ! RTSPStringPair.IsNullOrEmpty( _time ) )
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
            result = null;

            var parser = new RTSPParser( value , RTSPSeparator.SemiColon );

            if ( ! parser.ParseHeaders() )
            {
                return false;
            }

            var pairs = new RTSPStringPair[3];

            using ( var reader = new RTSPHeaderReader( parser.GetParsedHeaders() ) )
            {
                reader.Operator = RTSPOperator.Equality;

                while ( reader.Read() )
                {
                    if ( ! reader.SplitElementAsField() )
                    {
                        continue;
                    }

                    if ( reader.IsNptTimeElementType )
                    {
                        RTSPStringPair.TryParse( reader.GetElementValue() , out pairs[0] );
                    }

                    if ( reader.IsClockElementType )
                    {
                        RTSPStringPair.TryParse( reader.GetElementValue() , out pairs[1] ); 
                    }

                    if ( reader.IsTimeElementType )
                    {
                        RTSPStringPair.TryParse( reader.GetElementValue() , out pairs[2] );
                    }
                }

                if ( pairs.All( RTSPStringPair.IsNullOrEmpty ) )
                {
                    return false;
                }

                result = new RTSPHeaderRange( pairs[0] , pairs[1] , pairs[2] );

                return true;
            }
        }
    }
}
