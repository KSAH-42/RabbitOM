using System;

namespace RabbitOM.Net.Rtsp
{
    /// <summary>
    /// Represent the message status
    /// </summary>
    public sealed class RTSPMessageStatus
    {
        /// <summary>
        /// Represent a null object value
        /// </summary>
        public  readonly static RTSPMessageStatus UnDefined = new RTSPMessageStatus( RTSPStatusCode.UnDefined , "Un defined" );






        /// <summary>
        /// The code
        /// </summary>
        private readonly RTSPStatusCode           _code     = RTSPStatusCode.UnDefined;

        /// <summary>
        /// The reson
        /// </summary>
        private readonly string                   _reason   = string.Empty;






        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">the status code</param>
        /// <param name="reason">the status details</param>
        public RTSPMessageStatus( RTSPStatusCode code , string reason )
        {
            _code   = code;
            _reason = RTSPDataConverter.Trim( reason );
        }






        /// <summary>
        /// Gets the status code
        /// </summary>
        public RTSPStatusCode Code
        {
            get => _code;
        }

        /// <summary>
        /// Gets the reason
        /// </summary>
        public string Reason
        {
            get => _reason;
        }






        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        internal bool TryValidate()
        {
            return _code != RTSPStatusCode.UnDefined;
        }
    }
}
