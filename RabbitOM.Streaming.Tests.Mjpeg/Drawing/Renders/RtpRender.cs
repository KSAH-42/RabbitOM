using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;

namespace RabbitOM.Streaming.Tests.Mjpeg.Drawing.Renders
{
    public abstract class RtpRender : IDisposable
    {
        protected RtpRender()
        {
            SystemEvents.PowerModeChanged += OnPowerModeChanged;
        }

        ~RtpRender()
        {
            Dispose( false) ;
        }
        




        public byte[] Frame { get; set; }
        public bool HighQuality { get; set; }
        public FrameworkElement TargetControl { get; set; } 




        public abstract void Render();

        public abstract void Clear();

        public virtual void Invalidate()
        {
            TargetControl?.InvalidateVisual();
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
                SystemEvents.PowerModeChanged -= OnPowerModeChanged;
                Clear();
            }
        }




        protected virtual void OnPowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Resume)
            {
                Invalidate();
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
