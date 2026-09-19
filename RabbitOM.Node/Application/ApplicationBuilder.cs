using System;
using System.IO;
using System.Linq;

namespace RabbitOM.Node.Application
{
	using RabbitOM.Node.Scripting;
	using RabbitOM.Node.Scripting.Models;

	public sealed class ApplicationBuilder
	{
		private ApplicationParameters _parameters;
		private ScriptModel _script;
		private IScriptRunner _scriptRunner;


		public ApplicationBuilder SetParameters( string[] parameters )
		{
			if ( _parameters != null )
			{
				throw new InvalidOperationException( "the parameters has been already set" );
			}

			var applicationParameters = ApplicationParameters.Parse( parameters );
			applicationParameters.Validate();
			_parameters = applicationParameters;

			return this;
		}

		public ApplicationBuilder LoadScript()
		{
			if ( _parameters == null )
			{
				throw new InvalidOperationException( "Parameters must be set be call this method" );
			}

			if ( _script != null )
			{
				throw new InvalidOperationException( "The script is already loaded" );
			}

			if ( string.IsNullOrWhiteSpace( _parameters.ScriptWorkflow ) )
			{
				return this;
			}

			var content = File.Exists( _parameters.ScriptWorkflow ) ? File.ReadAllText( _parameters.ScriptWorkflow ) : _parameters.ScriptWorkflow;

			var script = ScriptModelDeserializer.Deserialize( content );

			ScriptModelValidator.Validate( script );

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

			foreach ( var assembly in _script.Assemblies ?? Enumerable.Empty<ScriptAssemblyModel>() )
			{
				builder.Assemblies.Add( assembly.Name );
			}

			var nodeScript = builder.Build();
			var configurer = new NodeScriptConfigurer( nodeScript );

			foreach ( var property in _script.Properties ?? Enumerable.Empty<ScriptPropertyModel>() )
			{
				configurer.ConfigureProperty( property.Name , property.Value );
			}

			_scriptRunner = new NodeScriptRunner( nodeScript );

			return this;
		}

		public IApplication Build()
		{
			return new NodeApplication( _scriptRunner ?? NullNodeScriptRunner.Instance , _parameters.Uri );
		}
	}
}
