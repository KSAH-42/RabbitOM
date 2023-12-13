using System;

namespace RabbitOM.Net.Rtsp.Apha
{
    public sealed class RTSPMediaChannelRunner : IDisposable
    {
        private readonly IRTSPMediaChannel _channel;

        private TimeSpan _idleTimeout;





        public RTSPMediaChannelRunner( IRTSPMediaChannel channel )
        {
            _channel = channel ?? throw new ArgumentNullException( nameof( channel ) );
        }





        public TimeSpan IdleTimeout 
        { 
            get => _idleTimeout;
        }





        public void Run()
        {
            if ( ! _channel.IsConnected )
            {
                _idleTimeout = _channel.Configuration.RetriesInterval;

                if ( ! _channel.Connect() )
                {
                    return;
                }

                if ( ! _channel.StartStreaming() )
                {
                    _channel.Disconnect();
                }

                _idleTimeout = _channel.Configuration.KeepAliveInterval;
            }
            else
            {
                if ( _channel.KeepAlive() )
                {
                    return;
                }

                if ( _channel.IsStreamingStarted )
                {
                    _channel.StopStreaming();
                }

                _channel.Disconnect();

                _idleTimeout = _channel.Configuration.RetriesInterval;
            }
        }

        public void Dispose()
        {
            if ( _channel.IsStreamingStarted )
            {
                _channel.StopStreaming();
            }

            _channel.Disconnect();
        }
    }
}
