using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    internal sealed class RtspHeaderRegistryBucket
    {
        private object _valueObject = null;
        
        private readonly List<object> _values = new List<object>();

        
        
        public bool IsEmpty 
        { 
            get
            {
                return _values.Count == 0;
            }
        }

        public IList<object> Values
        {
            get
            {
                return _values;
            }
        }

        public object ValueObject
        {
            get
            {
                return _valueObject;
            }

            set
            {
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
    }
}
