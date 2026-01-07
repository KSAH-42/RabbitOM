using System;
using System.Windows;
using System.Windows.Media;

namespace RabbitOM.Streaming.Windows.Presentation.Renders
{
    /// <summary>
    /// Represent the base render class
    /// </summary>
    public abstract class Renderer : IDisposable
    {
        static Renderer()
        {
            RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
        }

        /// <summary>
        /// Release resources
        /// </summary>
        ~Renderer()
        {
            Dispose( false) ;
        }
        






        /// <summary>
        /// Gets / Sets the frame buffer
        /// </summary>
        public byte[] Frame { get; set; }

        /// <summary>
        /// Gets / Sets the target control
        /// </summary>
        public FrameworkElement TargetControl { get; set; }     
        






        /// <summary>
        /// Set image source
        /// </summary>
        /// <param name="element">the framework element</param>
        /// <param name="source">the source</param>
        protected static void SetImageSource( FrameworkElement element , ImageSource source )
        {
            if ( element is System.Windows.Controls.Image image )
            {
                image.BeginInit();
                image.Source = source;
                image.EndInit();
            }
        }







        /// <summary>
        /// Check if we can do a rendering
        /// </summary>
        /// <returns>returns true for a success, otherwise false.</returns>
        public virtual bool CanRender()
        {
            return TargetControl != null && Frame?.Length > 0;
        }

        /// <summary>
        /// Render
        /// </summary>
        public abstract void Render();

        /// <summary>
        /// Clear
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Invalidate / Refresh
        /// </summary>
        public virtual void Invalidate()
        {
            TargetControl?.InvalidateVisual();
        }

        /// <summary>
        /// Release resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Release resources
        /// </summary>
        /// <param name="disposing">true if dispose method is call explicitly</param>
        protected virtual void Dispose( bool disposing )
        {
            if ( disposing )
            {
                Clear();
            }
        }
        






        /// <summary>
        /// Occurs during an exception
        /// </summary>
        /// <param name="ex">the exception</param>
        protected virtual void OnException( Exception ex )
        {
            System.Diagnostics.Debug.WriteLine( ex );
        }
    }
}
