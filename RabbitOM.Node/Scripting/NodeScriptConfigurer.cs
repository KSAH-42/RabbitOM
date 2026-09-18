using System;

namespace RabbitOM.Node.Scripting
{
	public sealed class NodeScriptConfigurer
	{
		private readonly NodeScript _nodeScript;

		public NodeScriptConfigurer( NodeScript nodeScript )
		{
			_nodeScript = nodeScript ?? throw new ArgumentNullException( nameof( nodeScript ) );
		}

		// TODO: maybe refactor this method by removing the StringExtensions and create a property_converter class (not static) that used dictionary where K=type and V=Func , inject to the ctor the source object, add method to add convert func, etc.. and create static factory method that populate the dictionary

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

			if ( property.PropertyType == typeof( bool ) )
			{
				property.SetValue( _nodeScript , value.ToBool() );
			}
			else if ( property.PropertyType == typeof( char ) )
			{
				property.SetValue( _nodeScript , value.ToChar() );
			}
			else if ( property.PropertyType == typeof( sbyte ) )
			{
				property.SetValue( _nodeScript , value.ToSByte() );
			}
			else if ( property.PropertyType == typeof( byte ) )
			{
				property.SetValue( _nodeScript , value.ToByte() );
			}
			else if ( property.PropertyType == typeof( short ) )
			{
				property.SetValue( _nodeScript , value.ToShort() );
			}
			else if ( property.PropertyType == typeof( ushort ) )
			{
				property.SetValue( _nodeScript , value.ToUShort() );
			}
			else if ( property.PropertyType == typeof( int ) )
			{
				property.SetValue( _nodeScript , value.ToInt() );
			}
			else if ( property.PropertyType == typeof( uint ) )
			{
				property.SetValue( _nodeScript , value.ToUInt() );
			}
			else if ( property.PropertyType == typeof( long ) )
			{
				property.SetValue( _nodeScript , value.ToLong() );
			}
			else if ( property.PropertyType == typeof( ulong ) )
			{
				property.SetValue( _nodeScript , value.ToULong() );
			}
			else if ( property.PropertyType == typeof( double ) )
			{
				property.SetValue( _nodeScript , value.ToDouble() );
			}
			else if ( property.PropertyType == typeof( float ) )
			{
				property.SetValue( _nodeScript , value.ToFloat() );
			}
			else if ( property.PropertyType == typeof( decimal ) )
			{
				property.SetValue( _nodeScript , value.ToDecimal() );
			}
			else if ( property.PropertyType == typeof( DateTime ) )
			{
				property.SetValue( _nodeScript , value.ToDateTime() );
			}
			else if ( property.PropertyType == typeof( TimeSpan ) )
			{
				property.SetValue( _nodeScript , value.ToTimeSpan() );
			}
			else if ( property.PropertyType == typeof( Guid ) )
			{
				property.SetValue( _nodeScript , value.ToGuid() );
			}
			else
			{
				throw new NotSupportedException( $"the property:{property.Name} and it's dataType:{property.PropertyType} on the {_nodeScript.GetType()} class is not supported" );
			}
		}
	}
}
