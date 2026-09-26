using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace RabbitOM.Player.Dialogs
{
    using RabbitOM.Player.Data;

    public partial class AboutDialog : Window
    {
        public static readonly RoutedCommand VisitWebSiteCommand = new RoutedCommand();
        public static readonly RoutedCommand CloseCommand = new RoutedCommand();

        public AboutDialog()
        {
            InitializeComponent();
        }

        public ReadOnlyObservableCollection<ModuleInfo> Modules { get; } = new ObservableCollection<ModuleInfo>( ModuleInfoFactory.GetCurrentProcessModules() ).ToReadOnly();

        private void OnVisitWebSite( object sender , ExecutedRoutedEventArgs e )
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo() { UseShellExecute = true , FileName = "https://github.com/KSAH-42/RabbitOM" }
            };

            try
            {
                process.Start();
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.ToString() );
            }
        }

        private void OnClose( object sender , ExecutedRoutedEventArgs e )
        {
            Close();
        }
    }
}
