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
		private IScriptRunner _scriptRunner;

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

		public ApplicationBuilder SetupRunner()
		{
			if ( _script == null )
			{
				throw new InvalidOperationException( "The script is already loaded" );
			}

			if ( _scriptRunner != null )
			{
				throw new InvalidOperationException( "The runner is alread setup" );
			}

			var builder = new NodeScriptBuilder()
			{
				Language = _script.Language ,
				Code = _script.Code ,
			};

			foreach ( var assembly in _script.References ?? Enumerable.Empty<ReferenceModel>() )
			{
				builder.References.Add( assembly.Name );
			}

			var nodeScript = builder.Build();
			var configurer = new NodeScriptConfigurer( nodeScript );

			foreach ( var property in _script.Properties ?? Enumerable.Empty<PropertyModel>() )
			{
				configurer.ConfigureProperty( property.Name , property.Value );
			}

			_scriptRunner = new NodeScriptRunner( nodeScript );

			return this;
		}

		public IApplication Build()
		{
			return new NodeApplication( new ConsoleLogger() , _scriptRunner ?? NullNodeScriptRunner.Instance , _settings );
		}
	}
}
