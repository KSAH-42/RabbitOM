﻿/*
 EXPERIMENTATION of the next implementation of the rtp layer

                    IMPLEMENTATION  NOT COMPLETED
*/

using System;
using System.IO;

namespace RabbitOM.Net.Rtp.H264
{
    public sealed class H264Decoder : IDisposable
    {
        private readonly MemoryStream _stream = new MemoryStream();

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

                if ( nalunit.IsSingle )
                {
                    continue;
                }

                if ( nalunit.IsSPS )
                {
                    OnDecodeSPS( nalunit );
                }

                if ( nalunit.IsPPS )
                {
                    OnDecodePPS( nalunit );
                }

                OnWriteStream( nalunit );
            }

            throw new NotImplementedException();
        }

        public void Clear()
        {
            _stream.SetLength(0);
        }

        public void Dispose()
        {
            _stream.Dispose();
        }





        
        private void OnDecodeSPS( H264NalUnit nalunit )
        {
        }

        private void OnDecodePPS( H264NalUnit nalunit )
        {
        }

        private void OnWriteStream( H264NalUnit nalunit )
        {
            if ( nalunit.Prefix.Length == 0 )
            {
                _stream.Write( StartPrefix.StartPrefixS4.Values , 0 , StartPrefix.StartPrefixS4.Values.Length );
            }

            _stream.Write( nalunit.Payload , 0 , nalunit.Payload.Length );
        }
    }
}