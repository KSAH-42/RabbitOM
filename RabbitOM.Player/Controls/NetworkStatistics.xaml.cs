using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace RabbitOM.Player.Controls
{
	public partial class NetworkStatistics : UserControl
    {
		public static readonly RoutedCommand StartMonitoringCommand = new RoutedCommand();
		public static readonly RoutedCommand StopMonitoringCommand = new RoutedCommand();




		private readonly DispatcherTimer _timer = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds( 1000 ) };





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

		public static readonly DependencyProperty FrameHeightProperty
			= DependencyProperty.Register(
				"FrameHeight", typeof(long) ,
					typeof(NetworkStatistics) );

		public static readonly DependencyProperty FrameWidthProperty
			= DependencyProperty.Register(
				"FrameWidth", typeof(long) ,
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

		public long FrameHeight
		{
			get => (long) GetValue( FrameHeightProperty );
			set => SetValue( FrameHeightProperty , value );
		}

		public long FrameWidth
		{
			get => (long) GetValue( FrameWidthProperty );
			set => SetValue( FrameWidthProperty , value );
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
			if ( DataSource == null || _timer.IsEnabled )
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
			Update();
		}

		public void Update()
		{
			var source = DataSource;

			if ( source == null )
			{
				return;
			}

			Codec = source.GetCodec();
			ConnectionStatus = source.GetConnectionStatus();
			BytesReceivedPerSecond = source.GetBytesReceivedPerSecond();
			PacketReceivedPerSecond = source.GetPacketReceivedPerSecond();
			FrameCountPerSecond = source.GetFrameCountPerSecond();
			FrameHeight = source.GetFrameHeight();
			FrameWidth = source.GetFrameWidth();
		}






        private void OnUserControlLoaded( object sender , RoutedEventArgs e )
        {
            _timer.Tick += OnTimerTick;
        }

        private void OnUserControlUnloaded( object sender , RoutedEventArgs e )
        {
			_timer.Tick -= OnTimerTick;
        }

		private void OnTimerTick( object sender , System.EventArgs e )
        {
            Update();
        }
    }
}
