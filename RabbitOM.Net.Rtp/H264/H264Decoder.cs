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
        private readonly MemoryStream _stream = new MemoryStream();

        public bool TryDecode( H264NalUnitCollection nalunits , out byte[] result )
        {
            result = default;

            if ( nalunits == null )
            {
                return false;
            }

            foreach ( var nalunit in nalunits )
            {
                if ( ! nalunit.TryValidate() || nalunit.CanSkip() )
                {
                    continue;
                }
            }

            throw new InvalidOperationException();
        }

        public void Clear()
        {
            _stream.SetLength(0);
        }

        public void Dispose()
        {
            _stream.Dispose();
        }
    }
}