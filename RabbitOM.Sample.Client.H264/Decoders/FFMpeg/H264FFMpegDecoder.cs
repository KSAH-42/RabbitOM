using System;

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








        public override bool IsOpened
        {
            get
            {
                return _codec != null;
            }
        }

        public override void Open()
        {
            if ( _codec != null )
            {
                throw new InvalidOperationException();
            }

            _codec = ffmpeg.avcodec_find_decoder( AVCodecID.AV_CODEC_ID_H264 );

            if ( _codec == null )
            {
                throw new InvalidOperationException();
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
                ffmpeg.av_free( _swframe );
                _swframe = null;
            }

            if ( _frame != null )
            {
                ffmpeg.av_free( _frame );
                _frame = null;
            }

            fixed ( AVDictionary** opts = &_options )
            {
                if ( opts != null )
	            {
		            ffmpeg.av_dict_free(opts);
	            }
            }

            _options = null;

            if ( _context != null )
            {
                if ( _context->extradata != null )
		        {
			        ffmpeg.av_free( _context->extradata );

			        _context->extradata = null;
			        _context->extradata_size = 0;
		        }

                ffmpeg.avcodec_close( _context );
                ffmpeg.av_free( _context );

                _codec = null;
                _context = null;
            }
        }

        public override void Decode( byte[] buffer , H264Surface surface ) { }

        protected unsafe override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                Close();
            }

            base.Dispose( disposing );
        }
    }
}