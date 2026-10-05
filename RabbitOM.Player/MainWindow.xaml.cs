using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RabbitOM.Player
{
	using RabbitOM.Net.Rtsp;
	using RabbitOM.Player.Configuration;
    using RabbitOM.Player.Controls;
	using RabbitOM.Player.Data;
	using RabbitOM.Player.Dialogs;
	using RabbitOM.Player.Services;
	using DialogStyle = RabbitOM.Player.Themes.Styles.WindowStyle;

    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty StreamingStatusProperty = DependencyProperty.Register( nameof(StreamingStatus), typeof(bool) , typeof(MainWindow) );
        public static readonly DependencyProperty SelectedSourceProperty = DependencyProperty.Register( nameof(SelectedSource), typeof(string) , typeof(MainWindow) );
        public static readonly DependencyProperty StretchImageProperty = DependencyProperty.Register( nameof(StretchImage), typeof(Stretch) , typeof(MainWindow) , new PropertyMetadata(Stretch.Fill,OnStretchImageChanged) );
        public static readonly DependencyProperty InfoVisibilityProperty = DependencyProperty.Register( nameof(InfoVisibility), typeof(Visibility) , typeof(MainWindow) , new PropertyMetadata(Visibility.Collapsed,OnInfoVisibilityChanged) );
        public static readonly DependencyProperty StatisticsVisibilityProperty = DependencyProperty.Register( nameof(StatisticsVisibility), typeof(Visibility) , typeof(MainWindow) , new PropertyMetadata(Visibility.Visible,OnStatisticsVisibilityChanged) );
        public static readonly DependencyProperty ScriptProperty = DependencyProperty.Register( nameof(Script), typeof(string) , typeof(MainWindow) );





        public MainWindow()
        {
            InitializeComponent();

            CommandBindings.Add( new CommandBinding( ToggleStartStopStreamingCommand , OnToggleStartStopStreaming ));
            CommandBindings.Add( new CommandBinding( StartStreamingCommand , OnStartStreaming ));
            CommandBindings.Add( new CommandBinding( StopStreamingCommand , OnStopStreaming ));
            CommandBindings.Add( new CommandBinding( ToggleFullScreenCommand , OnToggleFullScreen ));
            CommandBindings.Add( new CommandBinding( ToggleStretchCommand , OnToggleStretch ));
            CommandBindings.Add( new CommandBinding( ShowNetworkSettingsDialogCommand , OnShowNetworkSettingsDialog ));
            CommandBindings.Add( new CommandBinding( ShowUrisDialogCommand , OnShowUrisDialog ));
            CommandBindings.Add( new CommandBinding( ShowAboutDialogCommand , OnShowAboutDialog ));
            CommandBindings.Add( new CommandBinding( ShowStatisticsCommand , OnShowStatistics ));
            CommandBindings.Add( new CommandBinding( ShowInfoCommand , OnShowInfo ));
            CommandBindings.Add( new CommandBinding( FocusCommand , OnFocus ));
            CommandBindings.Add( new CommandBinding( SaveImageCommand , OnSaveImage , OnCanSaveImage ));

            _scriptEditor.Runner = new ScriptRunner();
        }







        public bool StreamingStatus
        {
            get => (bool) GetValue( StreamingStatusProperty );
            private set => SetValue( StreamingStatusProperty , value );
        }

        public string SelectedSource
        {
            get => GetValue( SelectedSourceProperty ) as string;
            set => SetValue( SelectedSourceProperty , value );
        }

        public string Script
        {
            get => GetValue( ScriptProperty ) as string;
            set => SetValue( ScriptProperty , value );
        }

        public Stretch StretchImage
        {
            get => (Stretch) GetValue( StretchImageProperty );
            set => SetValue( StretchImageProperty , value );
        }

        public Visibility InfoVisibility
        {
            get => (Visibility) GetValue( InfoVisibilityProperty );
            set => SetValue( InfoVisibilityProperty , value );
        }

        public Visibility StatisticsVisibility
        {
            get => (Visibility) GetValue( StatisticsVisibilityProperty );
            set => SetValue( StatisticsVisibilityProperty , value );
        }

        public ObservableCollection<string> Sources { get; } = new ObservableCollection<string>( new ApplicationConfiguration().GetSourcesOrDefault().Select( element => element.Uri ) );







        public RoutedCommand CloseCommand { get; } = new RoutedCommand();
        public RoutedCommand StartStreamingCommand { get; } = new RoutedCommand();
        public RoutedCommand StopStreamingCommand { get; } = new RoutedCommand();
        public RoutedCommand ToggleStartStopStreamingCommand { get; } = new RoutedCommand();
        public RoutedCommand ToggleFullScreenCommand { get; } = new RoutedCommand();
        public RoutedCommand ToggleStretchCommand { get; } = new RoutedCommand();
        public RoutedCommand ShowNetworkSettingsDialogCommand { get; } = new RoutedCommand();
        public RoutedCommand ShowUrisDialogCommand { get; } = new RoutedCommand();
        public RoutedCommand ShowAboutDialogCommand { get; } = new RoutedCommand();
        public RoutedCommand ShowStatisticsCommand { get; } = new RoutedCommand();
        public RoutedCommand ShowInfoCommand { get; } = new RoutedCommand();
        public RoutedCommand FocusCommand { get; } = new RoutedCommand();
        public RoutedCommand SaveImageCommand { get; } = new RoutedCommand();
        public RoutedCommand StretchCommand { get; } = new RoutedCommand();







        private void OnWindowClosing( object sender , CancelEventArgs e )
        {
            _mediaPlayer.Stop();
            _mediaPlayer.Dispose();
        }

        private void OnCloseApplication( object sender , ExecutedRoutedEventArgs e )
        {
            if ( MessageBox.Show( "Would you like to close the application ?" , "Closing Application" , MessageBoxButton.YesNo , MessageBoxImage.Question ) == MessageBoxResult.Yes )
            {
                Close();
            }
        }

        private void OnToggleStartStopStreaming( object sender , ExecutedRoutedEventArgs e )
        {
            Action<object,ExecutedRoutedEventArgs> action = StreamingStatus ? OnStopStreaming : OnStartStreaming;

            action( sender , e );
        }

        private void OnStartStreaming( object sender , ExecutedRoutedEventArgs e )
        {
            try
            {
                if ( _mediaPlayer.IsStarted )
                {
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
                StreamingStatus = _mediaPlayer.IsStarted;
            }
        }

        private void OnStopStreaming( object sender , ExecutedRoutedEventArgs e )
        {
            _mediaPlayer.Stop();
            StreamingStatus = false;
        }

        private void OnToggleFullScreen( object sender , ExecutedRoutedEventArgs e )
        {
            DialogStyle.SetFullScreen( this , ! DialogStyle.GetFullScreen( this ) );
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

        private void OnShowAboutDialog( object sender , ExecutedRoutedEventArgs e )
        {
            var dialog = new AboutDialog() { Owner = Window.GetWindow( this ) };

            dialog.ShowDialog();
        }

        private void OnToggleStretch( object sender , ExecutedRoutedEventArgs e )
        {
            StretchImage = StretchImage == Stretch.Fill
                ? Stretch.Uniform
                : Stretch.Fill
                ;
        }

        private void OnShowStatistics( object sender , ExecutedRoutedEventArgs e )
        {
            StatisticsVisibility = StatisticsVisibility == Visibility.Visible 
                ? Visibility.Collapsed
                : Visibility.Visible
                ;
        }

        private void OnShowInfo( object sender , ExecutedRoutedEventArgs e )
        {
            InfoVisibility = InfoVisibility == Visibility.Visible
                ? Visibility.Collapsed
                : Visibility.Visible
                ;
        }

        private void OnCanSaveImage( object sender , CanExecuteRoutedEventArgs e )
        {
            e.CanExecute = _mediaPlayer.GetImage() is BitmapSource;
        }

        private void OnSaveImage( object sender , ExecutedRoutedEventArgs e )
        {
            var dialog = new SaveImageDialog() { Owner = Window.GetWindow( this ) };

            dialog.Source = _mediaPlayer.GetImage() as BitmapSource;

            dialog.TakeSnasphot();
            dialog.ShowDialog();
        }

        private void OnFocus( object sender , ExecutedRoutedEventArgs e )
        {
            var source = e.Parameter as UIElement;

            source?.Focus();
        }

        private static void OnStretchImageChanged( DependencyObject sender , DependencyPropertyChangedEventArgs e )
        {
			var window = sender as MainWindow;

            if ( window != null )
            {
                window._mediaPlayer.StretchImage = (Stretch) e.NewValue;
            }
        }

        private static void OnInfoVisibilityChanged( DependencyObject sender , DependencyPropertyChangedEventArgs e )
        {
			var window = sender as MainWindow;

            if ( window != null )
            {
                window._mediaPlayer.SourceVisibility = (Visibility) e.NewValue;
            }
        }

        private static void OnStatisticsVisibilityChanged( DependencyObject sender , DependencyPropertyChangedEventArgs e )
        {
			var window = sender as MainWindow;

            if ( window != null )
            {
                window._mediaPlayer.Statistics.Visibility = (Visibility) e.NewValue;
            }
        }
	}
}
