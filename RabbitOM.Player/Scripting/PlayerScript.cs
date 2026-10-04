using System;
using System.Windows;

namespace RabbitOM.Player.Scripting
{
	public abstract class PlayerScript : IDisposable
	{
		public Window Window { get; } = App.Current.MainWindow;

		public virtual void Setup() { } // call after properties has been apply

		public void Dispose()
		{
			Dispose( true );
			GC.SuppressFinalize( this );
		}

		protected virtual void Dispose( bool disposing )
		{
		}
	}

	// merge all this feature below into indivual service class and expose an IReadonlyCollection<IService> as property in the PlayerScript class
	// or expose IServiceProvider with individual static factory class

	/*
		* node class is smart wrapper around node process where the node process don't use the rtsp client
		* transport as web api node send script
		* public IReadOnlyCollection<Node> Nodes { get; } 
		* public void AddNode( new NodeConfiguration() ) {}
		* public Node GetNodeById( string id ) 
		* public void RemoveNodeById( string id ) 
		* public void RemoveAllNodes() 
		* public void StartNodeById( string id ) 
		* public void StopNodeById( string id ) 
		* public void StopAllNodes() 
		*/

	/*
		* public ICache Cache { get; } // data persitance into a sqllite db
		*/

	/*
		* public IEventStore EventStore { get; } // local event store using sqllite db and expose a dialogbox for making queries
		*/

	/*
	 *  // check if we can used directly/only the kestrel server class instead of using the httplistener or using all the asp.net core
		* public IServer Server { get; } // for controlling remotly the application using http server
		*/

	/*
		* public IAProxy IAProxy { get; } // communicate with IA cloud agent
		*/

	/*
		* move it into the window class ?
		* public IReadOnlyCollection<Decoder or Player> Decoders { get; } 
		*/
}
