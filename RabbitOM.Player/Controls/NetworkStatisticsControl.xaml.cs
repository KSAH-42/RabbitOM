using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace RabbitOM.Player.Controls
{
	public partial class NetworkStatisticsControl : UserControl
    {
		private readonly DispatcherTimer _timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds( 1 ) };






		public NetworkStatisticsControl()
        {
            InitializeComponent();
        }







		public static readonly DependencyProperty DataSourceProperty =
			DependencyProperty.Register(
				nameof(DataSource),
					typeof(IStatisticsDataSource),
						typeof(NetworkStatisticsControl));

		public static readonly DependencyProperty ConnectionStatusProperty =
			DependencyProperty.Register(
				nameof(ConnectionStatus),
					typeof(bool),
						typeof(NetworkStatisticsControl));

		public static readonly DependencyProperty CodecProperty =
			DependencyProperty.Register(
				nameof(Codec),
					typeof(string),
						typeof(NetworkStatisticsControl));

		public static readonly DependencyProperty TransportProperty =
			DependencyProperty.Register(
				nameof(Transport),
					typeof(string),
						typeof(NetworkStatisticsControl));

		public static readonly DependencyProperty ClockProperty =
			DependencyProperty.Register(
				nameof(Clock),
					typeof(long),
						typeof(NetworkStatisticsControl));

		public static readonly DependencyProperty FrameHeightProperty =
			DependencyProperty.Register(
				nameof(FrameHeight),
					typeof(long),
						typeof(NetworkStatisticsControl));

		public static readonly DependencyProperty FrameWidthProperty =
			DependencyProperty.Register(
				nameof(FrameWidth),
					typeof(long),
						typeof(NetworkStatisticsControl));

		public static readonly DependencyProperty BytesReceivedPerSecondProperty =
			DependencyProperty.Register(
				nameof(BytesReceivedPerSecond),
					typeof(long),
						typeof(NetworkStatisticsControl));

		public static readonly DependencyProperty PacketReceivedPerSecondProperty =
			DependencyProperty.Register(
				nameof(PacketReceivedPerSecond),
					typeof(long),
						typeof(NetworkStatisticsControl));

		public static readonly DependencyProperty FrameCountPerSecondProperty =
			DependencyProperty.Register(
				nameof(FrameCountPerSecond),
					typeof(long),
						typeof(NetworkStatisticsControl));

		public static readonly DependencyProperty PacketsLostCountPerSecondProperty =
			DependencyProperty.Register(
				nameof(PacketsLostCount),
					typeof(long),
						typeof(NetworkStatisticsControl));

		public static readonly DependencyProperty MaxFrameCountPerSecondProperty =
			DependencyProperty.Register(
				nameof(MaxFrameCountPerSecond),
					typeof(long),
						typeof(NetworkStatisticsControl));

		public static readonly DependencyProperty MaxBytesReceivedPerSecondProperty =
			DependencyProperty.Register(
				nameof(MaxBytesReceivedPerSecond),
					typeof(long),
					typeof(NetworkStatisticsControl));

		public static readonly DependencyProperty MaxPacketReceivedPerSecondProperty =
			DependencyProperty.Register(
				nameof(MaxPacketReceivedPerSecond),
					typeof(long),
						typeof(NetworkStatisticsControl));








		public IStatisticsDataSource DataSource
		{
			get => (IStatisticsDataSource) GetValue( DataSourceProperty );
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

		public string Transport
		{
			get => (string) GetValue( TransportProperty );
			set => SetValue( TransportProperty , value );
		}

		public long Clock
		{
			get => (long) GetValue( ClockProperty );
			set => SetValue( ClockProperty , value );
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

		public long PacketsLostCount
		{
			get => (long) GetValue( PacketsLostCountPerSecondProperty );
			set => SetValue( PacketsLostCountPerSecondProperty , value );
		}

		public long MaxFrameCountPerSecond
		{
			get => (long) GetValue( MaxFrameCountPerSecondProperty );
			set => SetValue( MaxFrameCountPerSecondProperty , value );
		}

		public long MaxBytesReceivedPerSecond
		{
			get => (long) GetValue( MaxBytesReceivedPerSecondProperty );
			set => SetValue( MaxBytesReceivedPerSecondProperty , value );
		}

		public long MaxPacketReceivedPerSecond
		{
			get => (long) GetValue( MaxPacketReceivedPerSecondProperty );
			set => SetValue( MaxPacketReceivedPerSecondProperty , value );
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
			_timer.Stop();

			Update();
		}

		private void Update()
		{
			var source = DataSource ?? NullStatisticsDataSource.Value;

			Codec = source.GetCodec();
			Clock = source.GetClock();
			Transport = source.GetTransport();
			ConnectionStatus = source.GetConnectionStatus();
			BytesReceivedPerSecond = source.GetBytesReceivedPerSecond();
			PacketReceivedPerSecond = source.GetPacketReceivedPerSecond();
			FrameCountPerSecond = source.GetFrameCountPerSecond();
			FrameHeight = source.GetFrameHeight();
			FrameWidth = source.GetFrameWidth();
			PacketsLostCount = source.GetPacketsLostCount();
			MaxFrameCountPerSecond = source.GetMaxFrameCountPerSecond();
			MaxBytesReceivedPerSecond = source.GetMaxBytesReceivedPerSecond();
			MaxPacketReceivedPerSecond = source.GetMaxPacketReceivedPerSecond();
		}










        private void OnUserControlLoaded( object sender , RoutedEventArgs e )
        {
            _timer.Tick += OnTimerTick;
        }

        private void OnUserControlUnloaded( object sender , RoutedEventArgs e )
        {
			_timer.Stop();
			_timer.Tick -= OnTimerTick;
        }

		private void OnTimerTick( object sender , System.EventArgs e )
        {
            Update();
        }
    }
}
