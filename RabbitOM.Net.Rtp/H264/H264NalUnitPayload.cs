/*
 EXPERIMENTATION of the next implementation of the rtp layer

              O P T I M I Z A T I O N S

 Reduce copy by using array segment to remove Buffer.Copy or similar methods


                    IMPLEMENTATION  NOT COMPLETED

Before to make any optimization:

  * First take a look on native code generation from IL Code
 
Then:

  => Reduce the number of statements on try parse methods
  
  => Time complexity O(N,M)

  => add tests for protocol violations

if ( _data == null )
            {
                _data = new byte[ _nalUnit.Buffer.Count ];
                
                System.Buffer.BlockCopy( _nalUnit.Buffer.Array , _nalUnit.Buffer.Offset , _data , 0 , _data.Length );
            }

            return _data;

        
*/
using System;

namespace RabbitOM.Net.Rtp.H264
{
    public sealed class H264NalUnitPayload
    {
        private readonly H264NalUnit _nalUnit;




		public H264NalUnitPayload( H264NalUnit nalunit )
		{
            _nalUnit = nalunit ?? throw new ArgumentNullException( nameof( nalunit ) );
		}

        


        public byte[] GetData()
        {
            throw new NotImplementedException();
        }

        public byte[] GetDataAsFU_A()
        {
            throw new NotImplementedException();
        }

        public byte[] GetDataAsFU_B()
        {
            throw new NotImplementedException();
        }

        public byte[] GetDataAsSTAP_A()
        {
            throw new NotImplementedException();
        }

        public byte[] GetDataAsSTAP_B()
        {
            throw new NotImplementedException();
        }

        public byte[] GetDataAsSPS()
        {
            throw new NotImplementedException();
        }

        public byte[] GetDataAsPPS()
        {
            throw new NotImplementedException();
        }
    }
}