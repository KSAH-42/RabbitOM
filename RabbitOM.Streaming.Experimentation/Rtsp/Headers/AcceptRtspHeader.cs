using System;
using System.Collections.Generic;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public sealed class AcceptRtspHeader
    {
        public static readonly string TypeName = "Accept";





        private readonly Dictionary<string,WeightedString> _mimes = new Dictionary<string,WeightedString>( StringComparer.OrdinalIgnoreCase );
        

        
        public IReadOnlyCollection<WeightedString> Mimes { get => _mimes.Values; }

        

        
        
        public bool AddMime( WeightedString mime )
        {
            if ( WeightedString.IsNullOrEmpty( mime ) )
            {
                return false;
            }

            if ( ! SupportedTypes.Formats.Contains( mime.Value ) )
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

        public bool RemoveMime( WeightedString mime )
        {
            if ( WeightedString.IsNullOrEmpty( mime ) )
            {
                return false;
            }

            if ( ! _mimes.ContainsValue( mime ) )
            {
                return false;
            }

            return _mimes.Remove( mime.Value );
        }

        public bool RemoveMimeBy( Func<WeightedString,bool> predicate )
        {
            if ( predicate == null )
            {
                throw new ArgumentNullException( nameof( predicate ) );
            }

            var language = _mimes.Values.FirstOrDefault( predicate );

            if ( language == null )
            {
                return false;
            }

            return _mimes.Remove( language.Value );
        }

        public void ClearMimes()
        {
            _mimes.Clear();
        }

        public override string ToString()
        {
            return string.Join( ", " , _mimes.Values );
        }
        



        
        
        public static bool TryParse( string input , out AcceptRtspHeader result )
        {
            result = null;

            if ( RtspHeaderParser.TryParse( input , "," , out var tokens ) )
            {
                var header = new AcceptRtspHeader();

                foreach ( var token in tokens )
                {
                    if ( WeightedString.TryParse( token , out var mime ) )
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
    }
}
