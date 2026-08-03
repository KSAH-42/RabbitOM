using System;
using System.Threading;

namespace RabbitOM.Streaming.Rtsp.Clients
{
    public sealed class RtspCommunicationCounter
    {
        private long _ticks;
        private long _connectionsCountSucceed;
        private long _connectionsCountError;
        private long _packetReceivedCount;
        private long _bytesReceivedCount;




        public long ConnectionsCountSucceed
        {
            get => Volatile.Read( ref _connectionsCountSucceed );
        }

        public long ConnectionsCountErrors
        {
            get => Volatile.Read( ref _connectionsCountError );
        }

        public long PacketReceivedPerSecond
        {
            get => GetAverageValue( ref _packetReceivedCount );
        }

        public long BytesReceivedPerSecond
        {
            get => GetAverageValue( ref _bytesReceivedCount );
        }
        



        public void IncreaseConnectionsSucceed()
        {
            IncrementValue( ref _connectionsCountSucceed );
        }

        public void IncreaseConnectionsFailure()
        {
            IncrementValue( ref _connectionsCountError );
        }

        public void AddPacketReceived( long value )
        {
            IncrementValue( ref _packetReceivedCount , value );
        }

        public void AddBytesReceived( long value )
        {
            IncrementValue( ref _bytesReceivedCount , value );
        }

        public void Clear()
        {
            Interlocked.Exchange( ref _ticks , 0 );
            Interlocked.Exchange( ref _connectionsCountSucceed , 0 );
            Interlocked.Exchange( ref _connectionsCountError , 0 );
            Interlocked.Exchange( ref _bytesReceivedCount , 0 );
            Interlocked.Exchange( ref _packetReceivedCount , 0 );
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

        private long GetAverageValue( ref long valueMember )
        {
            var totalSeconds = (long) TimeSpan.FromTicks( Environment.TickCount - _ticks ).TotalSeconds;

            var result = totalSeconds > 0 ? valueMember / totalSeconds : valueMember ;

            valueMember = 0;

            return result;
        }
    }
}
