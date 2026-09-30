using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace RabbitOM.Player.Dialogs
{
    using RabbitOM.Player.Data;
    using RabbitOM.Player.Services;

    public partial class AboutDialog : Window
    {
        public static readonly RoutedCommand VisitWebSiteCommand = new RoutedCommand();
        public static readonly RoutedCommand CloseCommand = new RoutedCommand();

        private readonly WebNavigationService _navigationService = new WebNavigationService();

        public AboutDialog()
        {
            InitializeComponent();
        }

        public ReadOnlyObservableCollection<ModuleInfo> Modules { get; } = new ObservableCollection<ModuleInfo>( ModuleInfoFactory.GetCurrentProcessModules() ).ToReadOnly();

        private void OnClose( object sender , ExecutedRoutedEventArgs e )
        {
            Close();
        }

        private void OnVisitWebSite( object sender , ExecutedRoutedEventArgs e )
        {
            try
            {
                _navigationService.Navigate( "https://github.com/KSAH-42/RabbitOM" );
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.ToString() );
            }
        }
    }
}
