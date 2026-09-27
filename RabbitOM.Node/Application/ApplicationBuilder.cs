using System;
using System.IO;
using System.Linq;

namespace RabbitOM.Node.Application
{
	using RabbitOM.Node.Logging;
	using RabbitOM.Node.Scripting;
	using RabbitOM.Node.Scripting.Models;

	public sealed class ApplicationBuilder
	{
		private IScriptHost _scriptHost;

		private ScriptModel _script;

		private readonly ApplicationSettings _settings;





		public ApplicationBuilder( ApplicationSettings settings )
		{
			_settings = settings ?? throw new ArgumentNullException( nameof( settings ) );
		}





		public ApplicationBuilder LoadScript()
		{
			if ( _script != null )
			{
				throw new InvalidOperationException( "The script is already loaded" );
			}

			if ( _settings == null )
			{
				throw new InvalidOperationException( "No settings has been defined" );
			}

			if ( string.IsNullOrWhiteSpace( _settings.Script ) )
			{
				throw new InvalidOperationException( "No script has been defined" );
			}

			var content = File.Exists( _settings.Script ) ? File.ReadAllText( _settings.Script ) : _settings.Script;

			var script = Deserializer.Deserialize( content );

			Validator.Validate( script );

			_script = script;

			return this;
		}

		public ApplicationBuilder SetupScriptHost()
		{
			if ( _script == null )
			{
				throw new InvalidOperationException( "The script is already loaded" );
			}

			if ( _scriptHost != null )
			{
				throw new InvalidOperationException( "The runner is alread setup" );
			}

			var nodeScript = new NodeScriptBuilder( _script ).LoadReferences().Build();
			var configurer = new NodeScriptConfigurer( nodeScript );

			foreach ( var property in _script.Properties ?? Enumerable.Empty<PropertyModel>() )
			{
				configurer.ConfigureProperty( property.Name , property.Value );
			}

			_scriptHost = new NodeScriptHost( nodeScript );

			return this;
		}

		public IApplication Build()
		{
			return new NodeApplication( new ConsoleLogger() , _scriptHost ?? NullNodeScriptHost.Instance , _settings );
		}
	}
}
