// This decoder doesn't support hardware acceleration, you need to implement your own decoder using ffmpeg
using System;
using System.Linq;

#pragma warning disable CS0618

namespace RabbitOM.Sample.Client.H264.Codecs.FFMpeg
{
    using FFmpeg.AutoGen;

    public unsafe sealed class H264FFMpegDecoder : H264Decoder
    {
        static H264FFMpegDecoder()
        {
            ffmpeg.RootPath = AppDomain.CurrentDomain.BaseDirectory;
        }







        private AVCodec* _codec = null;

        private AVCodecContext* _context = null;

        private AVFrame* _frame = null;

        private AVFrame* _swframe = null;

        private AVPacket* _rawPacket = null;

        private AVDictionary* _options = null;

        private byte[] _extraParameters;








        public override bool IsOpened
        {
            get
            {
                return _codec != null && _context != null && _frame != null && _rawPacket != null;
            }
        }

        public override void Open()
        {
            if ( _codec != null )
            {
                throw new InvalidOperationException();
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

                    if ( _swframe == null )
                    {
                        _swframe = ffmpeg.av_frame_alloc();
                    }

                    if ( _rawPacket == null )
                    {
                        _rawPacket = ffmpeg.av_packet_alloc();
                    }

                    if ( _rawPacket != null )
                    {
                        _rawPacket = ffmpeg.av_packet_alloc();
                    }
                }
            }
            catch( Exception )
            {
                Close();
                throw;
            }
        }

        public override void Close()
        {
            if ( _rawPacket != null )
            {
                ffmpeg.av_packet_unref( _rawPacket );
                _rawPacket = null;
            }

            if ( _swframe != null )
            {
                fixed ( AVFrame** ppFrame = &_swframe )
                {
                    ffmpeg.av_frame_free( ppFrame );
                }

                _swframe = null;
            }

            if ( _frame != null )
            {
                fixed ( AVFrame** ppFrame = &_frame )
                {
                    ffmpeg.av_frame_free( ppFrame );
                }

                _frame = null;
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

        public unsafe override void Decode( byte[] buffer , H264Options options )
        {
            if ( buffer == null || buffer.Length <= 0 )
            {
                return;
            }

            fixed ( byte* rawBuffer = &buffer[0] )
            {
                if ( _extraParameters == null || ! _extraParameters.SequenceEqual( options.ExtraParameters ) )
                {
                    if ( ! OnReConfiguringCodec( ref options ) )
                    {
                        return;
                    }
                }

                var got_frame = 0;

	            _rawPacket->data = rawBuffer;
	            _rawPacket->size = buffer.Length;

                var length = ffmpeg.avcodec_decode_video2( _context , _frame , &got_frame, _rawPacket );

	            if ( length != buffer.Length )
	            {
		            return;
	            }

                if ( got_frame == 0 )
                {
                    return;
                }
            }

            OnDecoded( new H264DecodedEventArgs( new H264Surface( options , _context->width , _context->height , (IntPtr) _frame ) ) );
        }

        protected unsafe override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                Close();
            }

            base.Dispose( disposing );
        }

        private unsafe bool OnReConfiguringCodec( ref H264Options options )
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
    }
}

#pragma warning restore CS0618