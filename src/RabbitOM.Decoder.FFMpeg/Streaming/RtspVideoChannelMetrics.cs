namespace RabbitOM.Decoder.FFMpeg.Streaming
{
	using RabbitOM.Threading;

	public sealed class RtspVideoChannelMetrics
	{
        private readonly ReaderWriterLockProvider _provider = new ReaderWriterLockProvider();

        private string? _codec;
        private string? _transport;
        private long _clock;
        private long _bytesReceivedCount;
        private long _packetReceivedCount;
        private long _frameCount;
        private long _frameHeight;
        private long _frameWidth;
        private long _packetsLostCount;
        private long _maxFrameCount;
        private long _maxFrameCountPerSecond;
        private long _maxBytesReceivedCount;
        private long _maxBytesReceivedPerSecond;
        private long _maxPacketReceivedCount;
        private long _maxPacketReceivedPerSecond;
        private long _ticks;








        public string? Codec
        {
            get => _codec ?? string.Empty;
        }

        public string? Transport
        {
            get => _transport ?? string.Empty;
        }

        public long Clock
        {
            get
            {
                using ( _provider.ReaderLock )
                {
                    return _clock;
                }
            }
        }

        public long BytesReceivedPerSecond
        {
            get
            {
                using ( _provider.ReaderLock )
                {
                    return GetAverageValue( ref _bytesReceivedCount , ref _ticks );
                }
            }
        }

        public long PacketReceivedPerSecond
        {
            get
            {
                using ( _provider.ReaderLock )
                {
                    return GetAverageValue( ref _packetReceivedCount , ref _ticks );
                }
            }
        }

        public long FrameCountPerSecond
        {
            get
            {
                using ( _provider.ReaderLock )
                {
                    return GetAverageValue( ref _frameCount , ref _ticks );
                }
            }
        }

        public long FrameHeight
        {
            get
            {
                using ( _provider.ReaderLock )
                {
                    return _frameHeight;
                }
            }
        }

        public long FrameWidth
        {
            get
            {
                using ( _provider.ReaderLock )
                {
                    return _frameWidth;
                }
            }
        }

        public long PacketsLostCount
        {
            get
            {
                using ( _provider.ReaderLock )
                {
                    return _packetsLostCount;
                }
            }
        }

        public long MaxFrameCountPerSecond
        {
            get
            {
                using ( _provider.ReaderLock )
                {
                    return GetAverageValue( ref _maxFrameCount , ref _maxFrameCountPerSecond , ref _ticks );
                }
            }
        }

        public long MaxBytesReceivedPerSecond
        {
            get
            {
                using ( _provider.ReaderLock )
                {
                    return GetAverageValue( ref _maxBytesReceivedCount , ref _maxBytesReceivedPerSecond , ref _ticks );
                }
            }
        }

        public long MaxPacketReceivedPerSecond
        {
            get
            {
                using ( _provider.ReaderLock )
                {
                    return GetAverageValue( ref _maxPacketReceivedCount , ref _maxPacketReceivedPerSecond , ref _ticks );
                }
            }
        }









        public static void IncrementValue( ref long memberValue , long value , ref long ticks )
        {
            if ( value < 0 )
            {
                throw new ArgumentException( nameof( value ) );
            }

            var sum = memberValue + value;

            memberValue = sum > 0 ? sum : long.MaxValue;

            ticks = DateTime.Now.Ticks;
        }

        public static long GetAverageValue( ref long memberValue , ref long ticks )
        {
            var totalSeconds = (long) TimeSpan.FromTicks( DateTime.Now.Ticks - ticks ).TotalSeconds;

            var result = totalSeconds > 0 ? memberValue / totalSeconds : memberValue ;

            memberValue = 0;

            return result;
        }

        public static long GetAverageValue( ref long memberValue , ref long maxMemberValue , ref long ticks )
        {
            var totalSeconds = (long) TimeSpan.FromTicks( DateTime.Now.Ticks - ticks ).TotalSeconds;

            var result = totalSeconds > 0 ? memberValue / totalSeconds : memberValue ;

            if ( maxMemberValue < result )
            {
                maxMemberValue = result;
            }

            memberValue = 0;

            return maxMemberValue;
        }











        private void SetCodec( string value )
        {
            _codec = value;
        }

        private void SetTransport( string value )
        {
            _transport = value;
        }

        private void SetClock( long value )
        {
            using ( _provider.WriterLock )
            {
                _clock = value;
            }
        }

        private void SetFrameSize( long height , long width )
        {
            using ( _provider.WriterLock )
            {
                _frameHeight = height;
                _frameWidth = width;
            }
        }

        private void AddPacketsLost( long value )
        {
            using ( _provider.WriterLock )
            {
                IncrementValue( ref _packetsLostCount , value , ref _ticks );
            }
        }

        private void AddBytesReceived( long value )
        {
            using ( _provider.WriterLock )
            {
                IncrementValue( ref _bytesReceivedCount , value , ref _ticks );
                IncrementValue( ref _maxBytesReceivedCount , value , ref _ticks );
            }
        }

        private void IncreasePacketReceived()
        {
            using ( _provider.WriterLock )
            {
                IncrementValue( ref _packetReceivedCount , 1 , ref _ticks );
                IncrementValue( ref _maxPacketReceivedCount , 1 , ref _ticks );
            }
        }

        private void IncreaseFrameCount()
        {
            using ( _provider.WriterLock )
            {
                IncrementValue( ref _frameCount , 1 , ref _ticks );
                IncrementValue( ref _maxFrameCount , 1 , ref _ticks );
            }
        }

        private void Clear()
        {
            using ( _provider.WriterLock )
            {
                _codec = default;
                _transport = default;
                _ticks = default;
                _clock = default;
                _frameCount = default;
                _bytesReceivedCount = default;
                _packetReceivedCount = default;
                _frameHeight = default;
                _frameWidth = default;
                _packetsLostCount = default;
                _maxFrameCount = default;
                _maxFrameCountPerSecond = default;
                _maxBytesReceivedCount = default;
                _maxBytesReceivedPerSecond = default;
                _maxPacketReceivedCount = default;
                _maxPacketReceivedPerSecond = default;
            }
        }








        internal sealed class Writer
        {
            private readonly RtspVideoChannelMetrics _metrics;
            public Writer( RtspVideoChannelMetrics metrics ) => _metrics = metrics ?? throw new ArgumentNullException( nameof( metrics ) );
            public void Clear() => _metrics.Clear();
            public void SetCodec( string value ) => _metrics.SetCodec( value );
            public void SetTransport( string value ) => _metrics.SetTransport( value );
            public void SetClock( long value ) => _metrics.SetClock( value );
            public void SetFrameSize( long height , long width ) => _metrics.SetFrameSize( height , width );
            public void AddPacketsLost( long value ) => _metrics.AddPacketsLost( value );
            public void AddBytesReceived( long value ) => _metrics.AddBytesReceived( value );
            public void IncreasePacketReceived() => _metrics.IncreasePacketReceived();
            public void IncreaseFrameCount() => _metrics.IncreaseFrameCount();
        }
	}
}
