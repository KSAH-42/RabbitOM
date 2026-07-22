using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RabbitOM.Sample.Client.Mjpeg.Rendering
{
    public sealed class JpegRenderer : IDisposable
    {
        private WriteableBitmap _writableBitmap;

        private Int32Rect _bitmapRegion;

        private BitmapPixelsData _pixelsData;

        private FrameworkElement _targetControl;

        private byte[] _frame;

        private readonly RenderingSizeInfo _renderingSize = new RenderingSizeInfo();






        public JpegRenderer()
        {
            RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
        }






        public FrameworkElement TargetControl
        {
            get => _targetControl;
            set => _targetControl = value;
        }

        public byte[] Frame
        {
            get => _frame;
            set => _frame = value;
        }




        public bool CanRender()
        {
            return _targetControl != null && _frame?.Length > 0;
        }


        public void Render()
        {
            try
            {
                var decoder = JpegBitmapDecoder.Create( new MemoryStream( _frame ) , BitmapCreateOptions.IgnoreImageCache , BitmapCacheOption.None );

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

        public void Invalidate()
        {
            if ( _writableBitmap != null )
            {
                using ( var locker = new WritableBitmapLocker(_writableBitmap) )
                {
                    _writableBitmap.AddDirtyRect( _bitmapRegion );
                }
            }

            _targetControl?.InvalidateVisual();
        }

        public void Clear()
        {
            _pixelsData = BitmapPixelsData.Empty;
            _writableBitmap = null;
            _targetControl = null;
        }

        public void Dispose()
        {
        }





        private void OnDraw( BitmapSource frame )
        {
            if ( _renderingSize.ChangeValues( frame.Width , frame.Height ) )
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

                OnPrepare( _writableBitmap );
            }

            using ( var bitmapLocker = new WritableBitmapLocker( _writableBitmap ) )
            {
                _writableBitmap.WritePixels(_bitmapRegion, _pixelsData.Buffer , _pixelsData.Stride , 0 );
                _writableBitmap.AddDirtyRect( _bitmapRegion );
            }
        }

        private void OnPrepare( ImageSource source )
        {
            if ( _targetControl is System.Windows.Controls.Image image )
            {
                image.BeginInit();
                image.Source = source;

                if ( source != null )
                {
                    RenderOptions.SetCachingHint( image , CachingHint.Cache );
                    RenderOptions.SetBitmapScalingMode( image , BitmapScalingMode.NearestNeighbor );
                    RenderOptions.SetEdgeMode( image , EdgeMode.Aliased );
                }

                image.EndInit();
            }
        }

        private void OnException( Exception ex )
        {
            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
