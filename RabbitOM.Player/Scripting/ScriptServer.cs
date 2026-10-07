using System;

namespace RabbitOM.Player.Scripting
{
	public abstract class ScriptServer : IDisposable
	{
		public abstract bool IsStarted { get; }
		public abstract void MapRouteGet( string route , Action action );
		public abstract void MapRouteGet<TDto>( string route , Action<TDto> action );
		public abstract void MapRoutePost( string route , Action action );
		public abstract void MapRoutePost<TDto>( string route , Action<TDto> action );
		public abstract void Start( string endpoint );
		public abstract void Stop();
		public abstract void Close();

		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}

		protected virtual void Dispose( bool disposing )
		{
		}
	}
}
