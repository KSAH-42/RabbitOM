using System;
using System.Windows;

namespace RabbitOM.Player.Controls
{
    public class SelectedRegionRoutedEventArgs : RoutedEventArgs
    {
        public SelectedRegionRoutedEventArgs( RoutedEvent routedEvent , object source , double top , double left , double width , double height )
            : base( routedEvent , source )
        {
            Top = top;
            Left = left;
            Width = width;
            Height = height;
            Right = Left + width;
            Bottom = Top + height;
        }




        public double Top { get; }

        public double Left { get; }

        public double Bottom { get; }

        public double Right { get; }

        public double Height { get; }

        public double Width { get; }




        public static bool IsValid( SelectedRegionRoutedEventArgs e )
        {
            return e != null && e.Source != null && e.Height > 0 && e.Width > 0;
        }
    }
}
