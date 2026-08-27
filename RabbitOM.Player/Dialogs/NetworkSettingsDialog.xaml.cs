using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace RabbitOM.Player.Dialogs
{
    public partial class NetworkSettingsDialog : Window
    {
        public const string TcpTransport = "TCP";
        public const string UdpTransport = "UDP";
        public const string MulticastTransport = "MULTICAST";





        public static readonly RoutedCommand CloseCommand = new RoutedCommand();





        public NetworkSettingsDialog()
        {
            InitializeComponent();
        }






        public static readonly DependencyProperty SelectedTransportProperty =
            DependencyProperty.Register(
                nameof(SelectedTransport),
                    typeof(string),
                        typeof(NetworkSettingsDialog),
                            new PropertyMetadata(TcpTransport,OnSelectedTransportChanged));

        public static readonly DependencyProperty PortProperty=
            DependencyProperty.Register(
                nameof(Port),
                    typeof(int),
                        typeof(NetworkSettingsDialog),
                            new PropertyMetadata(5004),null);

        public static readonly DependencyProperty IPAddressProperty=
            DependencyProperty.Register(
                nameof(IPAddress),
                    typeof(string),
                        typeof(NetworkSettingsDialog),
                            new PropertyMetadata("224.0.0.1"),null);

        public static readonly DependencyProperty IsDatagramProtocolProperty=
            DependencyProperty.Register(
                nameof(IsDatagramProtocol),
                    typeof(bool),
                        typeof(NetworkSettingsDialog),
                            new PropertyMetadata(false),null);






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

        public bool IsDatagramProtocol
        {
            get => (bool) GetValue( IsDatagramProtocolProperty );
            private set => SetValue( IsDatagramProtocolProperty , value );
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

        private static void OnSelectedTransportChanged( DependencyObject sender , DependencyPropertyChangedEventArgs e )
        {
            var control = sender as NetworkSettingsDialog;

            if ( control != null )
            {
                var transport = e.NewValue as string;

                control.IsDatagramProtocol = transport == UdpTransport || transport == MulticastTransport;
            }
        }
    }
}
