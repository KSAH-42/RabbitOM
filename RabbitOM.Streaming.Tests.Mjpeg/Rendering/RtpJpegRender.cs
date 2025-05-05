using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace RabbitOM.Streaming.Tests.Mjpeg.Rendering
{
    public class RtpJpegRender : IDisposable
    {
        private WriteableBitmap _writableBitmap;
        private Int32Rect _rec;





        ~RtpJpegRender()
        {
            Dispose( false) ;
        }
        




        public int DpiX { get; set; } = 96;
        public int DpiY { get; set; } = 96;
        public FrameworkElement TargetControl { get; set; } 
        public bool HighQuality { get; set; }





        public void Render( byte[] frame )
        {
            try
            {
                using ( var bitmap = new System.Drawing.Bitmap( new MemoryStream(frame)))
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


        public void Clear()
        {
            SetImageSource( TargetControl , _writableBitmap = null );
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
            {
                Clear();
            }
        }

        protected virtual void DrawImage( Bitmap bitmap )
        {
            if ( _writableBitmap == null || _writableBitmap.PixelWidth != bitmap.Width || _writableBitmap.PixelHeight != bitmap.Height )
            {
                _writableBitmap = new WriteableBitmap(bitmap.Width,bitmap.Height,DpiX,DpiY, HighQuality ? PixelFormats.Bgr32 : PixelFormats.Bgr24 ,null);
                
                _rec = new Int32Rect(0,0,bitmap.Width,bitmap.Height);

                SetImageSource( TargetControl , _writableBitmap );
            }                    
            
            var bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,HighQuality ? System.Drawing.Imaging.PixelFormat.Format32bppRgb : System.Drawing.Imaging.PixelFormat.Format24bppRgb );

            _writableBitmap.Lock();
            _writableBitmap.WritePixels(_rec, bmpData.Scan0, bmpData.Stride * bitmap.Height, bmpData.Stride );

            bitmap.UnlockBits(bmpData);
        }






        protected virtual void OnException( Exception ex )
        {
            System.Diagnostics.Debug.WriteLine( ex );
        }







        private static void SetImageSource( FrameworkElement element , ImageSource source )
        {
            if ( element is System.Windows.Controls.Image image )
            {
                image.BeginInit();
                image.Source = source;
                image.EndInit();
            }
        }
    }
}
