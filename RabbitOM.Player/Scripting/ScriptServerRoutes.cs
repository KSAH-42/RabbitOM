using System;
using System.Collections.Concurrent;

namespace RabbitOM.Player.Scripting
{
	public sealed class ScriptServerRoutes
	{
        private readonly ConcurrentDictionary<string, Delegate> _postRoutes = new ();

        public IReadOnlyDictionary<string,Delegate> PostRoutes { get => _postRoutes; }

        public void AddPostRoute( string pattern , Delegate handler )
        {
            if ( string.IsNullOrWhiteSpace( pattern ) )
            {
                throw new ArgumentNullException( nameof( pattern ) );
            }

            _postRoutes[ pattern ] = handler ?? throw new ArgumentNullException( nameof( handler ) );
        }

        public void RemoveAllRoutes()
        {
            _postRoutes.Clear();
        }
	}
}
