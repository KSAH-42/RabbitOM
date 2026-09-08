using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RabbitOM.Player
{
    using RabbitOM.Net.Rtsp;
    using RabbitOM.Player.Configuration;
    using RabbitOM.Player.Controls;
    using RabbitOM.Player.Dialogs;
    using System.Windows.Media;
    using DialogStyle = RabbitOM.Player.Themes.Styles.WindowStyle;

    public partial class MainWindow : Window
    {
        public static readonly RoutedCommand ControlCommand = new RoutedCommand();
        public static readonly RoutedCommand SaveImageCommand = new RoutedCommand();
        public static readonly RoutedCommand ShowAboutDialogCommand = new RoutedCommand();
        public static readonly RoutedCommand ShowUrisDialogCommand = new RoutedCommand();
        public static readonly RoutedCommand ShowNetworkSettingsDialogCommand = new RoutedCommand();
        public static readonly RoutedCommand ToggleFullScreenCommand = new RoutedCommand();
        public static readonly RoutedCommand FocusCommand = new RoutedCommand();

        public static readonly DependencyProperty ButtonStatusProperty = DependencyProperty.Register( nameof(ButtonStatus), typeof(string) , typeof(MainWindow) , new PropertyMetadata( "Play" ) );
        public static readonly DependencyProperty SelectedSourceProperty = DependencyProperty.Register( nameof(SelectedSource), typeof(string) , typeof(MainWindow) );

        public string ButtonStatus
        {
            get => GetValue( ButtonStatusProperty ) as string;
            set => SetValue( ButtonStatusProperty , value );
        }

        public string SelectedSource
        {
            get => GetValue( SelectedSourceProperty ) as string;
            set => SetValue( SelectedSourceProperty , value );
        }

        public ObservableCollection<string> Sources { get; } = new ObservableCollection<string>( new ApplicationConfiguration().GetSourcesOrDefault().Select( element => element.Uri ) );

        private void OnWindowClosing( object sender , System.ComponentModel.CancelEventArgs e )
        {
            _mediaPlayer.Stop();
        }

        private void OnControl( object sender , ExecutedRoutedEventArgs e )
        {
            try
            {
                if ( _mediaPlayer.IsStarted )
                {
                    _mediaPlayer.Stop();
                    return;
                }

                if ( ! RtspUri.TryParse( SelectedSource , out RtspUri uri ) )
                {
                    MessageBox.Show( "Please enter a valid uri" , "Uri format" , MessageBoxButton.OK , MessageBoxImage.Information );
                    return;
                }

                if ( ! Sources.Any( uriValue => StringComparer.OrdinalIgnoreCase.Equals( uriValue ?? string.Empty , SelectedSource ?? string.Empty ) ) )
                {
                    Sources.Add( SelectedSource );
                }

                _mediaPlayer.Source = uri.ToString( true );
                _mediaPlayer.UserName = uri.UserName;
                _mediaPlayer.Password = uri.Password;

                _mediaPlayer.Configure();
                _mediaPlayer.Play();
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message , "Error" , MessageBoxButton.OK , MessageBoxImage.Error );
            }
            finally
            {
                ButtonStatus = _mediaPlayer.IsStarted ? "Stop" : "Play";
            }
        }

        private void OnCloseApplication( object sender , ExecutedRoutedEventArgs e )
        {
            if ( MessageBox.Show( "Would you like to close the application ?" , "Closing Application" , MessageBoxButton.YesNo , MessageBoxImage.Question ) == MessageBoxResult.Yes )
            {
                Close();
            }
        }

        private void OnCanSaveImage( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _mediaPlayer.Image.Source is BitmapSource;
        }

        private void OnSaveImage( object sender , ExecutedRoutedEventArgs e )
        {
            var dialog = new SaveImageDialog() { Owner = Window.GetWindow( this ) };

            dialog.Source = _mediaPlayer.Image.Source as BitmapSource;

            dialog.TakeSnasphot();
            dialog.ShowDialog();
        }

        private void OnShowAboutDialog( object sender , ExecutedRoutedEventArgs e )
        {
            var dialog = new AboutDialog() { Owner = Window.GetWindow( this ) };

            dialog.ShowDialog();
        }

        private void OnShowUrisDialog( object sender , ExecutedRoutedEventArgs e )
        {
            var dialog = new UrisDialog() { Owner = Window.GetWindow( this ) };

            dialog.Uris.AddRange( Sources.Select( uri => new UriInfo() { Value = uri } ) );

            if (dialog.ShowDialog() == true )
            {
                var selectedUri = SelectedSource;

                Sources.Clear();
                Sources.AddRange( dialog.Uris.Select( uri => uri.Value ) );
                SelectedSource = Sources.Contains( selectedUri ) ? selectedUri : Sources.FirstOrDefault();
            }
        }

        private void OnShowNetworkSettingsDialog( object sender , ExecutedRoutedEventArgs e )
        {
            var dialog = new NetworkSettingsDialog() { Owner = Window.GetWindow( this ) };

            if ( dialog.ShowDialog() == true )
            {
                if ( dialog.UseUdpTransport )
                {
                    _mediaPlayer.Transport = new UdpMediaPlayerTransport() { Port = dialog.Port };
                }

                else if ( dialog.UseMulticastTransport )
                {
                    _mediaPlayer.Transport = new MulticastMediaPlayerTransport() { Port = dialog.Port , IPAddress = dialog.IPAddress };
                }
                else
                {
                    _mediaPlayer.Transport = new TcpMediaPlayerTransport();
                }
            }
        }

        private void OnToggleFullScreen( object sender , ExecutedRoutedEventArgs e )
        {
            DialogStyle.SetFullScreen( this , ! DialogStyle.GetFullScreen( this ) );
        }

        private void OnControlFocus( object sender , ExecutedRoutedEventArgs e )
        {
            var source = e.Parameter as UIElement;

            if ( source == null )
            {
                return;
            }

            source.Focus();
        }
    }
}
