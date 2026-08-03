using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace RabbitOM.Player.Controls
{
	using RabbitOM.Player.Controls;

    public partial class NetworkStatistics : UserControl
    {
		public static readonly RoutedCommand StartMonitoringCommand = new RoutedCommand();
		public static readonly RoutedCommand StopMonitoringCommand = new RoutedCommand();




		private readonly DispatcherTimer _timer = new DispatcherTimer();





		public NetworkStatistics()
        {
            InitializeComponent();
        }






		public static readonly DependencyProperty DataSourceProperty
			= DependencyProperty.Register(
				"DataSource", typeof(IDataSource) ,
					typeof(NetworkStatistics) );


		public static readonly DependencyProperty ConnectionStatusProperty
			= DependencyProperty.Register(
				"ConnectionStatus", typeof(bool) ,
					typeof(NetworkStatistics) );

		public static readonly DependencyProperty CodecProperty
			= DependencyProperty.Register(
				"Codec", typeof(string) ,
					typeof(NetworkStatistics) );

		public static readonly DependencyProperty ConnectionsCountProperty
			= DependencyProperty.Register(
				"ConnectionsCount", typeof(long) ,
					typeof(NetworkStatistics) );

		public static readonly DependencyProperty BytesReceivedPerSecondProperty
			= DependencyProperty.Register(
				"BytesReceivedPerSecond", typeof(long) ,
					typeof(NetworkStatistics) );

		public static readonly DependencyProperty PacketReceivedPerSecondProperty
			= DependencyProperty.Register(
				"PacketReceivedPerSecond", typeof(long) ,
					typeof(NetworkStatistics) );

		public static readonly DependencyProperty FrameCountPerSecondProperty
			= DependencyProperty.Register(
				"FrameCountPerSecond", typeof(long) ,
					typeof(NetworkStatistics) );






		public IDataSource DataSource
		{
			get => (IDataSource) GetValue( DataSourceProperty );
			set => SetValue( DataSourceProperty , value );
		}

		public bool ConnectionStatus
		{
			get => (bool) GetValue( ConnectionStatusProperty );
			set => SetValue( ConnectionStatusProperty , value );
		}

		public string Codec
		{
			get => (string) GetValue( CodecProperty );
			set => SetValue( CodecProperty , value );
		}

		public long ConnectionsCount
		{
			get => (long) GetValue( ConnectionsCountProperty );
			set => SetValue( ConnectionsCountProperty , value );
		}

		public long BytesReceivedPerSecond
		{
			get => (long) GetValue( BytesReceivedPerSecondProperty );
			set => SetValue( BytesReceivedPerSecondProperty , value );
		}

		public long PacketReceivedPerSecond
		{
			get => (long) GetValue( PacketReceivedPerSecondProperty );
			set => SetValue( PacketReceivedPerSecondProperty , value );
		}

		public long FrameCountPerSecond
		{
			get => (long) GetValue( FrameCountPerSecondProperty );
			set => SetValue( FrameCountPerSecondProperty , value );
		}





		public void StartMonitoring()
		{
			if ( DataSource == null || ! _timer.IsEnabled )
			{
				return;
			}

			_timer.Start();
		}

		public void StopMonitoring()
		{
			if ( _timer.IsEnabled )
			{
				_timer.Stop();
			}

			DataSource?.Clear();
		}







        private void OnUserControlLoaded( object sender , RoutedEventArgs e )
        {
            _timer.Tick += OnTimerTick;
        }

        private void OnUserControlUnloaded( object sender , RoutedEventArgs e )
        {
			_timer.Tick -= OnTimerTick;
        }

		private void OnCanStartMonitoring( object sender , CanExecuteRoutedEventArgs e )
        {
			e.CanExecute = ! _timer.IsEnabled && DataSource != null;
        }

		private void OnStartMonitoring( object sender , ExecutedRoutedEventArgs e )
        {
			StartMonitoring();
        }

		private void OnCanStopMonitoring( object sender , CanExecuteRoutedEventArgs e )
        {
			e.CanExecute = _timer.IsEnabled;
        }

		private void OnStopMonitoring( object sender , ExecutedRoutedEventArgs e )
        {
			StopMonitoring();
        }

		private void OnTimerTick( object sender , System.EventArgs e )
        {
            var source = DataSource;

			if ( source == null )
			{
				return;
			}

			ConnectionStatus = source.ConnectionStatus;
			ConnectionsCount = source.ConnectionsCount;
			BytesReceivedPerSecond = source.BytesReceivedPerSecond;
			PacketReceivedPerSecond = source.PacketReceivedPerSecond;
			FrameCountPerSecond = source.FrameCountPerSecond;
        }
    }
}
