using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent a RTSP chunk
    /// </summary>
    public sealed class RTSPChunk
    {
        private readonly byte[]              _data     = null;



        /// <summary>
        /// Represent a null value
        /// </summary>
        public readonly static RTSPChunk     Null      = new RTSPChunk();



        /// <summary>
        /// Constructor
        /// </summary>
        private RTSPChunk()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">the data</param>
        public RTSPChunk( byte[] data )
        {
            _data = data;
        }



        /// <summary>
        /// Gets the data
        /// </summary>
        public byte[] Data
        {
            get => _data;
        }



        /// <summary>
        /// Perform a simple validation
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool Validate()
        {
            return _data != null && _data.Length > 0;
        }
    }
}
