using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Net.RtspV2.Headers
{
    public class AllowRtspHeader : RtspHeader 
    {
        public const string TypeName = "Allow";




        private readonly HashSet<RtspMethod> _methods = new HashSet<RtspMethod>();




        public IReadOnlyCollection<RtspMethod> Methods
        {
            get => _methods;
        }



        public bool TryAddMethod( RtspMethod method )
        {
            if ( method == null )
            {
                return false;
            }

            return _methods.Add( method );
        }

        public void RemoveMethod( RtspMethod method )
        {
            _methods.Remove( method );
        }

        public void RemoveMethods()
        {
            _methods.Clear();
        }
        
        public override bool TryValidate()
        {
            return _methods.Count > 0;
        }
        public override string ToString()
        {
            return string.Join( ", " , _methods );
        }
        




        public static bool TryParse( string value , out AllowRtspHeader result )
        {
            result = null;

            if ( string.IsNullOrWhiteSpace( value ) )
            {
                return false;
            }

            // Allow: DESCRIBE, SETUP, TEARDOWN, PLAY, PAUSE, GET_PARAMETER 

            var tokens = value.Split( new char[] { ',' } , StringSplitOptions.RemoveEmptyEntries );

            if ( tokens.Length <= 0 )
            {
                return false;
            }

            var header = new AllowRtspHeader();

            foreach( var token in tokens )
            {
                if ( RtspMethod.TryParse( token , out var method ) )
                {
                    header.TryAddMethod( method );
                }
            }

            return ( result = header.Methods.Count > 0 ? header : null ) != null ;
        }
    }
}
