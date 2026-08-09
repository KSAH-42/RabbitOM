using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace RabbitOM.Player.Dialogs
{
    using RabbitOM.Player.Extensions;

    public partial class UrisDialog : Window
    {
        public static readonly RoutedCommand MoveUpCommand = new RoutedCommand();
        public static readonly RoutedCommand MoveDownCommand = new RoutedCommand();
        public static readonly RoutedCommand RemoveCommand = new RoutedCommand();
        public static readonly RoutedCommand CancelCommand = new RoutedCommand();
        public static readonly RoutedCommand AcceptCommand = new RoutedCommand();

        public static readonly DependencyProperty SelectedUriProperty = DependencyProperty.Register( "SelectedUri", typeof(UriInfo) , typeof(UrisDialog) );

        public UrisDialog()
        {
            InitializeComponent();
        }

        public UriInfo SelectedUri
        {
            get => GetValue( SelectedUriProperty ) as UriInfo;
            set => SetValue( SelectedUriProperty , value );
        }

        public ObservableCollection<UriInfo> Uris { get; } = new ObservableCollection<UriInfo>();

        private void OnMoveUp( object sender , ExecutedRoutedEventArgs e )
        {
            var selectedUris = Uris.Where( uri => uri.IsSelected ).ToList();

            Uris.MoveUp( selectedUris );
            Uris.ForEach( x => x.IsSelected = selectedUris.Contains( x ) );
            SelectedUri = selectedUris.FirstOrDefault();
        }

        private void OnMoveDown( object sender , ExecutedRoutedEventArgs e )
        {
            var selectedUris = Uris.Where( uri => uri.IsSelected ).ToList();

            Uris.MoveDown( selectedUris );
            Uris.ForEach( x => x.IsSelected = selectedUris.Contains( x ) );
            SelectedUri = selectedUris.LastOrDefault();
        }

        private void OnCanRemove( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = Uris.Any( uri => uri.IsSelected );
        }

        private void OnRemove( object sender , ExecutedRoutedEventArgs e )
        {
            if ( MessageBox.Show( "Would you like to remove these elements ?" , "Question" , MessageBoxButton.YesNo , MessageBoxImage.Question ) == MessageBoxResult.Yes )
            {
                Uris.RemoveRange( Uris.Where( uri => uri.IsSelected ).ToList() );
            }
        }

        private void OnCancel( object sender , ExecutedRoutedEventArgs e )
        {
            Close();
        }

        private void OnAccept( object sender , ExecutedRoutedEventArgs e )
        {
            DialogResult = true;
        }
    }
}
