using System;
using System.Linq;

#pragma warning disable CS0618

namespace RabbitOM.Sample.Client.H264.Codecs.FFMpeg
{
    using FFmpeg.AutoGen;

    internal unsafe sealed class InternalH264FFMpegDecoder
    {
        static InternalH264FFMpegDecoder()
        {
            ffmpeg.RootPath = AppDomain.CurrentDomain.BaseDirectory;
        }







        private AVCodec* _codec = null;

        private AVCodecContext* _context = null;

        private AVFrame* _frame = null;

        private AVFrame* _swFrame = null;

        private AVPacket* _packet = null;

        private AVDictionary* _options = null;

        private SwsContext* _sws_context = null;

        private byte[] _extraParameters;

        private byte[] _imageBuffer;

        private int _actualWidth;

        private int _actualHeigth;

        private readonly int[] _stride = new int[1];









        public byte[] ExtraParameters
        {
            get => _extraParameters;
        }

        public byte[] ImageBuffer
        {
            get => _imageBuffer;
        }

        public int ActualWidth
        {
            get => _actualWidth;
        }

        public int ActualHeigth
        {
            get => _actualHeigth;
        }







        public bool IsOpened
        {
            get
            {
                return _codec != null && _context != null && _frame != null && _packet != null;
            }
        }

        public void Open()
        {
            if ( _codec != null )
            {
                throw new InvalidOperationException( "the codec is already opened" );
            }

            try
            {
                _codec = ffmpeg.avcodec_find_decoder( AVCodecID.AV_CODEC_ID_H264 );

                if ( _codec == null )
                {
                    throw new InvalidOperationException( "no codec found" );
                }

                _context = ffmpeg.avcodec_alloc_context3( _codec );

                if ( _context == null )
                {
                    throw new InvalidOperationException( "can not allocate a codec context" );
                }

	            _context->thread_count = 1;
                _context->flags2 |= ffmpeg.AV_CODEC_FLAG2_FAST;

                fixed( AVDictionary** opts = &_options )
                {
                    ffmpeg.av_dict_set( opts , "rtsp_transport", "none", 0);
                    ffmpeg.av_dict_set( opts , "allowed_media_types", "video", 0);

                    _context->pix_fmt = AVPixelFormat.AV_PIX_FMT_YUV420P;

	                if (ffmpeg.avcodec_open2( _context , _codec , opts ) < 0)
	                {
		                return;
	                }

                    if ( _frame == null )
                    {
                        _frame = ffmpeg.av_frame_alloc();
                    }

                    if ( _swFrame == null )
                    {
                        _swFrame = ffmpeg.av_frame_alloc();
                    }

                    if ( _packet == null )
                    {
                        _packet = ffmpeg.av_packet_alloc();
                    }
                }
            }
            catch( Exception )
            {
                Close();
                throw;
            }
        }

        public void Close()
        {
            if ( _swFrame != null )
            {
                fixed ( AVFrame** ppFrame = &_swFrame )
                {
                    ffmpeg.av_frame_free( ppFrame );
                }

                _swFrame = null;
            }

            if ( _frame != null )
            {
                fixed ( AVFrame** ppFrame = &_frame )
                {
                    ffmpeg.av_frame_free( ppFrame );
                }

                _frame = null;
            }

            if ( _sws_context != null )
            {
                ffmpeg.sws_freeContext( _sws_context );
	            _sws_context = null;
            }

            if ( _packet != null )
            {
                ffmpeg.av_packet_unref( _packet );
                _packet = null;
            }

            if ( _options != null )
            {
                fixed ( AVDictionary** ppOptions = &_options )
                {
                    ffmpeg.av_dict_free( ppOptions );
                }

                _options = null;
            }

            if ( _context != null )
            {
                fixed ( AVCodecContext** ppContext = &_context )
                {
                    if ( _context->extradata != null )
		            {
			            ffmpeg.av_free( _context->extradata );

			            _context->extradata = null;
			            _context->extradata_size = 0;
		            }

                    ffmpeg.avcodec_close( _context );
                    ffmpeg.avcodec_free_context( ppContext );
                }

                _context = null;
            }

            _codec = null;
            _extraParameters = null;
        }

        public bool CanConfigure( byte[] extraParameters )
        {
            if ( extraParameters == null || extraParameters.Length == 0 )
            {
                return false;
            }

            return _extraParameters == null || ! _extraParameters.SequenceEqual( extraParameters );
        }

