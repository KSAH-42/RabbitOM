﻿using System;
using System.Windows;
using System.Windows.Media;

namespace RabbitOM.Streaming.Tests.Mjpeg.Drawing.Renders
{
    public abstract class RtpRender : IDisposable
    {
        static RtpRender()
        {
            RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
        }

        ~RtpRender()
        {
            Dispose( false) ;
        }
        




        public byte[] Frame { get; set; }
        public bool HighQuality { get; set; } = true;
        public int DpiX { get; set; } = Screen.Current.DpiX;
        public int DpiY { get; set; } = Screen.Current.DpiY;
        public FrameworkElement TargetControl { get; set; }     





        public virtual bool CanRender()
        {
            return TargetControl != null && Frame != null && Frame.Length > 0;
        }

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
