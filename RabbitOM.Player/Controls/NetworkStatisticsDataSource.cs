using System;
using System.Threading;

namespace RabbitOM.Player.Controls
{
    public sealed class NetworkStatisticsDataSource : IDataSource
    {
        private long _ticks;
        private long _connectionStatus;
        private long _connectionsCount;
        private long _bytesReceivedCount;
        private long _packetReceivedCount;
        private long _frameCount;
        private volatile string _codec;




        public string Codec
        {
            get => _codec;
        }

        public bool ConnectionStatus
        {
            get => Volatile.Read( ref _connectionStatus ) != 0;
        }

        public long ConnectionsCount
        {
            get => Volatile.Read( ref _connectionsCount );
        }

        public long BytesReceivedPerSecond
        {
            get => GetAverageValue( ref _bytesReceivedCount );
        }

        public long PacketReceivedPerSecond
        {
            get => GetAverageValue( ref _packetReceivedCount );
        }

        public long FrameCountPerSecond
        {
            get => GetAverageValue( ref _frameCount );
        }




        public void Clear()
        {
            _codec = null;

            Interlocked.Exchange( ref _ticks , 0 );
            Interlocked.Exchange( ref _connectionStatus , 0 );
            Interlocked.Exchange( ref _connectionsCount , 0 );
            Interlocked.Exchange( ref _frameCount , 0 );
            Interlocked.Exchange( ref _bytesReceivedCount , 0 );
            Interlocked.Exchange( ref _packetReceivedCount , 0 );
        }

        public void SetCodec( string value )
        {
            _codec = value;
        }

        public void SetConnectionStatusOn()
        {
            SetValue( ref _connectionStatus , 1 );
        }

        public void SetConnectionStatusOff()
        {
            SetValue( ref _connectionsCount , 0 );
        }

        public void IncreaseConnectionsSucceed()
        {
            IncrementValue( ref _connectionsCount );
        }

        public void IncreasePacketReceived()
        {
            IncrementValue( ref _packetReceivedCount );
        }

        public void AddBytesReceived( long value )
        {
            IncrementValue( ref _bytesReceivedCount , value );
        }

        public void IncreaseFrameCount()
        {
            IncrementValue( ref _frameCount );
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
