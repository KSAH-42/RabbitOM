using System;
using System.Windows;

namespace RabbitOM.Player.Controls
{
    public sealed class UdpMediaPlayerTransport : MediaPlayerTransport
    {
        public static readonly DependencyProperty RtpPortProperty = DependencyProperty.Register( "RtpPort", typeof(int) , typeof(UdpMediaPlayerTransport) );

        public int RtpPort
        {
            get => (int) GetValue( RtpPortProperty );
            set => SetValue( RtpPortProperty , value );
        }
    }
}
