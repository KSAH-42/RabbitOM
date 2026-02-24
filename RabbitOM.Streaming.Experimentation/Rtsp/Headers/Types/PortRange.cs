using System;
using System.Linq;

namespace RabbitOM.Streaming.Experimentation.Rtsp.Headers.Types
{
    public struct PortRange
    {

        public static readonly PortRange Zero = new PortRange( 0 , 0 );


        public static readonly PortRange Any = new PortRange( 0 , int.MaxValue );





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






        public override string ToString()
        {
            return $"{Minimum}-{Maximum}";
        }





        public static bool TryParse( string input , out PortRange result )
        {
            result = default;

            if ( RtspHeaderParser.TryParse( RtspHeaderValueNormalizer.Normalize( input ) , "-" , out string[] tokens ) )
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
    }
}
