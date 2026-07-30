using System;

namespace RabbitOM.Sample.Client.Player.Codecs.FFMpeg
{
    using FFmpeg.AutoGen;

    public sealed unsafe class FFMpegSurface : Surface
    {
        private AVFrame* _frame;

        public FFMpegSurface( int frameWidth , int frameHeight , AVFrame* frame )
        {
            if ( frame == null )
            {
                throw new ArgumentNullException( nameof( frame ) );
            }

            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
            _frame = frame;
        }

        public override int FrameWidth { get; }

        public override int FrameHeight { get; }

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