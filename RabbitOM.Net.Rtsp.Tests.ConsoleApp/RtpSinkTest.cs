/*
 EXPERIMENTATION of the next implementation of the rtp layer

              O P T I M I Z A T I O N S

 For next implementation, optimize can to reduce copy 
 by using array segment to remove Buffer.Copy or similar methods

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
        public int[] CSRCIdentifiers { get; private set; }

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

            result.Marker          = (byte) ((buffer[ 1 ] & 0x80 ) ) != 0;
            result.PayloadType     = (byte) ( buffer[ 1 ] & 0x07F );
            result.SequenceNumber += (uint) ( buffer[ 2 ]  << 8 );
            result.SequenceNumber += (uint) ( buffer[ 3 ] );
            result.Timestamp      += (uint) ( buffer[ 4 ]  << 24 );
            result.Timestamp      += (uint) ( buffer[ 5 ]  << 16 );
            result.Timestamp      += (uint) ( buffer[ 6 ]  << 8  );
            result.Timestamp      += (uint) ( buffer[ 7 ]  << 0  );
            result.SSRC           += (uint) ( buffer[ 8 ]  << 24 );
            result.SSRC           += (uint) ( buffer[ 9 ]  << 16 );
            result.SSRC           += (uint) ( buffer[ 10 ] << 8 );
            result.SSRC           += (uint) ( buffer[ 11 ] );

            result.SequenceNumber = result.SequenceNumber % ( ushort.MaxValue + 1 );

            uint startIndex = 12;

            if ( result.CSRC > 0 )
            {
                result.CSRCIdentifiers = new int[ result.CSRC ];

                for ( uint i = 0 ; i < result.CSRCIdentifiers.Length && ( startIndex + i ) < buffer.Length ; ++i )
                {
                    result.CSRCIdentifiers[ i ] += buffer[ startIndex + i ] << 24; startIndex++;
                    result.CSRCIdentifiers[ i ] += buffer[ startIndex + i ] << 16; startIndex++;
                    result.CSRCIdentifiers[ i ] += buffer[ startIndex + i ] << 8; startIndex++;
                    result.CSRCIdentifiers[ i ] += buffer[ startIndex + i ]; startIndex++;
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
        public RTPFrame( RTPPacket[] packets ) 
            => Packets = packets;
        public RTPPacket[] Packets { get; private set; }

        public bool TryValidate()
            => Packets == null || Packets.Length == 0 ? false : true
            ;
    }

    public abstract class RTPFrameBuilder : IDisposable
    {
        ~RTPFrameBuilder()
            => Dispose();

        public abstract bool TryAddPacket( byte[] buffer );
        public abstract bool CanBuildFrame();
        public abstract RTPFrame BuildFrame();
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }
        protected abstract void Dispose( bool disposing );
        public abstract void Clear();
    }

    public sealed class DefaultRTPFrameBuilder : RTPFrameBuilder
    {
        private readonly object _lock = new object();
        private readonly Queue<RTPPacket> _packets = new Queue<RTPPacket>();
        private readonly Queue<int> _sizes = new Queue<int>();
        private RTPPacket _lastPacket;
        private int _frameSize;

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

        public override bool TryAddPacket( byte[] buffer )
        {
            if ( ! RTPPacket.TryParse( buffer , out RTPPacket packet ) )
            {
                return false;
            }

            lock ( _lock )
            {
                _packets.Enqueue( packet );
                _lastPacket = packet;

                OnPacketAdded( packet );
            
                return true;
            }
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

                int index = 0;

                while ( index < packets.Length )
                {
                    packets[ index ++ ] = _packets.Dequeue();
                }

                return new RTPFrame( packets );
            }
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
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

        private void OnPacketAdded( RTPPacket packet )
        {
            _frameSize++;

            if ( packet.Marker )
            {
                _sizes.Enqueue( _frameSize );
                _frameSize = 0;
            }
        }
    }

    public class RTPPacketReceivedEventArgs : EventArgs
    {
        public RTPPacketReceivedEventArgs( RTPPacket packet ) => Packet = packet;
        public RTPPacket Packet { get; private set; }
    }

    public class RTPFrameReceivedEventArgs : EventArgs
    {
        public RTPFrameReceivedEventArgs( RTPFrame frame ) => Frame = frame;
        public RTPFrame Frame { get; private set; }
    }

    // TODO: maybe in this class we can add stats objects
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
        /// TODO: use the DIP and replace with an interface
        
        private readonly DefaultRTPFrameBuilder _builder = new DefaultRTPFrameBuilder();

        public override void Write( byte[] packet )
        {
            if ( ! _builder.TryAddPacket( packet ) )
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

namespace RabbitOM.Net.Rtsp.Tests
{
    public sealed class H264NalUnitCollection : Queue<H264NalUnit>
    {
        public bool IsEmpty { get => Count == 0; }

        public bool Any() => Count > 0;
    }

    public sealed class H264NalUnit
    {
        private static int DefaultMinimuLength = 4;
        private static byte[] StartPrefix_001 = new byte[] { 0 , 0 , 1 };
        private static byte[] StartPrefix_0001 = new byte[] { 0 , 0 , 0 , 1 };

        public bool ForbiddenBit { get; private set; }
        public byte NRI { get; private set; }
        public byte Type { get; private set; }
        public bool IsReserved { get; private set; }
        public bool IsSingle { get; private set; }
        public bool IsSTAP_A { get; private set; }
        public bool IsSTAP_B { get; private set; }
        public bool IsMTAP_A { get; private set; }
        public bool IsMTAP_B { get; private set; }
        public bool IsFU_A { get; private set; }
        public bool IsFU_B { get; private set; }
        public bool IsCodedSliceNIDR { get; private set; }
        public bool IsCodedSlicePartitionA { get; private set; }
        public bool IsCodedSlicePartitionB { get; private set; }
        public bool IsCodedSlicePartitionC { get; private set; }
        public bool IsCodedSliceIDR { get; private set; }
        public bool IsSEI { get; private set; }
        public bool IsSPS { get; private set; }
        public bool IsPPS { get; private set; }
        public bool IsAccessDelimiter { get; private set; }
        public byte[] Payload { get; private set; } 

        public bool TryValidate()
            => Payload != null && Payload.Length >= 1;

        // Time complexity O(N) 
        // TODO: add parsing tests
        // TODO: add tests for protocol violations

        public static bool TryParse( byte[] buffer , out H264NalUnit result )
        {
            result = default;

            if ( buffer == null || buffer.Length < DefaultMinimuLength )
                return false;

            /*
                +----------------------------------+
                | Start Code Prefix (3 or 4 bytes) |
                +----------------------------------+
             */

            int offset = Sum( buffer , StartPrefix_0001.Length ) == 1 ? 3
                       : Sum( buffer , StartPrefix_001 .Length ) == 1 ? 2 
                       : -1;

            if ( offset < 0 )
                return false;

            /*
                +------------------------------------------+
                | NAL Unit Header (Variable size)          |
                +------------------------------------------+
                | Forbidden Zero Bit | NRI | NAL Unit Type |
                +------------------------------------------+
             */

            result = new H264NalUnit();

            result.ForbiddenBit           = (buffer[ ++offset ] & 0x1) == 1;
            result.NRI                    = (byte) ( ( buffer[ offset ] >> 1 ) & 0x2 );
            result.Type                   = (byte) ( ( buffer[ offset ] >> 1 ) & 0x1F );

            result.IsReserved            |= result.Type == 0;
            result.IsSingle               = result.Type >= 1 && result.Type <= 23;
            result.IsSTAP_A               = result.Type == 24;
            result.IsSTAP_B               = result.Type == 25;
            result.IsMTAP_A               = result.Type == 26;
            result.IsMTAP_B               = result.Type == 27;
            result.IsFU_A                 = result.Type == 28;
            result.IsFU_B                 = result.Type == 29;
            result.IsReserved            |= result.Type == 30;
            result.IsReserved            |= result.Type == 31;

            result.IsCodedSliceNIDR       = result.Type == 1;
            result.IsCodedSlicePartitionA = result.Type == 2;
            result.IsCodedSlicePartitionB = result.Type == 3;
            result.IsCodedSlicePartitionC = result.Type == 4;
            result.IsCodedSliceIDR        = result.Type == 5;
            result.IsCodedSliceIDR        = result.Type == 6;
            result.IsSPS                  = result.Type == 7;
            result.IsPPS                  = result.Type == 8;
            result.IsAccessDelimiter      = result.Type == 9;

            /*
                +----------------------------------+
                | Raw Byte Sequence Payload        |
                +----------------------------------+
             */

            result.Payload = new byte[ buffer.Length - ++ offset ];

            Buffer.BlockCopy( buffer , offset , result.Payload , 0 , result.Payload.Length );

            return true;
        }

        public static ulong Sum( byte[] buffer , int count )
        {
            ulong sum = 0;

            count = count > buffer.Length ? buffer.Length : count ;

            while ( -- count >= 0 )
                sum += buffer[ count ];

            return sum;
        }
    }

    public sealed class H264Parser : IDisposable
	{
        private readonly object _lock = new object();

        // Time complexity O(N) as first view
        // Time complexity O(N,M)  real => see TryParse

        public bool TryParse( RTPFrame frame , out H264NalUnitCollection result )
        {
            result = null;

            if ( frame == null || ! frame.TryValidate())
                return false;

            H264NalUnitCollection nalunits = new H264NalUnitCollection();

            foreach ( var packet in frame.Packets )
            {
                if ( H264NalUnit.TryParse( packet.Data , out H264NalUnit nalUnit ) )
                {
                    nalunits.Enqueue( nalUnit );
                }
            }

            result = nalunits.IsEmpty ? null : nalunits;

            return result != null;
        }

        public void Dispose()
        {
        }
	}
}