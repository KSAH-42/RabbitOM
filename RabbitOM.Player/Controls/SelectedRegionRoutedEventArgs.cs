using System;
using System.Windows;

namespace RabbitOM.Player.Controls
{
    public class SelectedRegionRoutedEventArgs : RoutedEventArgs
    {
        public SelectedRegionRoutedEventArgs( RoutedEvent routedEvent , object source , double x , double y ,  double width , double height , double scaleX , double scaleY , double translationX , double translationY )
            : base( routedEvent , source )
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




        internal static bool IsValid( SelectedRegionRoutedEventArgs e )
        {
            return e != null && e.Source != null && e.Height > 0 && e.Width > 0;
        }
    }
}
