using System;

namespace RabbitOM.Player.Scripting
{
	public sealed class ApplicationScriptConfigurer
	{
		private readonly ApplicationScript _script;

		public ApplicationScriptConfigurer( ApplicationScript script )
		{
			_script = script ?? throw new ArgumentNullException( nameof( script ) );
		}

		public void ConfigureProperty( string name, string value )
		{
			if ( string.IsNullOrWhiteSpace( name ) )
			{
				throw new ArgumentNullException( nameof( name ) );
			}

			var property = _script.GetType().GetProperty( name );

			if ( property == null )
			{
				throw new InvalidOperationException( "property not found" );
			}

			if ( property.Name != name )
			{
				throw new InvalidOperationException( "the property name is not the same" );
			}

			var converters = new Dictionary<Type,Action>();

			converters[ typeof( bool   ) ] = () => property.SetValue( _script , value.ToBool() );
			converters[ typeof( char   ) ] = () => property.SetValue( _script , value.ToChar() );
			converters[ typeof( sbyte  ) ] = () => property.SetValue( _script , value.ToSByte() );
			converters[ typeof( byte   ) ] = () => property.SetValue( _script , value.ToByte() );
			converters[ typeof( short  ) ] = () => property.SetValue( _script , value.ToShort() );
			converters[ typeof( ushort ) ] = () => property.SetValue( _script , value.ToUShort() );
			converters[ typeof( int    ) ] = () => property.SetValue( _script , value.ToInt() );
			converters[ typeof( uint   ) ] = () => property.SetValue( _script , value.ToUInt() );
			converters[ typeof( long   ) ] = () => property.SetValue( _script , value.ToLong() );
			converters[ typeof( ulong  ) ] = () => property.SetValue( _script , value.ToULong() );
			converters[ typeof( float  ) ] = () => property.SetValue( _script , value.ToFloat() );
			converters[ typeof( double ) ] = () => property.SetValue( _script , value.ToDouble() );
			converters[ typeof( decimal) ] = () => property.SetValue( _script , value.ToDecimal() );
			converters[ typeof( string ) ] = () => property.SetValue( _script , value );
			converters[ typeof( DateTime ) ] = () => property.SetValue( _script , value.ToDateTime() );
			converters[ typeof( TimeSpan ) ] = () => property.SetValue( _script , value.ToTimeSpan() );
			converters[ typeof( Guid     ) ] = () => property.SetValue( _script , value.ToGuid() );

			if ( converters.TryGetValue( property.PropertyType , out var converter ) )
			{
				converter.Invoke();
			}
			else
			{
				throw new NotSupportedException( $"the property:{property.Name} and it's dataType:{property.PropertyType} on the {_script.GetType()} class is not supported" );
			}
		}
	}
}
