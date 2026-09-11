using System;
using System.Windows;

namespace RabbitOM.Player.Controls
{
    using RabbitOM.Player.Data;

    public class ZoomChangedRoutedEventArgs : RoutedEventArgs
    {
        public ZoomChangedRoutedEventArgs( RoutedEvent routedEvent , object source , ZoomRegion region ) : base( routedEvent , source )
        {
            Region = region ?? throw new ArgumentNullException( nameof( region ) );
        }

        public ZoomRegion Region { get; }
 
        internal static bool IsValid( ZoomChangedRoutedEventArgs e )
        {
            return e != null && e.Source != null && e.Region.Height > 0 && e.Region.Width > 0;
        }
    }
}
