using FFmpeg.AutoGen;
using System;

namespace RabbitOM.Decoder.FFMpeg.Streaming.Codecs
{
    public sealed unsafe class Surface : IDisposable
    {
        private int _width;
        private int _height;
        private AVFrame* _frame;





        public Surface( int width , int height , AVFrame* frame )
        {
            _width = width;
            _height = height;
            _frame = frame != null ? frame : throw new ArgumentNullException( nameof( frame ) );
        }

        ~Surface()
        {
            Dispose();
        }





        public int Width { get => _width; }

        public int Height { get => _height; }

        public AVFrame* Frame { get => _frame; }





        public void Dispose()
        {
            if ( _frame != null )
            {
                fixed ( AVFrame** ppFrame = &_frame )
                {
                    ffmpeg.av_frame_free( ppFrame );
                }

                _frame = null;
            }

            _height = 0;
            _width = 0;

            GC.SuppressFinalize( this );
        }
    }
}