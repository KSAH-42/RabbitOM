/*
  The next implementation of the rtp layer
 
 */

using System;
using System.Collections.Generic;

namespace RabbitOM.Net.Rtsp.Tests
{
	public sealed class RTPPacket
	{
        public byte Version { get; private set; }
        public bool Padding  { get; private set; }
        public bool HasExtension { get; private set; }
        public ushort CSRC { get; private set; }
        public bool Marker { get; private set; }
        public byte PayloadType { get; private set; }
        public uint SequenceNumber { get; private set; }
        public uint Timestamp { get; private set; }
        public uint SSRC { get; private set; }
        public uint ExtensionId { get; private set; }
        public byte[] Data { get; private set; }
        public byte[] ExtensionData { get; private set; }
        public int[] CSRCIds { get; private set; }

        public bool TryValidate()
            => Version != 2 || Data == null || Data.Length <= 0 ? false : true;

        public static bool TryParse( byte[] buffer , out RTPPacket result )
        {
            result = null;

            if ( buffer == null || buffer.Length <= 11 )
            {
                return false;
            }

            result = new RTPPacket();

            result.Version         = (byte) ( ( buffer[ 0 ] & 0xC0 ) >> 6 );
            result.Padding         = (byte) ( ( buffer[ 0 ] & 0x20 ) >> 5 ) == 1;
            result.HasExtension    = (byte) ( ( buffer[ 0 ] & 0x10 ) >> 4 ) == 1;
            result.CSRC            = (ushort) ( buffer[ 0 ] & 0x0F );

            result.Marker          = (byte) ( ( buffer[ 1 ] & 0x80 ) ) != 0;
            result.PayloadType     = (byte) ( buffer[ 1 ] & 0x07F );
            result.SequenceNumber += ( (uint) buffer[ 2 ] << 8 );
            result.SequenceNumber += ( (uint) buffer[ 3 ] );
            result.Timestamp      += ( (uint) buffer[ 4 ] << 24 );
            result.Timestamp      += ( (uint) buffer[ 5 ] << 16 );
            result.Timestamp      += ( (uint) buffer[ 6 ] << 8 );
            result.Timestamp      += ( (uint) buffer[ 7 ] << 0 );
            result.SSRC           += ( (uint) buffer[ 8 ] << 24 );
            result.SSRC           += ( (uint) buffer[ 9 ] << 16 );
            result.SSRC           += ( (uint) buffer[ 10 ] << 8 );
            result.SSRC           += ( (uint) buffer[ 11 ] );

            result.SequenceNumber = result.SequenceNumber % ( ushort.MaxValue + 1 );

            uint startIndex = 12;

            if ( result.CSRC > 0 )
            {
                result.CSRCIds = new int[ result.CSRC ];

                for ( uint i = 0 ; i < result.CSRCIds.Length && ( startIndex + i ) < buffer.Length ; ++i )
                {
                    result.CSRCIds[ i ] += buffer[ startIndex + i ] << 24; startIndex++;
                    result.CSRCIds[ i ] += buffer[ startIndex + i ] << 16; startIndex++;
                    result.CSRCIds[ i ] += buffer[ startIndex + i ] << 8; startIndex++;
                    result.CSRCIds[ i ] += buffer[ startIndex + i ]; startIndex++;
                }
            }

            if ( result.HasExtension )
            {
                result.ExtensionId = ( (uint) buffer[ startIndex + 0 ] << 8 ) + (uint) ( buffer[ startIndex + 1 ] << 0 );

                uint extenstionSize = ( (uint) buffer[ startIndex + 2 ] << 8 ) + (uint) ( buffer[ startIndex + 3 ] << 0 ) * 4;

                startIndex += (uint) extenstionSize + 4;
            }

            if ( startIndex >= buffer.Length )
            {
                return false;
            }

            result.Data = new byte[ buffer.Length - startIndex ];

            Buffer.BlockCopy( buffer , (int) startIndex , result.Data , 0 , result.Data.Length );

            return true;
        }
    }

