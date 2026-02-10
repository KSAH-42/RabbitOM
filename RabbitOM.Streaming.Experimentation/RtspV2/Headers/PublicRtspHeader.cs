using System;
using System.Collections.Generic;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public class PublicRtspHeader : RtspHeader 
    {
        public const string TypeName = "Public";




        private readonly HashSet<RtspMethod> _methods = new HashSet<RtspMethod>();




        public IReadOnlyCollection<RtspMethod> Methods
        {
            get => _methods;
        }



        public override bool TryValidate()
        {
            return _methods.Count > 0;
        }

        public override string ToString()
        {
            return string.Join( ", " , _methods );
        }

        public bool TryAddMethod( RtspMethod method )
        {
            if ( method == null )
            {
                return false;
            }

            return _methods.Add( method );
        }

        public void AddMethod( RtspMethod method )
        {
            if ( method == null )
            {
                throw new ArgumentNullException( nameof( method ) );
            }

            if ( ! _methods.Add( method ) )
            {
                throw new ArgumentException( "the element is already added" );
            }
        }

        public void RemoveMethod( RtspMethod method )
        {
            _methods.Remove( method );
        }

        public void RemoveAllMethods()
        {
            _methods.Clear();
        }





        public static bool TryParse( string value , out PublicRtspHeader result )
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

            var header = new PublicRtspHeader();

            foreach ( var token in tokens )
            {
                if ( RtspMethod.TryParse( token , out var method ) )
                {
                    header.TryAddMethod( method );
                }
            }

            return ( result = header.Methods.Count > 0 ? header : null ) != null;
        }
    }
}
