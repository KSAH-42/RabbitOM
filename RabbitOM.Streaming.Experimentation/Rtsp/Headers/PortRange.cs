using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public struct PortRange : IEquatable<PortRange>
    {

        public static readonly PortRange Zero = new PortRange( 0 , 0 );






        public PortRange( int minimum , int maximum )
        {
            if ( minimum < 0 || maximum < 0 || minimum > maximum )
            {
                throw new ArgumentOutOfRangeException();
            }

            Minimum = minimum;
            Maximum = maximum;
        }






        public int Minimum { get; }

        public int Maximum { get; }






        public static bool operator == ( PortRange a , PortRange b )
        {
            return Equals( a , b );
        }

        public static bool operator != ( PortRange a , PortRange b )
        {
            return ! Equals( a , b );
        }







        public static bool Equals( in PortRange a , in PortRange b )
        {
            return a.Minimum == b.Minimum && a.Maximum == b.Maximum;
        }

        public static bool TryParse( string input , out PortRange result )
        {
            result = default;

            if ( RtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , "-" , out string[] tokens ) )
            {
                if ( ! int.TryParse( tokens.ElementAtOrDefault( 0 ) , out var minimum ) )
                {
                    return false;
                }

                if ( ! int.TryParse( tokens.ElementAtOrDefault( 1 ) , out var maximum ) )
                {
                    return false;
                }

                if ( minimum < 0 || maximum < 0 || minimum > maximum )
                {
                    return false;
                }

                result = new PortRange( minimum , maximum );
                
                return true;
            }
            
            return false;
        }





        public bool Equals( PortRange obj )
        {
            return Equals( obj , this );
        }

        public override bool Equals( object obj )
        {
            return obj is PortRange && Equals( (PortRange) obj );
        }

        public override int GetHashCode()
        {
            return Minimum.GetHashCode() ^ Maximum.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Minimum}-{Maximum}";
        }
    }
}
