using System;
using System.Windows;

namespace RabbitOM.Player.Controls
{
    public abstract class MediaPlayerTransport : DependencyObject
    {
        public static readonly DependencyProperty ReceiveTimeoutProperty = DependencyProperty.Register( "ReceiveTimeout", typeof(TimeSpan) , typeof(MediaPlayerTransport) );

        public TimeSpan ReceiveTimeout
        {
            get => (TimeSpan) GetValue( ReceiveTimeoutProperty );
            set => SetValue( ReceiveTimeoutProperty , value );
        }
    }
}
