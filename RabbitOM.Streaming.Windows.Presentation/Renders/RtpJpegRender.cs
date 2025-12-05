using System;
using System.IO;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RabbitOM.Streaming.Windows.Presentation.Renders
{
    /// <summary>
    /// Represent a jpeg decoder
    /// </summary>
    public sealed class RtpJpegRender : RtpRender
    {
        private WriteableBitmap _writableBitmap;

        private Int32Rect _bitmapRegion;
        
        private Rectangle _drawinRegion;

        private BitmapPixelsData _pixelsData;





        /// <summary>
        /// Do rendering
        /// </summary>
        public override void Render()
        {
            try 
            {
                var decoder = JpegBitmapDecoder.Create( new MemoryStream( Frame ) , BitmapCreateOptions.IgnoreImageCache , BitmapCacheOption.None );
                
                if ( decoder.Frames.Count > 0 )
                {
                    OnDraw( decoder.Frames[0] );
                }
            }
            catch ( Exception ex )
            {
                OnException( ex );
            }
        }

        /// <summary>
        /// Invalidate
        /// </summary>
        public override void Invalidate()
        {
            if ( _writableBitmap != null )
            {
                using ( var locker = new WritableBitmapLocker(_writableBitmap) )
                {
                    _writableBitmap.AddDirtyRect( _bitmapRegion );
                }
            }

            base.Invalidate();
        }
        
        /// <summary>
        /// Clear
        /// </summary>
        public override void Clear()
        {
            _pixelsData = BitmapPixelsData.Empty;

            SetImageSource( TargetControl , _writableBitmap = null );
        }






        /// <summary>
        /// Occurs when the image must be draw
        /// </summary>
        /// <param name="source">the source</param>
        private void OnDraw( BitmapSource source )
        {
            if ( source == null )
            {
                return;
            }

            if ( _writableBitmap == null || _writableBitmap.PixelWidth != source.Width || _writableBitmap.PixelHeight != source.Height )
            {
                _writableBitmap = new WriteableBitmap(source.PixelWidth,source.PixelHeight,DpiX,DpiY, source.Format , null);
                
                SetImageSource( TargetControl , _writableBitmap );

                _bitmapRegion = new Int32Rect(0,0,source.PixelWidth,source.PixelHeight);
                _drawinRegion = new Rectangle(0,0,source.PixelWidth,source.PixelHeight);
            }                    
            
            if ( BitmapPixelsData.IsNullOrEmpty( _pixelsData ) )
            {
                BitmapPixelsData.TryCreate( source, out _pixelsData );
            }
            else
            {
                BitmapPixelsData.TryCopy( source, ref _pixelsData );
            }

            if ( ! BitmapPixelsData.IsNullOrEmpty( _pixelsData ) )
            {
                using ( var bitmapLocker = new WritableBitmapLocker( _writableBitmap ) )
                {
                    _writableBitmap.WritePixels(_bitmapRegion, _pixelsData.Buffer , _pixelsData.Stride , 0 );
                    _writableBitmap.AddDirtyRect( _bitmapRegion );
                }
            }
        }
    }
}
