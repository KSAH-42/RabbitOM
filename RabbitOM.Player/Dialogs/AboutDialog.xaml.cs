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

        private void OnVisitWebSite( object sender , ExecutedRoutedEventArgs e )
        {
            if ( ! _navigationService.TryNavigate( "https://github.com/KSAH-42/RabbitOM" , out var exception ) )
            {
                MessageBox.Show( exception.ToString() );
            }
        }

        private void OnClose( object sender , ExecutedRoutedEventArgs e )
        {
            Close();
        }
    }
}
