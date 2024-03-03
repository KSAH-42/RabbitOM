using System;

namespace RabbitOM.Net.Rtp.H264
{
    public sealed class H264NalUnitPayload
    {
        private readonly H264NalUnit _nalUnit;



        private byte[] _data;

        private byte[] _fu_a;

        private byte[] _fu_b;

        private byte[] _stap_a;

        private byte[] _stap_b;

        private byte[] _pps;

        private byte[] _sps;





        public H264NalUnitPayload( H264NalUnit nalunit )
        {
            _nalUnit = nalunit ?? throw new ArgumentNullException( nameof( nalunit ) );
        }

        


        public byte[] GetData()
        {
            if ( _data == null )
            {
                _data = CreateFromBuffer( _nalUnit.Buffer.Count );
            }

            return _data;
        }

        public byte[] GetFuA()
        {
            if ( _fu_a == null )
            {
                throw new NotImplementedException();
            }

            return _fu_a;
        }

        public byte[] GetFuB()
        {
            if ( _fu_b == null )
            {
                throw new NotImplementedException();
            }

            return _fu_b;
        }

        public byte[] GetStapA()
        {
            if ( _stap_a == null )
            {
                throw new NotImplementedException();
            }

            return _stap_a;
        }

        public byte[] GetStapB()
        {
            if ( _stap_b == null )
            {
                throw new NotImplementedException();
            }

            return _stap_b;
        }

        public byte[] GetSPS()
        {
            if ( _sps == null )
            {
                throw new NotImplementedException();
            }

            return _sps;
        }

        public byte[] GetPPS()
        {
            if ( _pps == null )
            {
                throw new NotImplementedException();
            }

            return _pps;
        }





        private byte[] CreateFromBuffer( int length )
        {
            var buffer = new byte[ length ];

            Buffer.BlockCopy( _nalUnit.Buffer.Array , _nalUnit.Buffer.Offset , buffer , 0 , buffer.Length );

            return buffer;
        }
    }
}