        public unsafe bool Configure( byte[] extraParameters )
        {
            _extraParameters = new byte[ extraParameters.Length ];

            Buffer.BlockCopy( extraParameters , 0 , _extraParameters , 0 , _extraParameters.Length );

            fixed ( byte* pExtraData = _extraParameters )
            {
                if ( _context->extradata != null || _context->extradata_size != _extraParameters.Length )
	            {
                    if ( _context->extradata != null )
                    {
		                ffmpeg.av_free( _context->extradata );
                    }

		            _context->extradata      = null;
		            _context->extradata_size = 0;
	            }

                var size = ffmpeg.AV_INPUT_BUFFER_PADDING_SIZE + (ulong)_extraParameters.Length;

                if ( _context->extradata == null )
	            {
		            _context->extradata = (byte*) ffmpeg.av_malloc( size );
	            }

                if ( _context->extradata == null )
	            {
		            return false;
	            }

                _context->extradata_size = _extraParameters.Length;

                var pBuffer = _context->extradata;

                Buffer.MemoryCopy( pExtraData , pBuffer , _extraParameters.Length , _extraParameters.Length );
                byte* ptr = _context->extradata + _extraParameters.Length;

                ulong zero = 0;

                ((ulong*)ptr)[0] = zero;
                ((ulong*)ptr)[1] = zero;
                ((ulong*)ptr)[2] = zero;
                ((ulong*)ptr)[3] = zero;

                return true;
            }
        }

        public unsafe bool EndConfigure()
        {
            ffmpeg.avcodec_close( _context );

            fixed ( AVCodecContext** ppContext = &_context )
            {
                ffmpeg.avcodec_free_context( ppContext );
            }

            _context = ffmpeg.avcodec_alloc_context3( _codec );

            if ( _context == null )
            {
                return false;
            }

            _context->thread_count = 1;
            _context->flags  |= ffmpeg.AV_CODEC_FLAG_TRUNCATED;
            _context->flags2 |= ffmpeg.AV_CODEC_FLAG2_FAST;

            fixed ( AVDictionary** opts = &_options )
            {
                ffmpeg.av_dict_set( opts , "rtsp_transport", "none", 0);
                ffmpeg.av_dict_set( opts , "allowed_media_types", "video", 0);

                return ffmpeg.avcodec_open2( _context , _codec , opts ) >= 0;
            }
        }

        public unsafe bool Decode( byte[] buffer )
        {
            if ( buffer == null || buffer.Length == 0 || _context == null || _frame == null )
            {
                return false;
            }

            fixed ( byte* rawBuffer = &buffer[0] )
            {
                _packet->data = rawBuffer;
	            _packet->size = buffer.Length;

                var got_frame = 0;
	            var length = ffmpeg.avcodec_decode_video2( _context , _frame , &got_frame, _packet );

                if ( length != buffer.Length )
                {
                    return false;
                }

	            return got_frame != 0;
            }
        }

        public bool ScaleImage()
        {
            if ( _context == null )
            {
                return false;
            }

            if ( _actualWidth != _context->width || _actualHeigth != _context->height )
            {
                if ( _sws_context != null )
                {
                    ffmpeg.sws_freeContext( _sws_context );
	                _sws_context = null;
                }

                _actualWidth = _context->width;
                _actualHeigth = _context->height;
                _imageBuffer = null;
            }

            if ( _sws_context == null )
            {
                _sws_context = ffmpeg.sws_getContext( _actualWidth , _actualHeigth ,  AVPixelFormat.AV_PIX_FMT_YUV420P , _actualWidth , _actualHeigth , AVPixelFormat.AV_PIX_FMT_RGB24 , ffmpeg.SWS_BILINEAR , null , null , null );
            }

            if ( _sws_context == null )
            {
                return false;
            }

            if ( _imageBuffer == null )
            {
                _imageBuffer = new byte[ _actualHeigth * _actualWidth * 4 ];
            }

            fixed ( byte* buffer = _imageBuffer )
            {
                var dstData = new byte_ptrArray8();

                dstData[0] = buffer;
                _stride[0] = _actualWidth * 4;

                return ffmpeg.sws_scale( _sws_context , _frame->data , _frame->linesize , 0 , _actualHeigth , dstData , _stride ) >= 0;
            }
        }
    }
}

#pragma warning restore CS0618