using System;
using System.Linq;

#pragma warning disable CS0618

namespace RabbitOM.Sample.Player.Codecs.FFMpeg
{
    using FFmpeg.AutoGen;

    public unsafe sealed class FFMpegDecoder : Decoder
    {
        private AVCodec* _decoder = null;
        private AVCodecContext* _context = null;
        private AVFrame* _frame = null;
        private AVFrame* _swframe = null;
        private AVPacket* _rawPacket = null;
        private AVDictionary* _options = null;
        private byte[] _extraParameters;

        static FFMpegDecoder()
        {
            ffmpeg.RootPath = AppDomain.CurrentDomain.BaseDirectory;
        }

        public override bool IsOpened
        {
            get
            {
                return _decoder != null && _context != null && _frame != null && _rawPacket != null;
            }
        }

        public override void Open( CodecType type )
        {
            if ( _decoder != null )
            {
                throw new InvalidOperationException( "the codec is already opened" );
            }

            try
            {
                _decoder = ffmpeg.avcodec_find_decoder( FFMpegCodecTypeConverter.Convert( type ) );

                if ( _decoder == null )
                {
                    throw new InvalidOperationException( "no decoder found" );
                }

                _context = ffmpeg.avcodec_alloc_context3( _decoder );

                if ( _context == null )
                {
                    throw new InvalidOperationException( "can not allocate a codec context" );
                }

	            _context->thread_count = 1;
                _context->flags  |= ffmpeg.AV_CODEC_FLAG_TRUNCATED;
                _context->flags2 |= ffmpeg.AV_CODEC_FLAG2_FAST;
                _context->pix_fmt = AVPixelFormat.AV_PIX_FMT_RGB24;

                fixed( AVDictionary** opts = &_options )
                {
                    ffmpeg.av_dict_set( opts , "rtsp_transport", "none", 0);
                    ffmpeg.av_dict_set( opts , "allowed_media_types", "video", 0);

	                if ( ffmpeg.avcodec_open2( _context , _decoder , opts ) != 0 )
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

            if ( _rawPacket != null )
            {
                _rawPacket->data = null;
                _rawPacket->size = 0;

                fixed( AVPacket** ppPacket = &_rawPacket )
                {
                    ffmpeg.av_packet_free( ppPacket );
                }

                _rawPacket = null;
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

            _decoder = null;
            _extraParameters = null;
        }

        public override bool CanConfigure( byte[] extraParameters )
        {
            return extraParameters != null && ( _extraParameters == null || ! _extraParameters.SequenceEqual( extraParameters ) );
        }

        public override bool Configure( byte[] extraParameters )
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

                Buffer.MemoryCopy( pExtraData , _context->extradata , _extraParameters.Length , _extraParameters.Length );

                byte* ptr = _context->extradata + _extraParameters.Length;

                ((ulong*)ptr)[0] = 0;
                ((ulong*)ptr)[1] = 0;
                ((ulong*)ptr)[2] = 0;
                ((ulong*)ptr)[3] = 0;

                ffmpeg.avcodec_close( _context );

                fixed ( AVCodecContext** ppContext = &_context )
                {
                    ffmpeg.avcodec_free_context( ppContext );
                }

                _context = ffmpeg.avcodec_alloc_context3( _decoder );

                if ( _context == null )
                {
                    return false;
                }

                _context->thread_count = 1;
                _context->flags  |= ffmpeg.AV_CODEC_FLAG_TRUNCATED;
                _context->flags2 |= ffmpeg.AV_CODEC_FLAG2_FAST;
                _context->pix_fmt = AVPixelFormat.AV_PIX_FMT_RGB24;

                fixed ( AVDictionary** opts = &_options )
                {
                    ffmpeg.av_dict_set( opts , "rtsp_transport", "none", 0);
                    ffmpeg.av_dict_set( opts , "allowed_media_types", "video", 0);

                    return ffmpeg.avcodec_open2( _context , _decoder , opts ) == 0;
                }
            }
        }

        public override void Decode( byte[] buffer )
        {
            if ( buffer == null || buffer.Length == 0 || _context == null )
            {
                return;
            }

            fixed ( byte* rawBuffer = &buffer[0] )
            {
                var got_frame = 0;

                // normally, in this case, it's highly recommended to set to default _rawPacket->data as null after pin buffer adress and calling ffmpeg.decode func using a try finally bloc, the reason come from that the compactor on the GC can change the address of the raw buffer
                // and ffmpeg lib can manipulate a wrong buffer, dangled pointer
                // so the right approach is to force a clear on these members
                // so here i don't do that because it for reducing overhead and this members is not used elswhere
                // it's used only in this place
	            _rawPacket->data = rawBuffer;
	            _rawPacket->size = buffer.Length;

                var length = ffmpeg.avcodec_decode_video2( _context , _frame , &got_frame, _rawPacket );

                if ( length < 0 || got_frame == 0 )
                {
                    return;
                }
            }

            var clonedFrame = ffmpeg.av_frame_clone( _frame );

            if ( clonedFrame == null )
            {
                return;
            }

            OnDecoded( new DecodedEventArgs( new FFMpegSurface( _context->width , _context->height , clonedFrame ) ) );
        }
    }
}

#pragma warning restore CS0618