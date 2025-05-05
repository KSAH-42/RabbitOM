using System;
using System.Windows;
using System.Windows.Media;

namespace RabbitOM.Streaming.Tests.Mjpeg.Rendering
{
    public abstract class RtpRender : IDisposable
    {
        ~RtpRender()
        {
            Dispose( false) ;
        }
        




        public bool HighQuality { get; set; }
        public FrameworkElement TargetControl { get; set; } 





        public abstract void Render( byte[] frame );

        public abstract void Clear();

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




        protected virtual void OnException( Exception ex )
        {
            System.Diagnostics.Debug.WriteLine( ex );
        }







        protected static void SetImageSource( FrameworkElement element , ImageSource source )
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
