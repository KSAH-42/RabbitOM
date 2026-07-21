using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RabbitOM.Sample.Client.H264.Codecs.FFMpeg
{
    using FFmpeg.AutoGen;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public sealed unsafe class H264FFMpegRenderer : H264Renderer
    {
        private SwsContext* _sws_context = null;
	    private WriteableBitmap _bitmap;
        private Int32Rect _dirtyRect;
        private readonly int[] _stride = new int[1];

        public unsafe override void Render( H264Surface surface )
        {
            AVFrame* pFrame = surface.DecodedFrame != IntPtr.Zero ? (AVFrame*) surface.DecodedFrame : null;

            if ( pFrame == null )
            {
                return;
            }

            if ( ! OnRendering( ref surface ) )
            {
                return;
            }

            if ( _sws_context == null )
            {
                _sws_context = ffmpeg.sws_getContext( surface.FrameWidth , surface.FrameHeight,  AVPixelFormat.AV_PIX_FMT_YUV420P , surface.FrameWidth , surface.FrameHeight , AVPixelFormat.AV_PIX_FMT_RGB24 , ffmpeg.SWS_BILINEAR , null , null , null );

                if ( _sws_context == null )
                {
                    return;
                }
            }

            OnRender( ref surface , pFrame );
        }

        public void Close()
        {
            if ( _sws_context != null )
            {
                ffmpeg.sws_freeContext( _sws_context );
	            _sws_context = null;
            }

            _bitmap = null;
        }

        protected override void Dispose( bool disposing )
        {
            if ( disposing )
            {
                Close();
            }

            base.Dispose( disposing );
        }







        private bool OnRendering( ref H264Surface surface )
        {
            var image = surface.Options.TargetControl as System.Windows.Controls.Image;

            if ( image == null )
            {
                return false;
            }

            if ( _bitmap == null || _bitmap.Width != surface.FrameWidth || _bitmap.Height != surface.FrameHeight )
            {
                if ( _sws_context != null )
                {
                    ffmpeg.sws_freeContext( _sws_context );
	                _sws_context = null;
                }

                var dpi = VisualTreeHelper.GetDpi( image );

                var bitmap = new WriteableBitmap( surface.FrameWidth , surface.FrameHeight , dpi.PixelsPerInchX , dpi.PixelsPerInchY , PixelFormats.Rgb24  , null );

                using ( var locker = new WritableBitmapLocker( bitmap ) )
                {
                    var dirtyRect = new Int32Rect( 0 , 0 , surface.FrameWidth , surface.FrameHeight );

                    bitmap.AddDirtyRect( dirtyRect );

                    image.BeginInit();
                    image.Source = bitmap;

                    RenderOptions.SetCachingHint( image , CachingHint.Cache );
                    RenderOptions.SetBitmapScalingMode( image , BitmapScalingMode.NearestNeighbor );
                    RenderOptions.SetEdgeMode( image , EdgeMode.Aliased );

                    image.EndInit();

                    _bitmap = bitmap;
                    _dirtyRect = dirtyRect;
                }
            }

            return true;
        }

        private void OnRender( ref H264Surface surface , AVFrame* pFrame )
        {
            using ( var locker = new WritableBitmapLocker( _bitmap ) )
            {
                _stride[ 0 ] = _bitmap.BackBufferStride;

                var dstData = new byte_ptrArray8();
                dstData[0] = (byte*)_bitmap.BackBuffer;

                ffmpeg.sws_scale( _sws_context , pFrame->data , pFrame->linesize , 0 , surface.FrameHeight , dstData , _stride );

                _bitmap.AddDirtyRect( _dirtyRect );
            }
        }
    }
}