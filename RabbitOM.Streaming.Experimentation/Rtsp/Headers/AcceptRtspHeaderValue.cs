using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class AcceptRtspHeaderValue : RtspHeaderValue
    {
        public static readonly string TypeName = "Accept";


        private readonly Dictionary<string,StringWithQualityRtspHeaderValue> _mimes = new Dictionary<string,StringWithQualityRtspHeaderValue>( StringComparer.OrdinalIgnoreCase );
        
        
        public IReadOnlyCollection<StringWithQualityRtspHeaderValue> Mimes
        { 
            get => _mimes.Values; 
        }


        public static bool TryParse( string input , out AcceptRtspHeaderValue result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , "," , out string[] tokens ) )
            {
                var header = new AcceptRtspHeaderValue();

                foreach ( var token in tokens )
                {
                    if ( StringWithQualityRtspHeaderValue.TryParse( token , out var mime ) )
                    {
                        header.AddMime( mime );
                    }
                }
            
                if ( header.Mimes.Count > 0 )
                {
                    result = header;
                }
            }

            return result != null;
        }
        
        public bool AddMime( StringWithQualityRtspHeaderValue mime )
        {
            return AddMime( mime , RtspHeaderProtocolValidator.IsValidMime );
        }

        public bool AddMime( StringWithQualityRtspHeaderValue mime , Func<string,bool> validator )
        {
            if ( StringWithQualityRtspHeaderValue.IsNullOrEmpty( mime ) )
            {
                return false;
            }

            if ( validator == null || ! validator.Invoke( mime.Value ) )
            {
                return false;
            }

            if ( _mimes.ContainsKey( mime.Value ) )
            {
                return false;
            }

            _mimes[ mime.Value ] = mime;

            return true;
        }

        public bool RemoveMime( StringWithQualityRtspHeaderValue mime )
        {
            if ( StringWithQualityRtspHeaderValue.IsNullOrEmpty( mime ) || ! _mimes.ContainsValue( mime ) )
            {
                return false;
            }

            return _mimes.Remove( mime.Value );
        }

        public bool RemoveMimeBy( Func<StringWithQualityRtspHeaderValue,bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            return _mimes.Remove( _mimes.Values.FirstOrDefault( predicate )?.Value ?? string.Empty );
        }

        public void RemoveMimes()
        {
            _mimes.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _mimes.Values );
        }
    }
}
