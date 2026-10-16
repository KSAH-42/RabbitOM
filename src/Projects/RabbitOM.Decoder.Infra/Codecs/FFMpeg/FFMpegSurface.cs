using FFmpeg.AutoGen;
using System;

namespace RabbitOM.Decoder.Codecs
{
    public sealed unsafe class FFMpegSurface : Surface
    {
        private AVFrame* _frame;

        public FFMpegSurface( int width , int height , AVFrame* frame )
            : base( width , height )
        {
            if ( frame == null )
            {
                throw new ArgumentNullException( nameof( frame ) );
            }

            _frame = frame;
        }

        ~FFMpegSurface()
        {
            Dispose( false );
        }

        public override IntPtr Frame
        {
            get => _frame != null ? (IntPtr) _frame : IntPtr.Zero;
        }

        protected override void Dispose( bool disposing )
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
    }
}