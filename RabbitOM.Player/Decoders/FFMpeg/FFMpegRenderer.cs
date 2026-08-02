
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RabbitOM.Player.Codecs.FFMpeg
{
    using FFmpeg.AutoGen;

    public sealed unsafe class FFMpegRenderer : Renderer
    {
        private SwsContext* _sws_context = null;
        private WriteableBitmap _writableBitmap;
        private Image _image;
        private Int32Rect _updateRegion;
        private readonly int[] _stride = new int[1];

        public override bool IsOpened
        {
            get => _image != null;
        }

        public override void Open( FrameworkElement targetControl )
        {
            if ( _image != null )
            {
                throw new InvalidOperationException( "the render is already opened" );
            }

            var image = targetControl as Image;

            if ( image == null )
            {
                throw new ArgumentException( "the target control must be an image control" );
            }

            _image = image;
        }

        public unsafe override void Render( Surface surface )
        {
            if ( _image == null || surface == null )
            {
                return;
            }

            AVFrame* pFrame = surface.Frame != IntPtr.Zero ? (AVFrame*) surface.Frame : null;

            if ( pFrame == null || pFrame->data[0] == null )
            {
                return;
            }

            if ( _writableBitmap == null || surface.FrameWidth != _writableBitmap.PixelWidth || surface.FrameHeight != _writableBitmap.PixelHeight )
            {
                if ( _sws_context != null )
                {
                    ffmpeg.sws_freeContext( _sws_context );
                    _sws_context = null;
                }

                var dpi = VisualTreeHelper.GetDpi( _image );

                _writableBitmap = new WriteableBitmap( surface.FrameWidth , surface.FrameHeight , dpi.PixelsPerInchX , dpi.PixelsPerInchY , PixelFormats.Rgb24 , null );

                _updateRegion = new Int32Rect( 0 , 0 , surface.FrameWidth , surface.FrameHeight );

                _image.ConfigureSource( _writableBitmap );
            }

            if ( _sws_context == null )
            {
                _sws_context = ffmpeg.sws_getContext( surface.FrameWidth , surface.FrameHeight , AVPixelFormat.AV_PIX_FMT_YUV420P , surface.FrameWidth , surface.FrameHeight , AVPixelFormat.AV_PIX_FMT_RGB24 , ffmpeg.SWS_BILINEAR , null , null , null );

                if ( _sws_context == null )
                {
                    return;
                }
            }

            using ( var locker = new WritableBitmapLocker( _writableBitmap ) )
            {
                var dstData = new byte_ptrArray8();

                dstData[0] = (byte*) _writableBitmap.BackBuffer;
                _stride[0] = _writableBitmap.BackBufferStride;

                ffmpeg.sws_scale( _sws_context , pFrame->data , pFrame->linesize , 0 , surface.FrameHeight , dstData , _stride );

                _writableBitmap.AddDirtyRect( _updateRegion );
            }
        }

        public override void Close()
        {
            _writableBitmap = null;

            if ( _image != null )
            {
                _image.Source = null;
                _image = null;
            }

            if ( _sws_context != null )
            {
                ffmpeg.sws_freeContext( _sws_context );
	            _sws_context = null;
            }
        }
    }
}