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

                if ( ! nalunit.TryValidate() || nalunit.CanSkip() )
                {
                    continue;
                }

                if ( ! nalunit.IsSingle )
                {
                    continue;
                }

                if ( nalunit.IsSPS )
                {
                    OnDecodingSPS( nalunit );
                }
                else if ( nalunit.IsPPS )
                {
                    OnDecodingPPS( nalunit );
                }
                else
                {
                    OnDecoding( nalunit );
                }
            }

            _stream.Flush();

            result = _stream.ToArray();

            return result.Length > 0;
        }

        public void Dispose()
        {
        }





        
        private void OnDecodingSPS( H264NalUnit nalunit )
        {
            var sps = nalunit.Payload.GetDataAsSPS();

            throw new NotImplementedException();
        }

        private void OnDecodingPPS( H264NalUnit nalunit )
        {
            var pps = nalunit.Payload.GetDataAsPPS();

            throw new NotImplementedException();
        }

        private void OnDecoding( H264NalUnit nalunit )
        {
            var data   = nalunit.Payload.GetData();

            var prefix = nalunit.Prefix.Length > 0 ? nalunit.Prefix : StartPrefix.StartPrefixS4.Values;


            _stream.Write( prefix , 0 , prefix.Length );
            _stream.Write( data , 0 , data.Length );
        }
    }
}