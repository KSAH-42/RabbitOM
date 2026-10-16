using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Rtsp.Clients
{
    internal sealed class RtspSessionInfos
    {
        private readonly object _lock = new object();
        private readonly RtspClientSessionDescriptor _descriptor = new RtspClientSessionDescriptor();
        private readonly RtspMethodList _supportedMethods = new RtspMethodList();
        private string _sessionId = string.Empty;
        private bool _isReady;
        private bool _isPlaying;


        public object SyncRoot 
        {
            get => _lock;
        }

        public RtspClientSessionDescriptor Descriptor
        {
            get => _descriptor;
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

        public bool IsReady
        {
            get
            {
                lock ( _lock )
                {
                    return _isReady;
                }
            }
        }

        public bool IsPlaying
        {
            get
            {
                lock ( _lock )
                {
                    return _isPlaying;
                }
            }
        }

        public RtspMethodReadonlyList SupportedMethods
        {
            get => new RtspMethodReadonlyList( _supportedMethods );
        }


        public bool CanPrepare()
        {
            lock ( _lock )
            {
                if ( _isReady || _isPlaying )
                {
                    return false;
                }

                if ( _descriptor.IsValid() )
                {
                    return false;
                }

                return true;
            }
        }

        public bool CanSetup()
        {
            lock ( _lock )
            {
                if ( ! _isReady || _isPlaying )
                {
                    return false;
                }

                if ( ! _descriptor.IsValid() )
                {
                    return false;
                }

                if ( ! string.IsNullOrWhiteSpace( _sessionId ) )
                {
                    return false;
                }

                return true;
            }
        }

        public bool CanPlay()
        {
            lock ( _lock )
            {
                if ( string.IsNullOrWhiteSpace( _sessionId ) )
                {
                    return false;
                }

                if ( ! _isReady || ! _isPlaying )
                {
                    return false;
                }

                return true;
            }
        }

        public bool CanTearDown()
        {
            lock ( _lock )
            {
                if ( string.IsNullOrWhiteSpace( _sessionId ) )
                {
                    return false;
                }

                return true;
            }
        }

        public bool IsSessionIdRegistered()
        {
            lock ( _lock )
            {
                return ! string.IsNullOrWhiteSpace( _sessionId );
            }
        }

        public bool RegisterSessionId( string sessionId )
        {
            lock ( _lock )
            {
                _isReady   = false;
                _isPlaying = false;

                _sessionId = sessionId ?? string.Empty;

                if ( string.IsNullOrWhiteSpace( _sessionId ) )
                {
                    return false;
                }

                _isReady = true;

                return true;
            }
        }

        public void UnRegisterSessionId()
        {
            lock ( _lock )
            {
                _isReady   = false;
                _isPlaying = false;
                _sessionId = string.Empty;
            }
        }

        public bool TurnOnPlayingStatus()
        {
            lock ( _lock )
            {
                _isPlaying = false;

                if ( string.IsNullOrWhiteSpace( _sessionId ) )
                {
                    return false;
                }

                if ( ! _isReady )
                {
                    return false;
                }

                _isPlaying = true;

                return true;
            }
        }

        public void TurnOffPlayingStatus()
        {
            lock ( _lock )
            {
                _isPlaying = false;
            }
        }

        public bool AddSupportedMethod( RtspMethod method )
        {
            return _supportedMethods.TryAdd( method );
        }

        public bool AddSupportedMethods( IEnumerable<RtspMethod> methods )
        {
            return _supportedMethods.TryAddRange( methods );
        }

        public void Reset()
        {
            lock ( _lock )
            {
                _supportedMethods.Clear();
                _descriptor.Clear();
                _sessionId = string.Empty;
                _isReady   = false;
                _isPlaying = false;
            }
        }
    }
}
