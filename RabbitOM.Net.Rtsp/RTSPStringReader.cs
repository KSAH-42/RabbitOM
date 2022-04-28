using System;
using System.IO;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a tolerant string reader 
    /// </summary>
    public sealed class RTSPStringReader : IDisposable
    {
        private readonly StringReader _reader = null;



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="text">the input text</param>
        public RTSPStringReader( string text )
        {
            _reader = new StringReader( text ?? string.Empty );
        }



        /// <summary>
        /// Read a line
        /// </summary>
        /// <returns>returns a string</returns>
        public string ReadLine()
        {
            try
            {
                return _reader.ReadLine();
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return string.Empty;
        }

        /// <summary>
        /// Read to the end
        /// </summary>
        /// <returns>returns a string</returns>
        public string ReadToEnd()
        {
            try
            {
                return _reader.ReadToEnd();
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.WriteLine( ex );
            }

            return string.Empty;
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
