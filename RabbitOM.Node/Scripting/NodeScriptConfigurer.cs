using System;
using System.Collections.Generic;

namespace RabbitOM.Node.Scripting
{
	public sealed class NodeScriptConfigurer
	{
		private readonly NodeScript _nodeScript;

		public NodeScriptConfigurer( NodeScript nodeScript )
		{
			_nodeScript = nodeScript ?? throw new ArgumentNullException( nameof( nodeScript ) );
		}

		public void ConfigureProperty( string name, string value )
		{
			if ( string.IsNullOrWhiteSpace( name ) )
			{
				throw new ArgumentNullException( nameof( name ) );
			}

			var property = _nodeScript.GetType().GetProperty( name );

			if ( property == null )
			{
				throw new InvalidOperationException( "property not found" );
			}

			if ( property.Name != name )
			{
				throw new InvalidOperationException( "" );
			}

			var converters = new Dictionary<Type,Action>();

			converters[ typeof( bool   ) ] = () => property.SetValue( _nodeScript , value.ToBool() );
			converters[ typeof( char   ) ] = () => property.SetValue( _nodeScript , value.ToChar() );
			converters[ typeof( sbyte  ) ] = () => property.SetValue( _nodeScript , value.ToSByte() );
			converters[ typeof( byte   ) ] = () => property.SetValue( _nodeScript , value.ToByte() );
			converters[ typeof( short  ) ] = () => property.SetValue( _nodeScript , value.ToShort() );
			converters[ typeof( ushort ) ] = () => property.SetValue( _nodeScript , value.ToUShort() );
			converters[ typeof( int    ) ] = () => property.SetValue( _nodeScript , value.ToInt() );
			converters[ typeof( uint   ) ] = () => property.SetValue( _nodeScript , value.ToUInt() );
			converters[ typeof( long   ) ] = () => property.SetValue( _nodeScript , value.ToLong() );
			converters[ typeof( ulong  ) ] = () => property.SetValue( _nodeScript , value.ToULong() );
			converters[ typeof( float  ) ] = () => property.SetValue( _nodeScript , value.ToFloat() );
			converters[ typeof( double ) ] = () => property.SetValue( _nodeScript , value.ToDouble() );
			converters[ typeof( decimal) ] = () => property.SetValue( _nodeScript , value.ToDecimal() );
			converters[ typeof( DateTime ) ] = () => property.SetValue( _nodeScript , value.ToDateTime() );
			converters[ typeof( TimeSpan ) ] = () => property.SetValue( _nodeScript , value.ToTimeSpan() );
			converters[ typeof( Guid     ) ] = () => property.SetValue( _nodeScript , value.ToGuid() );

			if ( converters.TryGetValue( property.PropertyType , out var converter ) )
			{
				converter.Invoke();
			}
			else
			{
				throw new NotSupportedException( $"the property:{property.Name} and it's dataType:{property.PropertyType} on the {_nodeScript.GetType()} class is not supported" );
			}
		}
	}
}
