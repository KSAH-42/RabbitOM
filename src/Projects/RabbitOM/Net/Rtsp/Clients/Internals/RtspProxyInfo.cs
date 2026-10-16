using System;

namespace RabbitOM.Net.Rtsp.Clients
{
    internal sealed class RtspProxyInfo
    {
        private const int DefaultSequenceId  = 1;


        private readonly object _lock = new object();
        private int _sequenceId = DefaultSequenceId;
        private string _sessionId = string.Empty;


        public object SyncRoot
        {
            get => _lock;
        }

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

        public void ResetSequenceIdentifier()
        {
            lock ( _lock )
            {
                _sequenceId = DefaultSequenceId;
            }
        }

        public void SetSessionId( string value )
        {
            lock ( _lock )
            {
                _sessionId = RtspDataConverter.Trim( value );
            }
        }

        public void ClearSessionId()
        {
            lock ( _lock )
            {
                _sessionId = string.Empty;
            }
        }

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
