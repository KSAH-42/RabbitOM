using System;

namespace RabbitOM.Player.Controls
{
    using RabbitOM.Threading;

    public sealed class NetworkStatisticsDataSource : IStatisticsDataSource
    {
        private readonly ReaderWriterLockProvider _provider = new ReaderWriterLockProvider();

        private string _codec;
        private string _transport;
        private bool _connectionStatus;
        private long _clock;
        private long _bytesReceivedCount;
        private long _packetReceivedCount;
        private long _frameCount;
        private long _frameHeigth;
        private long _frameWidth;
        private long _packetsLostCount;
        private long _maxFrameCount;
        private long _maxFrameCountPerSecond;
        private long _maxBytesReceivedCount;
        private long _maxBytesReceivedPerSecond;
        private long _maxPacketReceivedCount;
        private long _maxPacketReceivedPerSecond;
        private long _ticks;

        public string GetCodec()
        {
            return _codec;
        }

        public string GetTransport()
        {
            return _transport;
        }

        public long GetClock()
        {
            using ( _provider.ReaderLock )
            {
                return _clock;
            }
        }

        public bool GetConnectionStatus()
        {
            using ( _provider.ReaderLock )
            {
                return _connectionStatus;
            }
        }

        public long GetBytesReceivedPerSecond()
        {
            using ( _provider.ReaderLock )
            {
                return NetworkStatisticsHelper.GetAverageValue( ref _bytesReceivedCount , ref _ticks );
            }
        }

        public long GetPacketReceivedPerSecond()
        {
            using ( _provider.ReaderLock )
            {
                return NetworkStatisticsHelper.GetAverageValue( ref _packetReceivedCount , ref _ticks );
            }
        }

        public long GetFrameCountPerSecond()
        {
            using ( _provider.ReaderLock )
            {
                return NetworkStatisticsHelper.GetAverageValue( ref _frameCount , ref _ticks );
            }
        }

        public long GetFrameHeight()
        {
            using ( _provider.ReaderLock )
            {
                return _frameHeigth;
            }
        }

        public long GetFrameWidth()
        {
            using ( _provider.ReaderLock )
            {
                return _frameWidth;
            }
        }

        public long GetPacketsLostCount()
        {
            using ( _provider.ReaderLock )
            {
                return _packetsLostCount;
            }
        }

        public long GetMaxFrameCountPerSecond()
        {
            using ( _provider.ReaderLock )
            {
                return NetworkStatisticsHelper.GetAverageValue( ref _maxFrameCount , ref _maxFrameCountPerSecond , ref _ticks );
            }
        }

        public long GetMaxBytesReceivedPerSecond()
        {
            using ( _provider.ReaderLock )
            {
                return NetworkStatisticsHelper.GetAverageValue( ref _maxBytesReceivedCount , ref _maxBytesReceivedPerSecond , ref _ticks );
            }
        }

        public long GetMaxPacketReceivedPerSecond()
        {
            using ( _provider.ReaderLock )
            {
                return NetworkStatisticsHelper.GetAverageValue( ref _maxPacketReceivedCount , ref _maxPacketReceivedPerSecond , ref _ticks );
            }
        }

        public void Clear()
        {
            using ( _provider.WriterLock )
            {
                _codec = default;
                _transport = default;
                _ticks = default;
                _connectionStatus = default;
                _clock = default;
                _frameCount = default;
                _bytesReceivedCount = default;
                _packetReceivedCount = default;
                _frameHeigth = default;
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

        internal void SetCodec( string value )
        {
            _codec = value;
        }

        internal void SetTransport( string value )
        {
            _transport = value;
        }

        internal void SetConnectionStatusOn()
        {
            using ( _provider.ReaderLock )
            {
                _connectionStatus = true;
            }
        }

        internal void SetConnectionStatusOff()
        {
            using ( _provider.ReaderLock )
            {
                _connectionStatus = false;
            }
        }

        internal void SetClock( long value )
        {
            using ( _provider.ReaderLock )
            {
                _clock = value;
            }
        }

        internal void SetFrameSize( long height , long width )
        {
            using ( _provider.ReaderLock )
            {
                _frameHeigth = height;
                _frameWidth = width;
            }
        }

        internal void AddPacketsLost( long value )
        {
            using ( _provider.ReaderLock )
            {
                NetworkStatisticsHelper.IncrementValue( ref _packetsLostCount , value , ref _ticks );
            }
        }

        internal void AddBytesReceived( long value )
        {
            using ( _provider.ReaderLock )
            {
                NetworkStatisticsHelper.IncrementValue( ref _bytesReceivedCount , value , ref _ticks );
                NetworkStatisticsHelper.IncrementValue( ref _maxBytesReceivedCount , value , ref _ticks );
            }
        }

        internal void IncreasePacketReceived()
        {
            using ( _provider.ReaderLock )
            {
                NetworkStatisticsHelper.IncrementValue( ref _packetReceivedCount , 1 , ref _ticks );
                NetworkStatisticsHelper.IncrementValue( ref _maxPacketReceivedCount , 1 , ref _ticks );
            }
        }

        internal void IncreaseFrameCount()
        {
            using ( _provider.ReaderLock )
            {
                NetworkStatisticsHelper.IncrementValue( ref _frameCount , 1 , ref _ticks );
                NetworkStatisticsHelper.IncrementValue( ref _maxFrameCount , 1 , ref _ticks );
            }
        }
    }
}
