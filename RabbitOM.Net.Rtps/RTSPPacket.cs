using System;

namespace RabbitOM.Net.Rtps
{
    /// <summary>
    /// Represent a packet
    /// </summary>
    public class RTSPPacket
    {
        private readonly byte[] _data = null;



        /// <summary>
        /// Represent a null object value
        /// </summary>
        public readonly static RTSPPacket Null = new RTSPPacket();

        
        
        /// <summary>
        /// Constructor
        /// </summary>
        private RTSPPacket()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="data">the data</param>
        public RTSPPacket( byte[] data )
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
        public virtual bool Validate()
        {
            return _data != null && _data.Length > 0;
        }
    }
}
