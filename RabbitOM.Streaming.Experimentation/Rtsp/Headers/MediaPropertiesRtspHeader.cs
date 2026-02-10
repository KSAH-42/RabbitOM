using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class MediaPropertiesRtspHeader : RtspHeader 
    {
        public const string TypeName = "Media-Properties";





        private readonly HashSet<string> _properties = new HashSet<string>();
        




        public IReadOnlyCollection<string> Properties
        {
            get => _properties;
        }




        public override bool TryValidate()
        {
            return _properties.Count > 0;
        }

        public override string ToString()
        {
            return string.Join( ", " , _properties );
        }

        public bool TryAddProperty( string property )
        {
            if ( string.IsNullOrWhiteSpace( property ) )
            {
                return false;
            }

            _properties.Add( property );

            return true;
        }

        public void AddProperty( string property )
        {
            if ( string.IsNullOrWhiteSpace( property ) )
            {
                throw new ArgumentException( nameof( property ) );
            }

            _properties.Add( property );
        }

        public void RemoveProperty( string property )
        {
            _properties.Remove( property );
        }

        public void RemoveProperties()
        {
            _properties.Clear();
        }
        




        public static bool TryParse( string value , out MediaPropertiesRtspHeader result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            var tokens = value.Split( new char[] { ',' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens.Length <= 0 )
            {
                return false;
            }

            result = new MediaPropertiesRtspHeader();

            foreach ( var token in tokens )
            {
                result.TryAddProperty( token );
            }

            return true;
        }
    }
}
