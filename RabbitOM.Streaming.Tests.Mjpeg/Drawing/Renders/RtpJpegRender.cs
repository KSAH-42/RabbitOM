using System;
using System.IO;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RabbitOM.Streaming.Tests.Mjpeg.Drawing.Renders
{
    public class RtpJpegRender : RtpRender
    {
        private WriteableBitmap _writableBitmap;
        private Int32Rect _bitmapRegion;
        private Rectangle _drawinRegion;




        public int DpiX { get; set; } = 96;
        public int DpiY { get; set; } = 96;




        public override void Render()
        {
            if ( Frame == null )
            {
                return;
            }

            try
            {
                using ( var bitmap = new Bitmap( new MemoryStream( Frame ) ) )
                {
                    DrawImage( bitmap );
                }
            }
            catch( Exception ex )
            {
                OnException( ex );
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
            
            using ( var dataLocker = new BitmapDataLocker(bitmap, _drawinRegion, HighQuality ) )
            using ( var bitmapLocker = new WritableBitmapLocker( _writableBitmap ) )
            {
                _writableBitmap.WritePixels(_bitmapRegion, dataLocker.GetScan0() , dataLocker.GetStride() , dataLocker.GetOffset() );
                _writableBitmap.AddDirtyRect( _bitmapRegion );
            }
        }
    }
}
