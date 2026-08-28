using System;
using System.Windows;

namespace RabbitOM.Player.Controls
{
    public abstract class MediaPlayerTransport : DependencyObject
    {
        public static readonly DependencyProperty ReceiveTimeoutProperty =
            DependencyProperty.Register(
                nameof(ReceiveTimeout),
                    typeof(TimeSpan),
                        typeof(MediaPlayerTransport),
                            new PropertyMetadata( TimeSpan.FromSeconds( 3 ) ));

        public static readonly DependencyProperty SendTimeoutProperty =
            DependencyProperty.Register(
                nameof(SendTimeout),
                    typeof(TimeSpan),
                        typeof(MediaPlayerTransport),
                            new PropertyMetadata( TimeSpan.FromSeconds( 3 ) ));

        public static readonly DependencyProperty RetriesIntervalProperty =
            DependencyProperty.Register(
                nameof(RetriesInterval),
                    typeof(TimeSpan),
                        typeof(MediaPlayerTransport),
                            new PropertyMetadata( TimeSpan.FromSeconds( 5 ) ));







        public TimeSpan ReceiveTimeout
        {
            get => (TimeSpan) GetValue( ReceiveTimeoutProperty );
            set => SetValue( ReceiveTimeoutProperty , value );
        }

        public TimeSpan SendTimeout
        {
            get => (TimeSpan) GetValue( SendTimeoutProperty );
            set => SetValue( SendTimeoutProperty , value );
        }

        public TimeSpan RetriesInterval
        {
            get => (TimeSpan) GetValue( RetriesIntervalProperty );
            set => SetValue( RetriesIntervalProperty , value );
        }
    }
}
