using RabbitOM.Net.Sdp;
using RabbitOM.Net.Sdp.Extensions;
using RabbitOM.Net.Rtsp.Codecs;
using RabbitOM.Net.Rtsp.Remoting;
using RabbitOM.Net.Rtsp.Remoting.Invokers;
using System;
using System.Linq;

namespace RabbitOM.Net.Rtsp.Alpha
{
    internal sealed class RTSPMediaService : IDisposable
    {
        private readonly object _lock;

        private readonly IRTSPEventDispatcher _dispatcher;

        private readonly IRTSPClientConfiguration _configuration;

        private readonly IRTSPConnection _connection;

        private readonly RTSPPipeLineCollection _pipeLines;

        private SessionDescriptor _sdp;

        private RTSPTrackInfo _trackInfo;

        private string _sessionId;





        public RTSPMediaService( IRTSPEventDispatcher dispatcher )
        {
            _dispatcher = dispatcher ?? throw new ArgumentNullException( nameof( dispatcher ) );
            
            _lock = new object();
            _configuration = new RTSPClientConfiguration();
            _connection = new RTSPConnection();
            _pipeLines = new RTSPPipeLineCollection();
        }




        public object SyncRoot
            => _lock;

        public RTSPPipeLineCollection PipeLines
            => _pipeLines;

        public IRTSPClientConfiguration Configuration
            => _configuration;
        
        public IRTSPEventDispatcher Dispatcher
            => _dispatcher;

        public bool IsConnected
            => _connection.IsConnected;
       
        public bool IsOpened
            => _connection.IsOpened;

        public bool IsDisposed
            => _connection.IsDisposed;

        public bool IsSetup
            => throw new NotImplementedException();
       
        public bool IsPlaying
            => throw new NotImplementedException();

        public bool IsStreamingStarted
            => throw new NotImplementedException();

        public bool IsReceivingPacket
            => throw new NotImplementedException();








        public bool Open()
        {
            if ( ! _connection.TryOpen( _configuration.Uri ) )
            {
                return false;
            }

            if ( ! _connection.TryConfigureTimeouts( _configuration.ReceiveTimeout , _configuration.SendTimeout ) )
            {
                _connection.Close();
                return false;
            }

            return true;
        }

        public void Close()
        {
            _connection.Close();
            ResetMembers();
        }

        public void Dispose()
        {
            _connection.Dispose();
            ResetMembers();
        }

        public void Abort()
        {
            _connection.Abort();
            ResetMembers();
        }

        // this method don't check the supported methods just ignored them 
        // because some devices returns an empty list of supported methods....
        public bool Options()
        {
            return _connection.Options().Invoke().Succeed;
        }

        public bool Describe()
        {
            RTSPInvokerResult result = _connection.Describe().Invoke();

            if ( result == null || ! result.Succeed )
            {
                return false;
            }

            lock ( _lock )
            {
                return SessionDescriptor.TryParse( result.Request.GetBody() , out _sdp );
            }
        }

        public void PrepareSetup()
        {
            lock ( _lock )
            {
                _trackInfo = null;

                if ( _sdp == null )
                {
                    return;
                }

                lock ( _configuration.SyncRoot )
                {
                    MediaTrack mediaTrack = null;

                    if ( _configuration.MediaFormat == RTSPMediaFormat.Video )
                    {
                        mediaTrack = _sdp.SelectVideoMediaTracks().FirstOrDefault();
                    }

                    if ( _configuration.MediaFormat == RTSPMediaFormat.Audio )
                    {
                        mediaTrack = _sdp.SelectAudioMediaTracks().FirstOrDefault();
                    }

                    if ( mediaTrack != null )
                    {
                        if ( _configuration.MediaFormat == RTSPMediaFormat.Video )
                        {
                            _trackInfo = RTSPTrackInfo.NewVideoTrackInfo( mediaTrack.RtpMap.PayloadType , mediaTrack.RtpMap.Encoding , mediaTrack.RtpMap.ClockRate , mediaTrack.ControlUri , mediaTrack.Format.ProfileLevelId , 
                                string.IsNullOrWhiteSpace( mediaTrack.Format.SPS ) ? CodecInfo.Default_H264_SPS : mediaTrack.Format.SPS  , 
                                string.IsNullOrWhiteSpace( mediaTrack.Format.PPS ) ? CodecInfo.Default_H264_PPS : mediaTrack.Format.PPS 
                                );
                        }

                        if ( _configuration.MediaFormat == RTSPMediaFormat.Audio )
                        {
                            _trackInfo = RTSPTrackInfo.NewAudioTrackInfo( mediaTrack.RtpMap.PayloadType , mediaTrack.RtpMap.Encoding , mediaTrack.RtpMap.ClockRate , mediaTrack.ControlUri , mediaTrack.Format.ProfileLevelId );
                        }
                    }
                }
            }
        }

