using System;
using System.Windows;

namespace RabbitOM.Player.Controls
{
    public delegate void RoutedEventHandler<in TRoutedEventArgs>( object sender , TRoutedEventArgs e ) where TRoutedEventArgs : RoutedEventArgs;
}
