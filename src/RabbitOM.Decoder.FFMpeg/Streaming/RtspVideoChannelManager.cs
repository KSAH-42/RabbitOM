namespace RabbitOM.Decoder.FFMpeg.Streaming
{
	// TODO: merge add with start method
	// TODO: merge remove with stop method
	// TODO: merge clear with stopAll method

	// this class is really a related as a service type check moving this class in the
	// service namespace

	public sealed class RtspVideoChannelManager	 : IDisposable
	{
		private readonly object _lock = new object();
		private readonly List<RtspVideoChannel> _channels = new List<RtspVideoChannel>();




		public RtspVideoChannel? GetChannelAt( int index )
		{
			lock ( _lock )
			{
				if ( index < 0 || index >= _channels.Count )
				{
					throw new ArgumentException( nameof( index ) );
				}

				return EnsureNotDisposed( _channels[ index ] );
			}
		}

		public void AddChannel( RtspVideoChannel channel )
		{
			EnsureNotDisposed( channel );

			lock( _lock )
			{
				if ( _channels.Contains( channel ) ) // O(n) we tolerate
				{
					throw new ArgumentException( nameof( channel ) );
				}

				_channels.Add( channel );
			}
		}

		public bool RemoveChannel( RtspVideoChannel channel )
		{
			if ( channel == null )
			{
				return false;
			}

			lock( _lock )
			{
				if ( _channels.Remove( channel ) )
				{
					channel.Dispose();

					return true;
				}

				return false;
			}
		}

		public bool RemoveChannelAt( int index )
		{
			lock( _lock )
			{
				if ( index < 0 || index >= _channels.Count )
				{
					return false;
				}

				var channel = _channels[ index ];

				_channels.RemoveAt( index );

				channel?.Dispose();

				return true;
			}
		}

		public void RemoveChannels()
		{
			lock ( _lock )
			{
				_channels.ForEach( channel => channel.Dispose() );
				_channels.Clear();
			}
		}

		public void Dispose()
		{
			RemoveChannels();
		}






		public bool StartChannel( int index , RtspVideoChannelConfiguration configuration )
		{
			if ( configuration == null )
			{
				throw new ArgumentNullException( nameof( configuration ) );
			}

			lock ( _lock )
			{
				if ( index < 0 || index >= _channels.Count )
				{
					throw new ArgumentException( nameof( index ) );
				}

				var channel = EnsureNotDisposed( _channels[index] );

				return channel.Start( configuration );
			}
		}

		public void StopChannelAt( int index )
		{
			lock ( _lock )
			{
				if ( index < 0 || index >= _channels.Count )
				{
					throw new ArgumentException( nameof( index ) );
				}

				var channel = _channels[ index ] ;

				channel?.Stop();
			}
		}

		public void StopChannels()
		{
			lock ( _lock )
			{
				_channels.ForEach( channel => channel.Stop() );
			}
		}



		private static RtspVideoChannel EnsureNotDisposed( RtspVideoChannel channel )
		{
			if ( channel == null )
			{
				throw new ArgumentNullException( nameof( channel ) );
			}

			if ( channel.IsDisposed )
			{
				throw new InvalidCastException( "the channel is already disposed" );
			}

			return channel;
		}
	}
}
