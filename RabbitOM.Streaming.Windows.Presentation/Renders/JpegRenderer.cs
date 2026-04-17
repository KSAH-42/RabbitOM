using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace RabbitOM.Streaming.Windows.Presentation.Renders
{
    /// <summary>
    /// Represent a jpeg render
    /// </summary>
    public sealed class JpegRenderer : Renderer
    {
        private WriteableBitmap _writableBitmap;

        private Int32Rect _bitmapRegion;
        
        private BitmapPixelsData _pixelsData;

        private readonly RenderingSizeInfo _renderingSize = new RenderingSizeInfo();

        



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
        /// <param name="frame">the source</param>
        private void OnDraw( BitmapSource frame )
        {
            if ( ! _renderingSize.ChangeValues( frame.Width , frame.Height ) )
            {
                _pixelsData = BitmapPixelsData.Empty;
            }

            var succeed = BitmapPixelsData.IsNullOrEmpty( _pixelsData ) 
                ? BitmapPixelsData.TryCreate( frame, out _pixelsData )
                : BitmapPixelsData.TryCopy( frame, ref _pixelsData );
            
            if ( ! succeed )
            {
                return;
            }

            if ( _writableBitmap == null || _writableBitmap.PixelWidth != frame.Width || _writableBitmap.PixelHeight != frame.Height )
            {
                _writableBitmap = new WriteableBitmap(frame.PixelWidth,frame.PixelHeight,frame.DpiX,frame.DpiY, frame.Format , null);
                
                _bitmapRegion = new Int32Rect(0,0,frame.PixelWidth,frame.PixelHeight);
                
                SetImageSource( TargetControl , _writableBitmap );
            }   
            
            using ( var bitmapLocker = new WritableBitmapLocker( _writableBitmap ) )
            {
                _writableBitmap.WritePixels(_bitmapRegion, _pixelsData.Buffer , _pixelsData.Stride , 0 );
                _writableBitmap.AddDirtyRect( _bitmapRegion );
            }
        }
    }
}
