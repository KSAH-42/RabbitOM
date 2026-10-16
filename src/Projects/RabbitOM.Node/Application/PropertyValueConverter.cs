using System;

namespace RabbitOM.Node.Application
{
    public static class PropertyValueConverter
    {
        public static object Convert( Type type , string value )
        {
            if ( type == typeof( string ) )
            {
                return value ?? string.Empty;
            }
            else if ( type == typeof( bool ) )
            {
                return bool.Parse( value );
            }
            else if ( type == typeof( int ) )
            {
                return int.Parse( value );
            }
            else if ( type == typeof( TimeSpan ) )
            {
                return TimeSpan.Parse( value );
            }
            else
            {
                throw new NotSupportedException( "DataType not supported" );
            }
        }
    }
}