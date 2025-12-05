using System;

namespace RabbitOM.Streaming.Net.Rtsp
{
    /// <summary>
    /// Represent the message version
    /// </summary>
    public sealed class RtspMessageVersion
    {
        /// <summary>
        /// Represent the default version 1.0
        /// </summary>
        public readonly static RtspMessageVersion   Version_1_0    = new RtspMessageVersion( 1 , 0 );

        /// <summary>
        /// Represent the default version 1.1
        /// </summary>
        public readonly static RtspMessageVersion   Version_1_1    = new RtspMessageVersion( 1 , 1 );

        /// <summary>
        /// Represent the default version 1.2
        /// </summary>
        public readonly static RtspMessageVersion   Version_1_2    = new RtspMessageVersion( 1 , 2 );






        /// <summary>
        /// The major number
        /// </summary>
        private readonly int _majorNumber   = 0;

        /// <summary>
        /// The minor number
        /// </summary>
        private readonly int _minorNumber   = 0;






        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="majorNumber">the major version</param>
        /// <param name="minorNumber">the minor version</param>
        public RtspMessageVersion( int majorNumber , int minorNumber )
        {
            _majorNumber = majorNumber;
            _minorNumber = minorNumber;
        }






        /// <summary>
        /// Gets the major version
        /// </summary>
        public int MajorNumber
        {
            get => _majorNumber;
        }

        /// <summary>
        /// Gets the minor version
        /// </summary>
        public int MinorNumber
        {
            get => _minorNumber;
        }






        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        internal bool TryValidate()
        {
            if ( _majorNumber == 0 && _minorNumber == 0 )
            {
                return false;
            }

            if ( _majorNumber < 0 || _minorNumber < 0 )
            {
                return false;
            }

            return true;
        }
    }
}
