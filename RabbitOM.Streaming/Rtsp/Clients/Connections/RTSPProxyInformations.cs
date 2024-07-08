using System;

namespace RabbitOM.Streaming.Rtsp.Clients.Connections
{
    /// <summary>
    /// Represent the proxy infos
    /// </summary>
    internal sealed class RTSPProxyInformations
    {
        /// <summary>
        /// Represent the default sequence identifier
        /// </summary>
        public const int          DefaultSequenceId  = 1;




        private readonly object   _lock              = new object();

        private int               _sequenceId        = DefaultSequenceId;

        private string            _sessionId         = string.Empty;




        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot
        {
            get => _lock;
        }

        /// <summary>
        /// Gets the sequence identifier
        /// </summary>
        public int SequenceId
        {
            get
            {
                lock ( _lock )
                {
                    return _sequenceId;
                }
            }
        }

        /// <summary>
        /// Gets the session identifier
        /// </summary>
        public string SessionId
        {
            get
            {
                lock ( _lock )
                {
                    return _sessionId;
                }
            }
        }




        /// <summary>
        /// Increment the sequence identifier
        /// </summary>
        /// <returns>returns the next value</returns>
        public int GetNextSequenceIdentifier()
        {
            lock ( _lock )
            {
                if ( _sequenceId < 0 || _sequenceId == int.MaxValue )
                {
                    _sequenceId = 0;
                }

                return ++_sequenceId;
            }
        }

        /// <summary>
        /// Reset the sequence identifier
        /// </summary>
        public void ResetSequenceIdentifier()
        {
            lock ( _lock )
            {
                _sequenceId = DefaultSequenceId;
            }
        }

        /// <summary>
        /// Set the session identifier
        /// </summary>
        /// <param name="value">the session identifier</param>
        public void SetSessionId( string value )
        {
            lock ( _lock )
            {
                _sessionId = RTSPDataConverter.Trim( value );
            }
        }

        /// <summary>
        /// Clear the session identifier
        /// </summary>
        public void ClearSessionId()
        {
            lock ( _lock )
            {
                _sessionId = string.Empty;
            }
        }

        /// <summary>
        /// Reset all
        /// </summary>
        public void ResetAll()
        {
            lock ( _lock )
            {
                _sequenceId = DefaultSequenceId;
                _sessionId  = string.Empty;
            }
        }
    }
}
