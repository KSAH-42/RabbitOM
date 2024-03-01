/*
 EXPERIMENTATION of the next implementation of the rtp layer

                    IMPLEMENTATION  NOT COMPLETED
*/

using System;
using System.IO;

namespace RabbitOM.Net.Rtp.H264
{
    public sealed class H264Decoder : IDisposable
    {
        private readonly H264ParameterSet _parameterSet;
        private readonly MemoryStream _stream;




        public H264Decoder( byte[] sps , byte[] pps )
		{
            _parameterSet = new H264ParameterSet( sps , pps );

            _stream = new MemoryStream();
        }





        public bool TryDecode( H264NalUnitCollection nalunits , out byte[] result )
        {
            result = default;

            if ( nalunits == null )
            {
                return false;
            }

            while ( nalunits.Any() )
            {
                H264NalUnit nalunit = nalunits.Dequeue();

                if ( !nalunit.TryValidate() || nalunit.CanSkip() )
                {
                    continue;
                }

                if ( !nalunit.IsSingle )
                {
                    continue;
                }

                if ( nalunit.IsSPS )
                {
                    OnDecodeSPS( nalunit );
                }
                else if ( nalunit.IsPPS )
                {
                    OnDecodePPS( nalunit );
                }
                else if ( nalunit.IsSlice )
                {
                    OnDecodeSlice( nalunit );
                }
                else
                {
                    OnDecode( nalunit );
                }
            }

            _stream.Flush();

            result = _stream.ToArray();

            return result.Length > 0;
        }

        public void Dispose()
        {
        }





        
        private void OnDecodeSPS( H264NalUnit nalunit )
        {
            if ( nalunit.Payload.Length < 5 )
            {
                return;
            }

            throw new NotImplementedException();
        }

        private void OnDecodePPS( H264NalUnit nalunit )
        {
            throw new NotImplementedException();
        }

        private void OnDecodeSlice( H264NalUnit nalunit )
        {
            throw new NotImplementedException();
        }

        private void OnDecode( H264NalUnit nalunit )
        {
            if ( nalunit.Prefix.Length == 0 )
            {
                _stream.Write( StartPrefix.StartPrefixS4.Values , 0 , StartPrefix.StartPrefixS4.Values.Length );
            }

            _stream.Write( nalunit.Payload , 0 , nalunit.Payload.Length );
        }
    }
}