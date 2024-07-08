using System;

namespace RabbitOM.Streaming.Rtsp
{
    /// <summary>
    /// Represent a message response
    /// </summary>
    public sealed class RTSPMessageResponse : RTSPMessage
    {
        private readonly RTSPMessageStatus     _status      = null;

        private readonly RTSPHeaderCollection  _headers     = null;

        private readonly RTSPMessageBody       _body        = null;

        private readonly RTSPMessageVersion    _version     = null;




        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status">the status code</param>
        public RTSPMessageResponse( RTSPMessageStatus status )
            : this( status , RTSPMessageVersion.Version_1_0 )
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="status">the status code</param>
        /// <param name="version"></param>
        /// <exception cref="ArgumentNullException"/>
        public RTSPMessageResponse( RTSPMessageStatus status , RTSPMessageVersion version )
        {
            _status = status ?? throw new ArgumentNullException( nameof( status ) );
            _version = version ?? throw new ArgumentNullException( nameof( version ) );
            _headers = new RTSPHeaderCollection();
            _body = new RTSPMessageBody();
        }




        /// <summary>
        /// Gets the message status
        /// </summary>
        public RTSPMessageStatus Status
        {
            get => _status;
        }

        /// <summary>
        /// Gets the headers
        /// </summary>
        public override RTSPHeaderCollection Headers
        {
            get => _headers;
        }

        /// <summary>
        /// Gets the body
        /// </summary>
        public override RTSPMessageBody Body
        {
            get => _body;
        }

        /// <summary>
        /// Gets the message version
        /// </summary>
        public override RTSPMessageVersion Version
        {
            get => _version;
        }




        /// <summary>
        /// Validate
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public override bool TryValidate()
        {
            return _status.TryValidate()
                && _version.TryValidate()
                && _headers.ContainsKey( RTSPHeaderNames.CSeq );
        }

        /// <summary>
        /// Create an undefined message
        /// </summary>
        /// <returns>returns an instance</returns>
        internal static RTSPMessageResponse CreateUnDefinedResponse()
        {
            return new RTSPMessageResponse( RTSPMessageStatus.UnDefined );
        }
    }
}
