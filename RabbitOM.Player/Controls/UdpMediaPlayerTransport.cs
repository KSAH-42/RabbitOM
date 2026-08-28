using System;
using System.Windows;

namespace RabbitOM.Player.Controls
{
    public sealed class UdpMediaPlayerTransport : MediaPlayerTransport
    {
        public static readonly DependencyProperty PortProperty =
            DependencyProperty.Register(
                nameof(Port),
                    typeof(int),
                        typeof(UdpMediaPlayerTransport),
                            new PropertyMetadata(5004));

        public int Port
        {
            get => (int) GetValue( PortProperty );
            set => SetValue( PortProperty , value );
        }
    }
}
