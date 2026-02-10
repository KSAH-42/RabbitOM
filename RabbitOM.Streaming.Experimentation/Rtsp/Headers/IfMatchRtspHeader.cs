using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class IfMatchRtspHeader : RtspHeader 
    {
        public const string TypeName = "If-Match";
        



        private readonly List<string> _tags = new List<string>();
        




        public IReadOnlyCollection<string> Tags
        {
            get => _tags;
        }




        public override bool TryValidate()
        {
            return _tags.Count > 0;
        }

        public override string ToString()
        {
            return string.Join( ", " , _tags );
        }
        

        public bool TryAddTag( string tag )
        {
            var text = StringRtspNormalizer.Normalize( tag );

            if ( string.IsNullOrWhiteSpace( text ) )
            {
                return false;
            }

            _tags.Add( text );

            return true;
        }

        public void RemoveTag( string tag )
        {
            _tags.Remove( StringRtspNormalizer.Normalize( tag ) );
        }

        public void RemoveTags()
        {
            _tags.Clear();
        }





        public static bool TryParse( string value , out IfMatchRtspHeader result )
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

            result = new IfMatchRtspHeader();

            foreach ( var token in tokens )
            {
                result.TryAddTag( token );
            }

            return true;
        }
    }
}
