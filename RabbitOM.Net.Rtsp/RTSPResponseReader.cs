using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a response data reader
    /// </summary>
    public sealed class RTSPResponseReader : IDisposable
    {
        private string                              _fullVersion    = string.Empty;

        private string                              _majorVersion   = string.Empty;

        private string                              _minorVersion   = string.Empty;

        private string                              _statusCode     = string.Empty;

        private string                              _statusReason   = string.Empty;

        private string                              _body           = string.Empty;

        private readonly IDictionary<string,string> _headers        = null;

        private readonly RTSPStringReader           _reader         = null;





        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="input">the input string</param>
        public RTSPResponseReader( string input )
        {
            _reader = new RTSPStringReader( input );
            _headers = new Dictionary<string , string>( StringComparer.OrdinalIgnoreCase );
        }






        /// <summary>
        /// Gets the major version
        /// </summary>
        /// <returns>returns a value</returns>
        public int GetMajorVersion()
        {
            return RTSPDataConverter.ConvertToInteger( _majorVersion );
        }

        /// <summary>
        /// Gets the minor version
        /// </summary>
        /// <returns>returns a value</returns>
        public int GetMinorVersion()
        {
            return RTSPDataConverter.ConvertToInteger( _minorVersion );
        }

        /// <summary>
        /// Gets the status code
        /// </summary>
        /// <returns>returns a value</returns>
        public RTSPStatusCode GetStatusCode()
        {
            return RTSPDataConverter.ConvertToEnum<RTSPStatusCode>( _statusCode );
        }

        /// <summary>
        /// Gets the status details
        /// </summary>
        /// <returns>returns a value</returns>
        public string GetStatusReason()
        {
            return _statusReason;
        }

        /// <summary>
        /// Gets the headers
        /// </summary>
        /// <returns>returns a collection headers</returns>
        public IDictionary<string , string> GetHeaders()
        {
            return _headers;
        }

        /// <summary>
        /// Gets the body
        /// </summary>
        /// <returns>returns a string</returns>
        public string GetBody()
        {
            return _body;
        }






        /// <summary>
        /// Read a line
        /// </summary>
        /// <returns>the a value</returns>
        public string ReadLine()
        {
            return _reader.ReadLine();
        }

        /// <summary>
        /// Read the status line
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool ReadLineStatus()
        {
            string line = _reader.ReadLine();

            if ( string.IsNullOrWhiteSpace( line ) )
            {
                return false;
            }

            string[] tokens = line.Trim().Split( new char[] { ' ' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens == null || tokens.Length <= 1 || !tokens[0].StartsWith( "RTSP/" , StringComparison.OrdinalIgnoreCase ) )
            {
                return false;
            }

            _fullVersion = tokens[0];
            _statusCode = tokens[1];

            if ( tokens.Length >= 3 )
            {
                _statusReason = line.Substring( line.IndexOf( tokens[2] ) );
            }

            string[] versionParts = _fullVersion.Split( new char[]{ '/' , '.' } , StringSplitOptions.RemoveEmptyEntries );

            if ( versionParts == null || versionParts.Length < 3 )
            {
                return false;
            }

            _majorVersion = versionParts[1];
            _minorVersion = versionParts[2];

            return true;
        }

        /// <summary>
        /// Read headers
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool ReadHeaders()
        {
            int results = 0;

            for ( ; ; )
            {
                string line = _reader.ReadLine();

                if ( string.IsNullOrEmpty( line ) )
                {
                    break;
                }

                int separatorIndex = line.IndexOf( ':' );

                if ( separatorIndex <= 0 )
                {
                    continue;
                }

                string headerName = line.Remove( separatorIndex ) ?? string.Empty;

                if ( string.IsNullOrWhiteSpace( headerName ) )
                {
                    continue;
                }

                string headerValue = string.Empty;

                if ( ( separatorIndex + 1 ) < line.Length )
                {
                    headerValue = line.Substring( separatorIndex + 1 ) ?? string.Empty;
                }

                if ( string.IsNullOrWhiteSpace( headerValue ) )
                {
                    continue;
                }

                if ( _headers.ContainsKey( headerName ) )
                {
                    continue;
                }

                _headers[headerName] = headerValue;

                ++results;
            }

            return results > 0;
        }

        /// <summary>
        /// Read the body
        /// </summary>
        public void ReadBody()
        {
            _body = _reader.ReadToEnd() ?? string.Empty;
        }

        /// <summary>
        /// Release internal resources
        /// </summary>
        public void Dispose()
        {
            _reader.Dispose();
        }
    }
}
