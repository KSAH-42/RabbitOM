using System;

namespace RabbitOM.Streaming.Windows.Presentation.Renders
{
    /// <summary>
    /// Represent the render size infos
    /// </summary>
    public sealed class RenderingSizeInfo
    {
        /// <summary>
        /// Gets the width
        /// </summary>
        public double ActualWidth { get; private set; }
        
        /// <summary>
        /// Gets the previous width
        /// </summary>
        public double PreviousWidth { get; private set; }
        
        /// <summary>
        /// Gets the height
        /// </summary>
        public double ActualHeight { get; private set; }
        
        /// <summary>
        /// Gets the previous height
        /// </summary>
        public double PreviousHeight { get; private set; }





        /// <summary>
        /// Changes the values
        /// </summary>
        /// <param name="width">the width</param>
        /// <param name="height">the height</param>
        /// <returns>returns true for success, otherwise false</returns>
        public bool ChangeValues( double width , double height )
        {
            if ( ActualWidth == width && ActualHeight == height )
            {
                return false;
            }

            PreviousWidth = ActualWidth;
            PreviousHeight = ActualHeight;
            
            ActualWidth = width;
            ActualHeight = height;

            return true;
        }

        /// <summary>
        /// Clear the values
        /// </summary>
        public void ClearValues()
        {
            ActualWidth = 0;
            ActualHeight = 0;
            PreviousWidth = 0;
            PreviousHeight = 0;
        }
    }
}
