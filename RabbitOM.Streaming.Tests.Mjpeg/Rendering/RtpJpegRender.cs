using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RabbitOM.Streaming.Tests.Mjpeg.Rendering
{
    public class RtpJpegRender : RtpRender
    {
        private WriteableBitmap _writableBitmap;

        private Int32Rect _bitmapRegion;

        private Rectangle _drawinRegion;




        public int DpiX { get; set; } = 96;
        public int DpiY { get; set; } = 96;




        public override void Render( byte[] frame )
        {
            try
            {
                using ( var bitmap = new Bitmap( new MemoryStream(frame)))
                {
                    DrawImage( bitmap );
                }
            }
            catch( Exception ex )
            {
                OnException( ex );
            }
            finally
            {
                _writableBitmap?.Unlock();
            }
        }

        public override void Clear()
        {
            SetImageSource( TargetControl , _writableBitmap = null );
        }




        protected virtual void DrawImage( Bitmap bitmap )
        {
            if ( _writableBitmap == null || _writableBitmap.PixelWidth != bitmap.Width || _writableBitmap.PixelHeight != bitmap.Height )
            {
                _writableBitmap = new WriteableBitmap(bitmap.Width,bitmap.Height,DpiX,DpiY, HighQuality ? PixelFormats.Bgr32 : PixelFormats.Bgr24 ,null);
                
                SetImageSource( TargetControl , _writableBitmap );

                _bitmapRegion = new Int32Rect(0,0,bitmap.Width,bitmap.Height);
                _drawinRegion = new Rectangle(0,0,bitmap.Width,bitmap.Height);
            }                    
            
            var bitmapData = bitmap.LockBits(_drawinRegion, ImageLockMode.ReadOnly,HighQuality ? System.Drawing.Imaging.PixelFormat.Format32bppRgb : System.Drawing.Imaging.PixelFormat.Format24bppRgb );

            _writableBitmap.Lock();
            _writableBitmap.WritePixels(_bitmapRegion, bitmapData.Scan0, bitmapData.Stride * bitmap.Height, bitmapData.Stride );

            bitmap.UnlockBits( bitmapData );
        }
    }
}
