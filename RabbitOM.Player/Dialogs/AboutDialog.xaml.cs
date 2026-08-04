using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace RabbitOM.Player.Dialogs
{
    public partial class AboutDialog : Window
    {
        public static readonly RoutedCommand CloseCommand = new RoutedCommand();

        public AboutDialog()
        {
            InitializeComponent();
        }

        public ObservableCollection<ModuleInfo> Modules { get; } = new ObservableCollection<ModuleInfo>( ModuleInfoFactory.GetCurrentProcessModules() );

        private void OnClose( object sender , ExecutedRoutedEventArgs e )
        {
            Close();
        }
    }
}
