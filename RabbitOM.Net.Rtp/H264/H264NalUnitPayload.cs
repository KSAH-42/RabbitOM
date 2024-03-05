using System;

namespace RabbitOM.Net.Rtp.H264
{
    // Make some tests and see if it is usefull to move
    // this class as a struct

    public sealed class H264NalUnitPayload
    {
        private readonly H264NalUnit _nalUnit;

#pragma warning disable CS0169

        private byte[] _data;

        private byte[] _fu_a;

        private byte[] _fu_b;

        private byte[] _stap_a;

        private byte[] _stap_b;

        private byte[] _pps;

        private byte[] _sps;

#pragma warning restore CS0169



        public H264NalUnitPayload( H264NalUnit nalunit )
        {
            _nalUnit = nalunit ?? throw new ArgumentNullException( nameof( nalunit ) );
        }




        public byte[] GetAllData()
        {
            throw new NotImplementedException();
        }

        public byte[] GetFuA()
        {
            throw new NotImplementedException();
        }

        public byte[] GetFuB()
        {
            throw new NotImplementedException();
        }

        public byte[] GetStapA()
        {
            throw new NotImplementedException();
        }

        public byte[] GetStapB()
        {
            throw new NotImplementedException();
        }

        public byte[] GetSPS()
        {
            throw new NotImplementedException();
        }

        public byte[] GetPPS()
        {
            throw new NotImplementedException();
        }




        public bool TryGetAllData( out byte[] result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetFuA( out byte[] result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetFuB( out byte[] result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetStapA( out byte[] result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetStapB( out byte[] result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetSPS( out byte[] result )
        {
            throw new NotImplementedException();
        }

        public bool TryGetPPS( out byte[] result )
        {
            throw new NotImplementedException();
        }




        private byte[] CreateFromBuffer( int length )
        {
            var buffer = new byte[ length ];

            Buffer.BlockCopy( _nalUnit.Buffer.Array , _nalUnit.Buffer.Offset , buffer , 0 , buffer.Length );

            return buffer;
        }
    }
}
