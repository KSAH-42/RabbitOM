using System;
using System.IO;

namespace RabbitOM.Net.Rtp.Mjpeg
{
    /// <summary>
    /// Represent a MJPEG payload parser
    /// </summary>
    public sealed class MjpegRtpPayloadParser : MediaPayloadParser
    {
        private readonly MemoryStream _frameStream;




        private bool _hasExternalQuantizationTable;
        private int _currentDri;
        private int _currentQ;
        private int _currentType;
        private int _currentFrameWidth;
        private int _currentFrameHeight;
        private int _quantizationTablesLength;
        private byte[] _jpegHeaderBytes;
        private byte[] _quantizationTables;
        private DateTime _baseTime = DateTime.MinValue;
        private ArraySegment<byte> _jpegHeaderBytesSegment;




        /// <summary>
        /// Initialize a new instance of the parser
        /// </summary>
        public MjpegRtpPayloadParser()
        {
            _frameStream = new MemoryStream();
            
            _jpegHeaderBytes = Array.Empty<byte>();
            _quantizationTables = Array.Empty<byte>();
        }






        /// <inheritdoc/>
        public override void Parse( TimeSpan timeOffset , RtpPacket packet )
        {
            if ( packet == null )
            {
                return;
            }

            var byteSegment = packet.Data;

            if ( byteSegment.Array == null )
            {
                return;
            }

            if ( byteSegment.Count <= 0 || byteSegment.Count < MjpegHelper.JpegHeaderSize )
            {
                return;
            }

            int offset = byteSegment.Offset + 1;

            int fragmentOffset = RtpDataConverter.ConvertToInt24(byteSegment.Array, offset);
            offset += 3;

            int type = byteSegment.Array[offset++];
            int q = byteSegment.Array[offset++];
            int width = byteSegment.Array[offset++] * 8;
            int height = byteSegment.Array[offset++] * 8;
            int dri = 0;

            if (type > 63)
            {
                dri = RtpDataConverter.ConvertToInt16(byteSegment.Array, offset);
                offset += 4;
            }

            if (fragmentOffset == 0)
            {
                if (_frameStream.Position != 0)
                {
                    GenerateFrame( timeOffset );
                }

                bool quantizationTablesChanged = false;

                if ( q > 127 )
                {
                    int mbz = byteSegment.Array[offset];

                    if (mbz == 0)
                    {
                        _hasExternalQuantizationTable = true;

                        int quantizationTablesLength = RtpDataConverter.ConvertToInt16(byteSegment.Array, offset + 2);
                        offset += 4;

                        if (!ArrayHelper.Equals(byteSegment.Array, offset, quantizationTablesLength,_quantizationTables, 0, _quantizationTablesLength))
                        {
                            if (_quantizationTables.Length < quantizationTablesLength)
                            {
                                _quantizationTables = new byte[ quantizationTablesLength ];
                            }

                            Buffer.BlockCopy(byteSegment.Array, offset, _quantizationTables, 0,quantizationTablesLength);

                            _quantizationTablesLength = quantizationTablesLength;
                            quantizationTablesChanged = true;
                        }

                        offset += quantizationTablesLength;
                    }
                }

                if ( quantizationTablesChanged || _currentType != type || _currentQ != q || _currentFrameWidth != width ||  _currentFrameHeight != height || _currentDri != dri )
                {
                    _currentType = type;
                    _currentQ = q;
                    _currentFrameWidth = width;
                    _currentFrameHeight = height;
                    _currentDri = dri;

                    ReInitializeJpegHeader();
                }

                _frameStream.Write(_jpegHeaderBytesSegment.Array, _jpegHeaderBytesSegment.Offset, _jpegHeaderBytesSegment.Count);
            }

            if (fragmentOffset != 0 && _frameStream.Position == 0)
            {
                return;
            }

            if (_frameStream.Position > MjpegHelper.JpegMaxSize )
            {
                return;
            }

            int dataSize = byteSegment.Offset + byteSegment.Count - offset;

            if (dataSize < 0)
            {
                return;
            }

            _frameStream.Write(byteSegment.Array, offset, dataSize);
        }

        /// <inheritdoc/>
        public override void Reset()
        {
            _baseTime = default;
            _frameStream.Position = 0;
            _frameStream.SetLength( 0 );
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            _frameStream.Dispose();
        }

        private void ReInitializeJpegHeader()
        {
            if (!_hasExternalQuantizationTable)
            {
                MjpegHelper.GenerateQuantizationTables( ref _quantizationTables , ref _quantizationTablesLength , _currentQ );
            }

            var jpegHeaderSize = MjpegHelper.GetJpegHeaderSize( _quantizationTablesLength , _currentDri);

            _jpegHeaderBytes = new byte[jpegHeaderSize];
            _jpegHeaderBytesSegment = new ArraySegment<byte>(_jpegHeaderBytes);

            MjpegHelper.FillJpegHeader( _quantizationTables , _quantizationTablesLength , _jpegHeaderBytes , _currentType, _currentFrameWidth, _currentFrameHeight, _currentDri);
        }

        


        

        private void GenerateFrame(TimeSpan timeOffset)
        {
            if ( ! ArrayHelper.EndsWith( _frameStream.GetBuffer() , 0 , (int) _frameStream.Position , MjpegHelper.EndMarkerBytes ) )
            {
                _frameStream.Write( MjpegHelper.EndMarkerBytes , 0 , MjpegHelper.EndMarkerBytes.Length );
            }

            _frameStream.Flush();

            TryResolveTimestamp( ref _baseTime , timeOffset , out DateTime timestamp );

            var framesBytes = new byte[ _frameStream.Length ];

            _frameStream.Read( framesBytes , 0 , framesBytes.Length );
            _frameStream.SetLength( 0 );
            _frameStream.Position = 0;

            OnFrameReceived( new RtpFrameReceivedEventArgs( new JpegRtpFrame( timestamp , new ArraySegment<byte>( framesBytes ) )) );
        }
    }
}