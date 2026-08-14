using System;
using System.Windows;

namespace RabbitOM.Player.Controls
{
    public sealed class MulticastMediaPlayerTransport : MediaPlayerTransport
    {
        public static readonly DependencyProperty IPAddressProperty = DependencyProperty.Register( "IPAddress", typeof(string) , typeof(MulticastMediaPlayerTransport) );
        public static readonly DependencyProperty PortProperty = DependencyProperty.Register( "Port", typeof(int) , typeof(MulticastMediaPlayerTransport) );
        public static readonly DependencyProperty TTLProperty = DependencyProperty.Register( "TTL", typeof(byte) , typeof(MulticastMediaPlayerTransport) , new PropertyMetadata( 1 ) );

        public string IPAddress
        {
            get => GetValue( IPAddressProperty ) as string;
            set => SetValue( IPAddressProperty , value );
        }

        public int Port
        {
            get => (int) GetValue( PortProperty );
            set => SetValue( PortProperty , value );
        }

        public byte TTL
        {
            get => (byte) GetValue( TTLProperty );
            set => SetValue( TTLProperty , value );
        }
    }
}
