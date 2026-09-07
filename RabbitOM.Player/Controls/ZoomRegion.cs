using System;

namespace RabbitOM.Player.Controls
{
    public sealed class ZoomRegion
    {
        public ZoomRegion( double x , double y ,  double width , double height , double scaleX , double scaleY , double translationX , double translationY )
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            ScaleX = scaleX;
            ScaleY = scaleY;
            TranslationX = translationX;
            TranslationY = translationY;
        }

        public double Y { get; }

        public double X { get; }

        public double Height { get; }

        public double Width { get; }

        public double ScaleX { get; }

        public double ScaleY { get; }

        public double TranslationX { get; }

        public double TranslationY { get; }
    }
}
