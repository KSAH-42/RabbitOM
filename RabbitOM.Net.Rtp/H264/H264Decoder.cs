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



        public H264Decoder( byte[] sps , byte[] pps )
		{
            _parameterSet = new H264ParameterSet( sps , pps );
        }





        public bool TryDecode( H264NalUnitCollection nalunits , out byte[] result )
        {
            result = default;

            if ( nalunits == null )
            {
                return false;
            }

            using ( MemoryStream stream = new MemoryStream() )
            {
                while ( nalunits.Any() )
                {
                    H264NalUnit nalunit = nalunits.Dequeue();

                    if ( !nalunit.TryValidate() || nalunit.CanSkip() )
                    {
                        continue;
                    }

                    if ( ! nalunit.IsSingle )
                    {
                        continue;
                    }

                    if ( nalunit.IsSPS )
                    {
                        OnDecodeSPS( nalunit , stream );
                    }
                    else if ( nalunit.IsPPS )
                    {
                        OnDecodePPS( nalunit , stream );
                    }
                    else if ( nalunit.IsSlice )
                    {
                        OnDecodeSlice( nalunit , stream );
                    }
                    else
                    {
                        OnDecode( nalunit , stream );
                    }
                }

                stream.Flush();

                result = stream.ToArray();

                return result.Length > 0;
            }
        }

        public void Dispose()
        {
        }





        
        private void OnDecodeSPS( H264NalUnit nalunit , MemoryStream stream )
        {
            if ( nalunit.Payload.Length < 5 )
            {
                return;
            }

            throw new NotImplementedException();
        }

        private void OnDecodePPS( H264NalUnit nalunit , MemoryStream stream )
        {
            throw new NotImplementedException();
        }

        private void OnDecodeSlice( H264NalUnit nalunit , MemoryStream stream )
        {
            throw new NotImplementedException();
        }

        private void OnDecode( H264NalUnit nalunit , MemoryStream stream )
        {
            if ( nalunit.Prefix.Length == 0 )
            {
                stream.Write( StartPrefix.StartPrefixS4.Values , 0 , StartPrefix.StartPrefixS4.Values.Length );
            }

            stream.Write( nalunit.Payload , 0 , nalunit.Payload.Length );
        }
    }
}