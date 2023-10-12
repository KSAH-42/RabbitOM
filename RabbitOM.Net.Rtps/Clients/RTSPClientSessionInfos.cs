using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Rtps.Clients
{
    /// <summary>
    /// Represent the client session data informations
    /// </summary>
    public sealed class RTSPClientSessionInfos
    {
        private readonly object                      _lock             = new object();

        private readonly RTSPClientSessionDescriptor _descriptor       = new RTSPClientSessionDescriptor();

        private readonly RTSPMethodTypeList          _supportedMethods = new RTSPMethodTypeList();

        private string                               _sessionId        = string.Empty;

        private bool                                 _isReady          = false;

        private bool                                 _isPlaying        = false;   



        /// <summary>
        /// Gets the sync root
        /// </summary>
        public object SyncRoot 
        { 
            get => _lock;
        }

        /// <summary>
        /// Gets the descriptor
        /// </summary>
        public RTSPClientSessionDescriptor Descriptor
        {
            get => _descriptor;
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
        /// Gets the ready status
        /// </summary>
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
        
        /// <summary>
        /// Gets the playing status
        /// </summary>
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

        /// <summary>
        /// Gets the supported methods
        /// </summary>
        public RTSPMethodTypeReadonlyList SupportedMethods
        {
            get => new RTSPMethodTypeReadonlyList( _supportedMethods );
        }

        


        /// <summary>
        /// Check if the session can be prepared
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
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

        /// <summary>
        /// Check if the session can be setup
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
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

        /// <summary>
        /// Check if the session can be start the playing
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
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

        /// <summary>
        /// Check if the session can be start the playing
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
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

        /// <summary>
        /// Check if the session id has been registered
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
        public bool IsSessionIdRegistered()
        {
            lock ( _lock )
            {
                return ! string.IsNullOrWhiteSpace( _sessionId );
            }
        }

        /// <summary>
        /// Register the session identifier and update internal status
        /// </summary>
        /// <param name="sessionId">the session identifier</param>
        /// <returns></returns>
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

        /// <summary>
        /// Un register the session and update some internals status members
        /// </summary>
        public void UnRegisterSessionId()
        {
            lock ( _lock )
            {
                _isReady   = false;
                _isPlaying = false;
                _sessionId = string.Empty;
            }
        }

        /// <summary>
        /// Change the playing status 
        /// </summary>
        /// <returns>returns true for a success, otherwise false</returns>
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

        /// <summary>
        /// Change the playing status 
        /// </summary>
        public void TurnOffPlayingStatus()
        {
            lock ( _lock )
            {
                _isPlaying = false;
            }
        }

        /// <summary>
        /// Add a supported method
        /// </summary>
        /// <param name="method">the method</param>
        /// <returns>return true for a success, otherwise false</returns>
        public bool AddSupportedMethod( RTSPMethodType method )
        {
            return _supportedMethods.Add( method );
        }

        /// <summary>
        /// Add a supported method
        /// </summary>
        /// <param name="methods">a collection of methods</param>
        /// <returns>return true for a success, otherwise false</returns>
        public bool AddSupportedMethods( IEnumerable<RTSPMethodType> methods )
        {
            return _supportedMethods.AddRange( methods ) > 0;
        }

        /// <summary>
        /// Reset internal members
        /// </summary>
        public void Reset()
        {
            lock ( _lock )
            {
                _supportedMethods.Clear();
                _descriptor.Release();
                _sessionId = string.Empty;
                _isReady   = false;
                _isPlaying = false;
            }
        }
    }
}
