using System;

#pragma warning disable CS1591 

namespace RabbitOM.Net.Rtp
{
    [Obsolete]
    public sealed class RtpStream
	{
        private readonly object _lock = new object();

        private readonly IRtpPayloadParser _mediaPayloadParser;
        
        private readonly int _samplesFrequency;
        private ulong _samplesSum;
        private ushort _previousSeqNumber;
        private uint _previousTimestamp;
        private bool _isFirstPacket = true;
        private uint _syncSourceId = 0;
        private uint _packetsReceivedSinceLastReset = 0;
        private uint _packetsLostSinceLastReset = 0;
        private uint _cumulativePacketLost = 0;
        private uint _frameRate = 0;
        private uint _numberOfDecodedFrame = 0;
        private int _numberOfBytesPerSecond = 0;
        private int _actualNumberOfBytes;
        private DateTime? _lastDecodedTimeStamp = null;
        private TimeSpan? _previousTimeOffset = null;




        public RtpStream( IRtpPayloadParser mediaPayloadParser , int samplesFrequency )
        {
            _mediaPayloadParser = mediaPayloadParser ?? throw new ArgumentNullException( nameof( mediaPayloadParser ) );
            _samplesFrequency = samplesFrequency;
        }





        public ulong SamplesSum
        {
            get => GetField( ref _samplesSum );
            private set => SetField( ref _samplesSum , value );
        }

        public ushort PreviousSeqNumber
        {
            get => GetField( ref _previousSeqNumber );
            private set => SetField( ref _previousSeqNumber , value );
        }

        public uint PreviousTimestamp
        {
            get => GetField( ref _previousTimestamp );
            private set => SetField( ref _previousTimestamp , value );
        }

        public bool IsFirstPacket
        {
            get => GetField( ref _isFirstPacket );
            private set => SetField( ref _isFirstPacket , value );
        }

        public uint SyncSourceId
        {
            get => GetField( ref _syncSourceId );
            private set => SetField( ref _syncSourceId , value );
        }

        public uint PacketsReceivedSinceLastReset
        {
            get => GetField( ref _packetsReceivedSinceLastReset );
            private set => SetField( ref _packetsReceivedSinceLastReset , value );
        }

        public uint PacketsLostSinceLastReset
        {
            get => GetField( ref _packetsLostSinceLastReset );
            private set => SetField( ref _packetsLostSinceLastReset , value );
        }

        public uint CumulativePacketLost
        {
            get => GetField( ref _cumulativePacketLost );
            private set => SetField( ref _cumulativePacketLost , value );
        }

        public uint FrameRate
        {
            get => GetField( ref _frameRate );
            private set => SetField( ref _frameRate , value );
        }

        public uint NumberOfDecodedFrame
        {
            get => GetField( ref _numberOfDecodedFrame );
            private set => SetField( ref _numberOfDecodedFrame , value );
        }

        public DateTime? LastDecodedTimeStamp
        {
            get => GetField( ref _lastDecodedTimeStamp );
            private set => SetField( ref _lastDecodedTimeStamp , value );
        }

        public TimeSpan? PreviousTimeOffset
        {
            get => GetField( ref _previousTimeOffset );
            private set => SetField( ref _previousTimeOffset , value );
        }

        public int NumberOfBytesPerSecond
        {
            get => GetField( ref _numberOfBytesPerSecond );
            private set => SetField( ref _numberOfBytesPerSecond , value );
        }

        private int ActualNumberOfBytes
        {
            get => GetField( ref _actualNumberOfBytes );
            set => SetField( ref _actualNumberOfBytes , value );
        }





        public void Process( byte[] payloadSegment )
        {
            if ( ! RtpPacket.TryParse( payloadSegment , out RtpPacket packet ) )
            {
                return;
            }

            TimeSpan? timeOffset = null;

            lock ( _lock )
            {
                if ( _previousSeqNumber >= packet.SequenceNumber )
                {
                    _previousTimeOffset = null;
                }

                _actualNumberOfBytes += packet.Data.Count;

                _syncSourceId = packet.SSRC;

                if ( !_isFirstPacket )
                {
                    int delta = (ushort) ( packet.SequenceNumber - _previousSeqNumber );

                    if ( delta != 1 )
                    {
                        int lostCount = delta - 1;

                        if ( lostCount == -1 )
                        {
                            lostCount = ushort.MaxValue;
                        }

                        _cumulativePacketLost += (uint) lostCount;

                        if ( _cumulativePacketLost > 0x7FFFFF )
                        {
                            _cumulativePacketLost = 0x7FFFFF;
                        }

                        if ( lostCount > 0 )
                        {
                            _packetsLostSinceLastReset += (uint) lostCount;
                        }

                        _mediaPayloadParser.Reset();
                    }

                    _samplesSum += packet.Timestamp - _previousTimestamp;
                }

                ++_packetsReceivedSinceLastReset;

                _isFirstPacket = false;
                _previousSeqNumber = packet.SequenceNumber;
                _previousTimestamp = packet.Timestamp;

                timeOffset = _samplesFrequency != 0 ? new TimeSpan( (long) ( _samplesSum * 1000 / (uint) _samplesFrequency * TimeSpan.TicksPerMillisecond ) ) : TimeSpan.MinValue;

                _previousTimeOffset = timeOffset;
            }

            if ( packet.Data.Count == 0 )
            {
                return;
            }

            _mediaPayloadParser.Parse( timeOffset.Value , packet );
        }

        public void IncreaseDecodedFrame()
        {
            lock ( _lock )
            {
                var currentTime = DateTime.Now;

                if ( !_lastDecodedTimeStamp.HasValue )
                {
                    _lastDecodedTimeStamp = currentTime;
                }

                var deltaTime = currentTime - _lastDecodedTimeStamp.Value;

                if ( deltaTime.TotalSeconds >= 1 )
                {
                    _frameRate = _numberOfDecodedFrame;

                    _numberOfDecodedFrame = 0;
                    _lastDecodedTimeStamp = currentTime;
                    _numberOfBytesPerSecond = _actualNumberOfBytes;
                    _actualNumberOfBytes = 0;
                }
                else
                {
                    _numberOfDecodedFrame++;
                }
            }
        }

        public void Reset()
        {
            PacketsReceivedSinceLastReset = 0;
            FrameRate = 0;
            NumberOfDecodedFrame = 0;
            LastDecodedTimeStamp = null;
            PreviousTimeOffset = null;
            _mediaPayloadParser.Reset();
        }

        public void Clear()
        {
            SamplesSum = 0;
            PreviousSeqNumber = 0;
            PreviousTimestamp = 0;
            IsFirstPacket = true;

            SyncSourceId = 0;
            PacketsReceivedSinceLastReset = 0;
            PacketsLostSinceLastReset = 0;
            CumulativePacketLost = 0;
            FrameRate = 0;
            NumberOfDecodedFrame = 0;
            LastDecodedTimeStamp = null;
            PreviousTimeOffset = null;
            NumberOfBytesPerSecond = 0;
            ActualNumberOfBytes = 0;
        }

        private TValue GetField<TValue>( ref TValue memberValue )
        {
            lock ( _lock )
            {
                return memberValue;
            }
        }

        private void SetField<TValue>( ref TValue memberValue , TValue value )
        {
            lock ( _lock )
            {
                memberValue = value;
            }
        }
    }
}

#pragma warning restore CS1591 