    public sealed class RTPFrame
    {
        private readonly RTPPacket[] _packets;

        public RTPFrame( RTPPacket[] packets )
		    => _packets = packets;

        public RTPPacket[] Packets { get => _packets; }
    }

    public abstract class RTPFrameBuilder : IDisposable
    {
        public abstract bool TryAppendPacket( byte[] buffer );
        public abstract bool CanBuildFrame();
        public abstract RTPFrame BuildFrame();
        public abstract void Dispose();
        public abstract void Clear();
    }

    public sealed class DefaultRTPFrameBuilder : RTPFrameBuilder
    {
        private readonly object _lock = new object();
        private readonly Queue<RTPPacket> _packets = new Queue<RTPPacket>();
        private readonly Queue<int> _sizes = new Queue<int>();
        private RTPPacket _lastPacket;

        public RTPPacket LastPacket
        {
            get 
            {
                lock ( _lock )
                {
                    return _lastPacket;
                }
            }
        }

        public override bool TryAppendPacket( byte[] buffer )
        {
            if ( ! RTPPacket.TryParse( buffer , out RTPPacket packet  ) )
            {
				return false;
            }

            lock ( _lock )
            {
                _packets.Enqueue( packet );

                if ( packet.Marker )
                {
                    _sizes.Enqueue( _packets.Count );
                }

                _lastPacket = packet;
            }

            return true;
        }

        public override bool CanBuildFrame()
        {
            lock ( _lock )
            {
                return _sizes.Count > 0;
            }
        }

        public override RTPFrame BuildFrame()
        {
            lock ( _lock )
            {
                RTPPacket[] packets = new RTPPacket[ _sizes.Dequeue() ];

                int size = packets.Length;

                while ( size > 0 )
                {
                    packets[ packets.Length - size ] = _packets.Dequeue();

                    size--;
                }

                return new RTPFrame( packets );
            }
        }

        public override void Dispose()
        {
            Clear();
        }

        public override void Clear()
        {
            lock ( _lock )
            {
                _packets.Clear();
                _sizes.Clear();
                _lastPacket = null;
            }
        }
    }

    public class RTPPacketReceivedEventArgs : EventArgs
    {
        private readonly RTPPacket _packet;

		public RTPPacketReceivedEventArgs( RTPPacket packet )
    		=> _packet = packet;
    
        public RTPPacket Packet { get => _packet; }
    }

    public class RTPFrameReceivedEventArgs : EventArgs
    {
        private readonly RTPFrame _frame;

        public RTPFrameReceivedEventArgs( RTPFrame frame )
            => _frame = frame;

        public RTPFrame Frame { get => _frame; }
    }

    public abstract class RTPSink : IDisposable
    {
        public event EventHandler<RTPPacketReceivedEventArgs> PacketReceived;
        public event EventHandler<RTPFrameReceivedEventArgs> FrameReceived;

        public abstract void Write( byte[] packet );
        public abstract void Reset();
        public abstract void Dispose();

        protected virtual void OnPacketReceived( RTPPacketReceivedEventArgs e )
        {
            PacketReceived?.Invoke( this , e );
        }

        protected virtual void OnFrameReceived( RTPFrameReceivedEventArgs e )
        {
            FrameReceived?.Invoke( this , e );
        }
    }

    public sealed class DefaultRTPSink : RTPSink
    {
        private readonly DefaultRTPFrameBuilder _builder = new DefaultRTPFrameBuilder();

        public override void Write( byte[] packet )
        {
            if ( ! _builder.TryAppendPacket( packet ) )
                return;

            OnPacketReceived( new RTPPacketReceivedEventArgs( _builder.LastPacket ) );

            if ( _builder.CanBuildFrame() )
            {
                OnFrameReceived( new RTPFrameReceivedEventArgs( _builder.BuildFrame() ) );
            }
        }
        public override void Reset()
        {
            _builder.Clear();
        }
        public override void Dispose()
        {
            _builder.Dispose();
        }
    }
}