using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace RabbitOM.Player.Controls
{
	public partial class NetworkStatistics : UserControl
    {
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

		public static readonly DependencyProperty TransportProperty
			= DependencyProperty.Register(
				"Transport", typeof(string) ,
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

		public static readonly DependencyProperty PacketsLostCountPerSecondProperty
			= DependencyProperty.Register(
				"PacketsLostCount", typeof(long) ,
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

		public string Transport
		{
			get => (string) GetValue( TransportProperty );
			set => SetValue( TransportProperty , value );
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








		public void StartCollect()
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

			var datasource = DataSource;

			if ( datasource != null )
			{
				datasource.Clear();
				Update();
			}
		}

		public void Update()
		{
			var source = DataSource;

			if ( source == null )
			{
				return;
			}

			Codec = source.GetCodec();
			Transport = source.GetTransport();
			ConnectionStatus = source.GetConnectionStatus();
			BytesReceivedPerSecond = source.GetBytesReceivedPerSecond();
			PacketReceivedPerSecond = source.GetPacketReceivedPerSecond();
			FrameCountPerSecond = source.GetFrameCountPerSecond();
			FrameHeight = source.GetFrameHeight();
			FrameWidth = source.GetFrameWidth();
			PacketsLostCount = source.GetPacketsLostCount();
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