        public bool SetupAsTcp()
        {
            lock ( _lock )
            {
                _sessionId = null;

                if ( _trackInfo == null )
                {
                    return false;
                }

                var result = _connection.Setup()
                    .As<RTSPSetupInvoker>().SetDeliveryMode( RTSPDeliveryMode.Tcp )
                    .As<RTSPSetupInvoker>().SetTrackUri( _trackInfo.ControlUri )
                    .Invoke();

                if ( result == null || ! result.Succeed )
                {
                    return false;
                }

                _sessionId = result.Response.GetHeaderSessionId();

                return ! string.IsNullOrWhiteSpace( _sessionId );
            }
        }

        public bool SetupAsUdp()
        {
            lock ( _lock )
            {
                _sessionId = null;

                if ( _trackInfo == null )
                {
                    return false;
                }

                var result = _connection.Setup()
                    .As<RTSPSetupInvoker>().SetDeliveryMode( RTSPDeliveryMode.Udp )
                    .As<RTSPSetupInvoker>().SetTrackUri( _trackInfo.ControlUri )
                    .As<RTSPSetupInvoker>().SetUnicastPort( _configuration.RtpPort )
                    .Invoke();

                if ( result == null || ! result.Succeed )
                {
                    return false;
                }

                _sessionId = result.Response.GetHeaderSessionId();

                return !string.IsNullOrWhiteSpace( _sessionId );
            }
        }

        public bool SetupAsMulticast()
        {
            lock ( _lock )
            {
                _sessionId = null;

                if ( _trackInfo == null )
                {
                    return false;
                }

                var result = _connection.Setup()
                    .As<RTSPSetupInvoker>().SetDeliveryMode( RTSPDeliveryMode.Multicast )
                    .As<RTSPSetupInvoker>().SetTrackUri( _trackInfo.ControlUri )
                    .As<RTSPSetupInvoker>().SetMulticastAddress( _configuration.MulticastAddress )
                    .As<RTSPSetupInvoker>().SetMulticastPort( _configuration.RtpPort )
                    .As<RTSPSetupInvoker>().SetMulticastTTL( _configuration.TimeToLive )
                    .Invoke();

                if ( result == null || !result.Succeed )
                {
                    return false;
                }

                _sessionId = result.Response.GetHeaderSessionId();

                return ! string.IsNullOrWhiteSpace( _sessionId );
            }
        }


        public bool Play()
            => throw new NotImplementedException();
        public bool TearDown()
            => throw new NotImplementedException();
        public bool KeepAliveAsOptions()
            => throw new NotImplementedException();
        public bool KeepAliveAsGetParameter()
            => throw new NotImplementedException();
        public bool KeepAliveAsSetParameter()
            => throw new NotImplementedException();

        public bool WaitForConnection( TimeSpan timeout )
        {
            return _connection.WaitForConnection( timeout );
        }

        public void UpdateReceivingStatus(bool status)
        {
            // TODO: add check + fire events
        }

        public void UpdateStreamingRunningStatus(bool status)
        {
            // TODO: add check + fire events
        }

        private void ResetMembers()
        {
            lock ( _lock )
            {
                _sdp = null;
                _trackInfo = null;
                _sessionId = null;
            }
        }
    }
}
