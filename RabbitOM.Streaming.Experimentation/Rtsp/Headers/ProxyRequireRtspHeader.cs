using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class ProxyRequireRtspHeader : RtspHeader 
    {   
        public const string TypeName = "Proxy-Require";

        


        private readonly HashSet<string> _requirements = new HashSet<string>();
        



        public IReadOnlyCollection<string> Requirements
        {
            get => _requirements;
        }





        public override bool TryValidate()
        {
            return _requirements.Count > 0;
        }

        public override string ToString()
        {
            return string.Join( ", " , _requirements );
        }

        public bool TryAddRequirement( string requirement )
        {
            if ( string.IsNullOrWhiteSpace( requirement ) )
            {
                return false;
            }

            return _requirements.Add( requirement );
        }

        public void AddRequirement( string requirement )
        {
            if ( string.IsNullOrWhiteSpace( requirement ) )
            {
                throw new ArgumentException( nameof( requirement ) );
            }

            if ( ! _requirements.Add( requirement ) )
            {
                throw new ArgumentException( "element already added" );
            }
        }

        public void RemoveRequirement( string requirement )
        {
            _requirements.Remove( requirement );
        }

        public void RemoveRequirements()
        {
            _requirements.Clear();
        }





        public static bool TryParse( string value , out ProxyRequireRtspHeader result )
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

            result = new ProxyRequireRtspHeader();

            foreach( var token in tokens )
            {
                result.TryAddRequirement( token );
            }

            return true;
        }
    }
}
