using FFmpeg.AutoGen;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RabbitOM.Sample.Client.H264.Codecs.FFMpeg
{
    using RabbitOM.Sample.Client.H264.Decoders;

    public sealed unsafe class H264FFMpegRenderer : H264Renderer
    {
        private SwsContext* _sws_context = null;

        private WriteableBitmap _writableBitmap;

        private Int32Rect _updateRegion;

        private Image _image;

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
            if ( _image != null )
            {
                _image.Source = null;
                _image = null;
            }

            _writableBitmap = null;

            if ( _sws_context != null )
            {
                ffmpeg.sws_freeContext( _sws_context );
	            _sws_context = null;
            }
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
            _image = surface.Options.TargetControl as System.Windows.Controls.Image;

            if ( _image == null )
            {
                return false;
            }

            if ( _writableBitmap == null || _writableBitmap.Width != surface.FrameWidth || _writableBitmap.Height != surface.FrameHeight )
            {
                if ( _sws_context != null )
                {
                    ffmpeg.sws_freeContext( _sws_context );
	                _sws_context = null;
                }

                var dpi = VisualTreeHelper.GetDpi( _image );

                var writableBitmap = new WriteableBitmap( surface.FrameWidth , surface.FrameHeight , dpi.PixelsPerInchX , dpi.PixelsPerInchY , PixelFormats.Rgb24  , null );

                using ( var locker = new WritableBitmapLocker( writableBitmap ) )
                {
                    var updateRegion = new Int32Rect( 0 , 0 , surface.FrameWidth , surface.FrameHeight );

                    writableBitmap.AddDirtyRect( updateRegion );

                    _writableBitmap = writableBitmap;
                    _updateRegion = updateRegion;
                }

                _image.ConfigureSource( _writableBitmap );
            }

            return true;
        }

        private void OnRender( ref H264Surface surface , AVFrame* pFrame )
        {
            using ( var locker = new WritableBitmapLocker( _writableBitmap ) )
            {
                _stride[ 0 ] = _writableBitmap.BackBufferStride;

                var dstData = new byte_ptrArray8();
                dstData[0] = (byte*)_writableBitmap.BackBuffer;

                ffmpeg.sws_scale( _sws_context , pFrame->data , pFrame->linesize , 0 , surface.FrameHeight , dstData , _stride );

                _writableBitmap.AddDirtyRect( _updateRegion );
            }
        }
    }
}