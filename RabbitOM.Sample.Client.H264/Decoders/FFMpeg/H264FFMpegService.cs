using System;
using System.Linq;

#pragma warning disable CS0618

namespace RabbitOM.Sample.Client.H264.Codecs.FFMpeg
{
    using FFmpeg.AutoGen;

    internal unsafe sealed class H264FFMpegService
    {
        static H264FFMpegService()
        {
            ffmpeg.RootPath = AppDomain.CurrentDomain.BaseDirectory;
        }







        private AVCodec* _codec = null;

        private AVCodecContext* _context = null;

        private AVFrame* _frame = null;

        private AVFrame* _swFrame = null;

        private AVPacket* _packet = null;

        private AVDictionary* _options = null;

        private byte[] _extraParameters;








        public IntPtr AVCodecPointer
        {
            get => (IntPtr) _codec;
        }

        public IntPtr AVContextPointer
        {
            get => (IntPtr) _context;
        }

        public IntPtr AVFramePointer
        {
            get => (IntPtr) _frame;
        }

        public IntPtr AVSwFramePonter
        {
            get => (IntPtr) _swFrame;
        }

        public IntPtr AVPacketPointer
        {
            get => (IntPtr) _packet;
        }

        public IntPtr AVOptionsPointer
        {
            get => (IntPtr) _options;
        }

        public byte[] ExtraParameters
        {
            get => _extraParameters;
        }







        public bool IsFrameAllocated()
        {
            return _frame != null;
        }

        public void AllocateFrame()
        {
            if ( _frame == null )
            {
                _frame = ffmpeg.av_frame_alloc();
            }
        }

        public void FreeFrame()
        {
            if ( _frame != null )
            {
                fixed ( AVFrame** ppFrame = &_frame )
                {
                    ffmpeg.av_frame_free( ppFrame );
                }

                _frame = null;
            }
        }





        public bool IsSwFrameAllocated()
        {
            return _swFrame != null;
        }

        public void AllocateSwFrame()
        {
            if ( _swFrame == null )
            {
                _swFrame = ffmpeg.av_frame_alloc();
            }
        }

        public void FreeSwFrame()
        {
            if ( _swFrame != null )
            {
                fixed ( AVFrame** ppFrame = &_swFrame )
                {
                    ffmpeg.av_frame_free( ppFrame );
                }

                _swFrame = null;
            }
        }




        public bool IsPacketAllocated()
        {
            return _packet != null;
        }

        public void AllocatePacket()
        {
            if ( _packet == null )
            {
                _packet = ffmpeg.av_packet_alloc();
            }
        }

        public void FreePacket()
        {
            if ( _packet != null )
            {
                ffmpeg.av_packet_unref( _packet );
                _packet = null;
            }
        }






        public bool IsDecoderOpened()
        {
            return _codec != null && _context != null;
        }

        public void OpenDecoder()
        {
            if ( _codec != null )
            {
                throw new InvalidOperationException( "the codec is already opened" );
            }

            _codec = ffmpeg.avcodec_find_decoder( AVCodecID.AV_CODEC_ID_H264 );

            if ( _codec == null )
            {
                throw new InvalidOperationException( "codec found" );
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
		            throw new InvalidOperationException( "can not open the codec" );
	            }
            }
        }

        public void CloseDecoder()
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









        public bool CanConfigure( ref H264Options options )
        {
            if ( _extraParameters == null || options.ExtraParameters == null )
            {
                return false;
            }

            // TODO: use unsafe code and iterate using pointer to compare buffers for a fast iteration
            return _extraParameters.SequenceEqual( options.ExtraParameters );
        }

        public unsafe bool Configure( ref H264Options options )
        {
            _extraParameters = new byte[ options.ExtraParameters.Length ];

            Buffer.BlockCopy( options.ExtraParameters , 0 , _extraParameters , 0 , _extraParameters.Length );

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

                    if (ffmpeg.avcodec_open2( _context , _codec , opts ) < 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }






        public bool CanDecode( byte[] buffer , ref H264Options options )
        {
            if ( buffer == null || buffer.Length <= 0 )
            {
                return false;
            }

            if ( _context == null || _frame == null )
            {
                return false;
            }

            return true;
        }

        public unsafe bool Decode( byte[] buffer , ref H264Options options )
        {
            fixed ( byte* rawBuffer = &buffer[0] )
            {
                _packet->data = rawBuffer;
	            _packet->size = buffer.Length;

                var got_frame = 0;
	            var length = ffmpeg.avcodec_decode_video2( _context , _frame , &got_frame, _packet );

	            return length == buffer.Length && got_frame != 0;
            }
        }
    }
}

#pragma warning restore CS0618