using System;

namespace RabbitOM.Sample.Client.Mjpeg.Rendering
{
    public sealed class RenderingSizeInfo
    {
        public double ActualWidth { get; private set; }

        public double PreviousWidth { get; private set; }

        public double ActualHeight { get; private set; }

        public double PreviousHeight { get; private set; }




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

        public void ClearValues()
        {
            ActualWidth = 0;
            ActualHeight = 0;
            PreviousWidth = 0;
            PreviousHeight = 0;
        }
    }
}
