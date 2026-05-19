using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
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
                Debug.Assert( _values != null , "in net48, the default constructor is mandatory and it must not be called" );

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
                Debug.Assert( _values != null , "in net48, the default constructor is mandatory and it must not be called" );
                
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






        public static RtspHeaderRegistryBucket NewBucket()
        {
            return new RtspHeaderRegistryBucket( new List<object>() );
        }

        public static RtspHeaderRegistryBucket NewBucket( object valueObject )
        {
            return new RtspHeaderRegistryBucket( new List<object>() ) { ValueObject = valueObject };
        }
    }
}
