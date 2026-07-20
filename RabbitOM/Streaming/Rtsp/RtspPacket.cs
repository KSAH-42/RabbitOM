using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a packet
    /// </summary>
    public class RtspPacket
    {
        private readonly byte[] _data = null;



        /// <summary>
        /// Represent a null object value
        /// </summary>
        public readonly static RtspPacket Null = new RtspPacket();

        
        
        /// <summary>
        /// Constructor
        /// </summary>
        private RtspPacket()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">the data</param>
        public RtspPacket( byte[] data )
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
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public virtual bool TryValidate()
        {
            return _data != null && _data.Length > 0;
        }
    }
}
