using System;
using System.Threading;

namespace RabbitOM.Player.Controls
{
    public sealed class NetworkStatisticsDataSource : IDataSource
    {
        private volatile string _codec;
        private long _connectionStatus;
        private long _bytesReceivedCount;
        private long _packetReceivedCount;
        private long _frameCount;
        private long _frameHeigth;
        private long _frameWidth;
        private long _ticks;

        public string GetCodec()
        {
            return _codec;
        }

        public bool GetConnectionStatus()
        {
            return Volatile.Read( ref _connectionStatus ) != 0;
        }

        public long GetBytesReceivedPerSecond()
        {
            return GetAverageValue( ref _bytesReceivedCount );
        }

        public long GetPacketReceivedPerSecond()
        {
            return GetAverageValue( ref _packetReceivedCount );
        }

        public long GetFrameCountPerSecond()
        {
            return GetAverageValue( ref _frameCount );
        }

        public long GetFrameHeight()
        {
            return Volatile.Read( ref _frameHeigth );
        }

        public long GetFrameWidth()
        {
            return Volatile.Read( ref _frameWidth );
        }

        public void SetCodec( string value )
        {
            _codec = value;
        }

        public void SetConnectionStatusOn()
        {
            Interlocked.Exchange( ref _connectionStatus , 1 );
        }

        public void SetConnectionStatusOff()
        {
            Interlocked.Exchange( ref _connectionStatus , 0 );
        }

        public void SetFrameSize( long height , long width )
        {
            Interlocked.Exchange( ref _frameHeigth , height );
            Interlocked.Exchange( ref _frameWidth , width );
        }

        public void AddBytesReceived( long value )
        {
            IncrementValue( ref _bytesReceivedCount , value );
        }

        public void IncreasePacketReceived()
        {
            IncrementValue( ref _packetReceivedCount );
        }

        public void IncreaseFrameCount()
        {
            IncrementValue( ref _frameCount );
        }

        public void Clear()
        {
            _codec = null;

            Interlocked.Exchange( ref _ticks , 0 );
            Interlocked.Exchange( ref _connectionStatus , 0 );
            Interlocked.Exchange( ref _frameCount , 0 );
            Interlocked.Exchange( ref _bytesReceivedCount , 0 );
            Interlocked.Exchange( ref _packetReceivedCount , 0 );
            Interlocked.Exchange( ref _frameHeigth , 0 );
            Interlocked.Exchange( ref _frameWidth , 0 );
        }

        private void IncrementValue( ref long valueMember )
        {
            IncrementValue( ref valueMember , 1 );
        }

        private void IncrementValue( ref long valueMember , long value )
        {
            if ( value < 0 )
            {
                throw new ArgumentException( nameof( value ) );
            }

            Interlocked.Add( ref valueMember , value );
            Interlocked.Exchange( ref _ticks , Environment.TickCount );
        }

        private void SetValue( ref long valueMember , long value )
        {
            Interlocked.Exchange( ref valueMember , value );
            Interlocked.Exchange( ref _ticks , Environment.TickCount );
        }

        private long GetAverageValue( ref long valueMember )
        {
            var totalSeconds = (long) TimeSpan.FromTicks( Environment.TickCount - _ticks ).TotalSeconds;

            var result = totalSeconds > 0 ? valueMember / totalSeconds : valueMember ;

            valueMember = 0;

            return result;
        }
    }
}
