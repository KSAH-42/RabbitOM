using System;

namespace RabbitOM.Player.Scripting
{
	using RabbitOM.Player.Scripting.Messages;

	public abstract class PlayerScript : IDisposable
	{
		public PlayerScriptApplication Application { get; internal set; }

		public virtual void Setup() { }

		public abstract void Handle( Message message );

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
