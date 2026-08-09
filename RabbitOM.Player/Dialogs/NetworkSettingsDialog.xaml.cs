using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace RabbitOM.Player.Dialogs
{
    public partial class NetworkSettingsDialog : Window
    {
        public static readonly RoutedCommand CloseCommand = new RoutedCommand();

        public static readonly DependencyProperty SelectedTransportProperty = DependencyProperty.Register( "SelectedTransport", typeof(string) , typeof(NetworkSettingsDialog) ,  new PropertyMetadata( TcpTransport ) , null );
        public static readonly DependencyProperty PortProperty = DependencyProperty.Register( "Port", typeof(int) , typeof(NetworkSettingsDialog) , new PropertyMetadata( 5004 ) , null );
        public static readonly DependencyProperty IPAddressProperty = DependencyProperty.Register( "IPAddress", typeof(string) , typeof(NetworkSettingsDialog) , new PropertyMetadata( "224.0.0.1" ) , null );

        public const string TcpTransport = "TCP";
        public const string UdpTransport = "UDP";
        public const string MulticastTransport = "MULTICAST";

        public NetworkSettingsDialog()
        {
            InitializeComponent();
        }

        public bool UseTcpTransport
        {
            get => SelectedTransport == TcpTransport;
        }

        public bool UseUdpTransport
        {
            get => SelectedTransport == UdpTransport;
        }

        public bool UseMulticastTransport
        {
            get => SelectedTransport == MulticastTransport;
        }

        public int Port
        {
            get => (int) GetValue( PortProperty );
            set => SetValue( PortProperty , value );
        }

        public string IPAddress
        {
            get => GetValue( IPAddressProperty ) as string;
            set => SetValue( IPAddressProperty , value );
        }

        public string SelectedTransport
        {
            get => GetValue( SelectedTransportProperty ) as string;
            set => SetValue( SelectedTransportProperty , value );
        }

        public ObservableCollection<string> Transports { get; } = new ObservableCollection<string>() { TcpTransport , UdpTransport , MulticastTransport };

        private void OnCancel( object sender , ExecutedRoutedEventArgs e )
        {
            Close();
        }

        private void OnClose( object sender , ExecutedRoutedEventArgs e )
        {
            DialogResult = true;
        }
    }
}
