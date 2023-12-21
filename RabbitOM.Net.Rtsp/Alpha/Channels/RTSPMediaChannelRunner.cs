using System;

namespace RabbitOM.Net.Rtsp.Alpha
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



        // Inside this method, do we need to use a state machine class instead ?

        public void Run()
        {
            if ( ! _channel.IsOpened )
            {
                _idleTimeout = _channel.Configuration.RetriesInterval;

                if ( !_channel.Open() )
                    return;

                using ( var scope = new RTSPDisposeScope( () => _channel.Close() ) )
                {
                    if ( ! _channel.Options() )
                        return;
                    if ( ! _channel.Describe() )
                        return;
                    if ( ! _channel.Setup() )
                        return;
                    
                    scope.AddAction( () => _channel.TearDown() );
                    
                    if ( ! _channel.Play() )
                        return;
                    
                    scope.ClearActions();
                }

                _idleTimeout = _channel.Configuration.KeepAliveInterval;
            }
            else
            {
                if ( _channel.KeepAlive() )
                    return;

                if ( _channel.IsSetup )
                    _channel.TearDown();

                _channel.Close();

                _idleTimeout = _channel.Configuration.RetriesInterval;
            }
        }

        public void Dispose()
        {
            if ( _channel.IsSetup )
                _channel.TearDown();

            _channel.Close();
        }
    }
}
