using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace RabbitOM.Player.Dialogs
{
    public partial class NetworkSettingsDialog : Window
    {
        public static readonly RoutedCommand CloseCommand = new RoutedCommand();
        public static readonly RoutedCommand CancelCommand = new RoutedCommand();

        public NetworkSettingsDialog()
        {
            InitializeComponent();
        }

        public ObservableCollection<string> Transports { get; } = new ObservableCollection<string>()
        {
            "TCP" , "UDP" , "MULTICAST"
        };

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
