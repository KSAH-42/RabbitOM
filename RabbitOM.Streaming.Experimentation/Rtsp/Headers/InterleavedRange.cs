using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers
{
    public struct InterleavedRange : IEquatable<InterleavedRange>
    {

        public static readonly InterleavedRange Zero = new InterleavedRange( 0 , 0 );







        public InterleavedRange( byte minimum , byte maximum )
        {
            if ( minimum > maximum )
            {
                throw new ArgumentOutOfRangeException( nameof( minimum ) );
            }

            Minimum = minimum;
            Maximum = maximum;
        }






        public byte Minimum { get; }

        public byte Maximum { get; }







        public static bool operator == ( InterleavedRange a , InterleavedRange b )
        {
            return Equals( a , b );
        }

        public static bool operator != ( InterleavedRange a , InterleavedRange b )
        {
            return ! Equals( a , b );
        }







        public static bool Equals( in InterleavedRange a , in InterleavedRange b )
        {
            return a.Minimum == b.Minimum && a.Maximum == b.Maximum;
        }


        public static bool TryParse( string input , out InterleavedRange result )
        {
            result = default;

            if ( RtspHeaderParser.TryParse( StringRtspHeaderNormalizer.Normalize( input ) , "-" , out string[] tokens ) )
            {
                if ( ! byte.TryParse( tokens.ElementAtOrDefault( 0 ) , out var minimum ) )
                {
                    return false;
                }

                if ( ! byte.TryParse( tokens.ElementAtOrDefault( 1 ) , out var maximum ) )
                {
                    return false;
                }

                if ( minimum > maximum )
                {
                    return false;
                }

                result = new InterleavedRange( minimum , maximum );
                
                return true;
            }

            return false;
        }





        public bool Equals( InterleavedRange obj )
        {
            return Equals( obj , this );
        }

        public override bool Equals( object obj )
        {
            return obj is InterleavedRange && Equals( (InterleavedRange) obj );
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
