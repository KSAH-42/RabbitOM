using System;
using System.Windows;

namespace RabbitOM.Player.Controls
{
    public sealed class MulticastMediaPlayerTransport : MediaPlayerTransport
    {
        public static readonly DependencyProperty IPAddressProperty =
            DependencyProperty.Register(
                nameof(IPAddress),
                    typeof(string),
                        typeof(MulticastMediaPlayerTransport),
                            new PropertyMetadata( "224.0.0.1" ) );

        public static readonly DependencyProperty PortProperty =
            DependencyProperty.Register(nameof(Port),
                typeof(int),
                    typeof(MulticastMediaPlayerTransport),
                        new PropertyMetadata(5004));

        public static readonly DependencyProperty TimeToLiveProperty =
            DependencyProperty.Register(
                    nameof(TimeToLive),
                        typeof(byte),
                            typeof(MulticastMediaPlayerTransport),
                                new PropertyMetadata(1));






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

        public byte TimeToLive
        {
            get => (byte) GetValue( TimeToLiveProperty );
            set => SetValue( TimeToLiveProperty , value );
        }
    }
}
