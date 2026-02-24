using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public struct InterleavedRange
    {

        public static readonly InterleavedRange Zero = new InterleavedRange( 0 , 0 );


        public static readonly InterleavedRange Any = new InterleavedRange( 0 , byte.MaxValue );





        public InterleavedRange( byte minimum , byte maximum )
        {
            if ( minimum > maximum )
            {
                throw new ArgumentOutOfRangeException();
            }

            Minimum = minimum;
            Maximum = maximum;
        }






        public byte Minimum { get; }

        public byte Maximum { get; }






        public override string ToString()
        {
            return $"{Minimum}-{Maximum}";
        }





        public static bool TryParse( string input , out InterleavedRange result )
        {
            result = default;

            if ( RtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , "-" , out string[] tokens ) )
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
    }
}
