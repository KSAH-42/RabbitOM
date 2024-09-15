using System;
using System.Collections;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent the accept message header
    /// </summary>
    public sealed class RtspHeaderAccept : RtspHeader
    {
        private readonly HashSet<string> _mimes = new HashSet<string>();






        /// <summary>
        /// Initialize a new instance of header class
        /// </summary>
        public RtspHeaderAccept()
        {
        }

        /// <summary>
        /// Initialize a new instance of header class
        /// </summary>
        /// <param name="mime">the mime</param>
        public RtspHeaderAccept( string mime )
        {
            AddMime( mime );
        }






        /// <summary>
        /// Gets the name
        /// </summary>
        public override string Name
        {
            get => RtspHeaderNames.Accept;
        }

        /// <summary>
        /// Gets the mime types
        /// </summary>
        public IReadOnlyCollection<string> Mimes
        {
            get => _mimes;
        }






        /// <summary>
        /// Can add a mime value
        /// </summary>
        /// <param name="mime">the mime</param>
        /// <exception cref="ArgumentException"/>
        public bool CanAddMime( string mime )
        {
            if ( string.IsNullOrWhiteSpace( mime ) )
            {
                return false;
            }

            return ! _mimes.Contains( mime ) && _mimes.Count < MaximumOfMimes;
        }

        /// <summary>
        /// Add a new mime value
        /// </summary>
        /// <param name="mime">the mime</param>
        /// <exception cref="InvalidOperationException"/>
        public void AddMime( string mime )
        {
            if ( ! CanAddMime( mime ) || ! _mimes.Add( mime ) )
            {
                throw new InvalidOperationException( nameof( mime ) );
            }
        }

        /// <summary>
        /// Add a new mime value
        /// </summary>
        /// <param name="mime">the mime</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool TryAddMime( string mime )
        {
            return CanAddMime( mime ) && _mimes.Add( mime );
        }

        /// <summary>
        /// Remove an existing mime value
        /// </summary>
        /// <param name="mime">the mime</param>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool RemoveMime( string mime )
        {
            return _mimes.Remove( mime );
        }

        /// <summary>
        /// Remove all mimes
        /// </summary>
        public void RemoveAllMimes()
        {
            _mimes.Clear();
        }






        /// <summary>
        /// Gets the maximume of allowed mimes
        /// </summary>
        public static int MaximumOfMimes
        {
            get => 1000;
        }
        
        /// <summary>
        /// Try validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _mimes.Count > 0;
        }

        /// <summary>
        /// Returns an empty string
        /// </summary>
        /// <returns>returns a string value</returns>
        public override string ToString()
        {
            var writer = new RtspHeaderWriter()
            {
                Separator = RtspSeparator.Comma
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
        /// Create an new instance of the accept header
        /// </summary>
        /// <param name="mime">the mime</param>
        public static RtspHeaderAccept NewAcceptHeader( string mime )
        {
            RtspHeaderAccept header = new RtspHeaderAccept();

            header.TryAddMime( mime );

            return header;
        }

        /// <summary>
        /// Try to parse
        /// </summary>
        /// <param name="value">the header value</param>
        /// <param name="result">the output result</param>
        /// <returns>returns true for a success, otherwise false.</returns>
        public static bool TryParse( string value , out RtspHeaderAccept result )
        {
            result = null;

            var parser = new RtspParser( value , RtspSeparator.Comma );

            if ( !parser.ParseHeaders() )
            {
                return false;
            }

            using ( var reader = new RtspHeaderReader( parser.GetParsedHeaders() ) )
            {
                result = new RtspHeaderAccept();

                while ( reader.Read() )
                {
                    result.TryAddMime( reader.GetElement() );
                }

                return true;
            }
        }
    }
}
