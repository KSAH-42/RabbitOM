using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RabbitOM.Streaming.RtspV2.Headers
{
    internal struct RtspHeaderRegistryBucket
    {
        private readonly IList<object> _values;

        private object _valueObject;





        public RtspHeaderRegistryBucket( IList<object> values )
        {
            if ( values == null )
            {
                throw new ArgumentNullException( nameof( values ) );
            }

            _values = values;
            _valueObject = null;
        }






        public static RtspHeaderRegistryBucket NewBucket()
        {
            return new RtspHeaderRegistryBucket( new List<object>() );
        }

        public static RtspHeaderRegistryBucket NewBucket( object valueObject )
        {
            return new RtspHeaderRegistryBucket( new List<object>() ) { ValueObject = valueObject };
        }






        public object ValueObject
        {
            get
            {
                if ( _values.Count == 0 )
                {
                    _valueObject = null;
                }

                return _valueObject;
            }

            set
            {
                Debug.Assert( _values != null , "it seems that the parameterized constructor has not been called, and not allowed in this case" );

                if ( _valueObject != null )
                {
                    _values.Remove( _valueObject );
                }

                if ( value != null )
                {
                    _valueObject = value;
                    _values.Add( value );
                }
            }
        }

        public IList<object> Values
        {
            get
            {
                Debug.Assert( _values != null , "it seems that the parameterized constructor has not been called, and not allowed in this case" );

                return _values;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return _values.Count == 0;
            }
        }
    }
}
